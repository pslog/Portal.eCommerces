using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Admin.BusinessLogic;
using WebApplication.Models.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Admin.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [Authorize]
    public class ProductController : Controller
    {
        private PortalEntities db = new PortalEntities();

        // GET: Product
        public ActionResult Index()
        {
            var product_Products = db.product_Products.Include(p => p.product_Categories);
            return View(product_Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Products product_Products = db.product_Products
               .Include(i => i.product_Categories)
               .Include(i => i.share_Images)
               .Where(i => i.ID == id)
               .SingleOrDefault();
            if (product_Products == null)
            {
                return HttpNotFound();
            }
            return View(product_Products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.product_Categories, "ID", "Title");
            PopulateCategoriesDropDownList();
            PopulateStatusDropDownList();
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,CategoryID,Title,Quantity,Unit,PriceOfUnit,CoverImageID,Description,Description2,TotalView,TotalBuy,Tags,IsNewProduct,IsBestSellProduct,SortOrder,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] product_Products product_Products, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = upload;
                if (upload != null && upload.ContentLength > 0)
                {
                    if (file.ContentLength > 0)
                    {
                        // width + height will force size, care for distortion
                        //Exmaple: ImageUpload imageUpload = new ImageUpload { Width = 800, Height = 700 };

                        // height will increase the width proportionally
                        //Example: ImageUpload imageUpload = new ImageUpload { Height= 600 };

                        // width will increase the height proportionally
                        ImageUpload imageUpload = new ImageUpload { Width = 600 };

                        // rename, resize, and upload
                        //return object that contains {bool Success,string ErrorMessage,string ImageName}
                        ImageResult imageResult = imageUpload.RenameUploadFile(file);
                        if (imageResult.Success)
                        {
                            //TODO: write the filename to the db
                            var photo = new share_Images
                            {
                                ImageName = imageResult.ImageName,
                                ImagePath = Path.Combine(ImageUpload.LoadPath, imageResult.ImageName)
                            };
                            product_Products.share_Images = new List<share_Images>();
                            product_Products.share_Images.Add(photo);
                            if (product_Products.share_Images.Count() > 0)
                            {
                                product_Products.CoverImageID = product_Products.share_Images.ElementAt(0).ID;
                            }
                        }
                        else
                        {
                            // use imageResult.ErrorMessage to show the error
                            ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }
                }

                product_Products.GUID = System.Guid.NewGuid();
                db.product_Products.Add(product_Products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateStatusDropDownList(product_Products.Status);
            ViewBag.CategoryID = new SelectList(db.product_Categories, "ID", "Title", product_Products.CategoryID);
            return View(product_Products);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Products product_Products = db.product_Products
               .Include(i => i.product_Categories)
               .Include(i => i.share_Images)
               .Where(i => i.ID == id)
               .Single();
            //product_Products product_Products = db.product_Products.Find(id);
            if (product_Products == null)
            {
                return HttpNotFound();
            }
            PopulateCategoriesDropDownList(product_Products.CategoryID);
            PopulateStatusDropDownList(product_Products.Status);
            //ViewBag.CategoryID = new SelectList(db.product_Categories, "ID", "Title", product_Products.CategoryID);
            return View(product_Products);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,GUID,ProductCode,CategoryID,Title,Quantity,Unit,PriceOfUnit,CoverImageID,Description,Description2,TotalView,TotalBuy,Tags,IsNewProduct,IsBestSellProduct,SortOrder,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] product_Products product_Products)
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Products productToUpdate = db.product_Products
               .Include(i => i.product_Categories)
               .Include(i => i.share_Images)
               .Where(i => i.ID == id)
               .Single();
            if (TryUpdateModel(productToUpdate, null, new string[] { "ProductCode", "CategoryID", "Title", "Quantity", "Unit", "PriceOfUnit", "Description", "Description2", "Tags", "IsNewProduct", "IsBestSellProduct", "SortOrder", "Status" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateCategoriesDropDownList(productToUpdate.CategoryID);
            PopulateStatusDropDownList(productToUpdate.Status);
            //ViewBag.CategoryID = new SelectList(db.product_Categories, "ID", "Title", product_Products.CategoryID);
            return View(productToUpdate);
        }
        private void PopulateCategoriesDropDownList(object selectedDepartment = null)
        {
            var CategoriesQuery = from c in db.product_Categories
                                  orderby c.Title
                                  select c;
            ViewBag.CategoryID = new SelectList(CategoriesQuery, "ID", "Title", selectedDepartment);
        }

        private void PopulateStatusDropDownList(object selectedStatus = null)
        {
            var Status = StatusProductViewModels.GetListStatusOptions();
            ViewBag.Status = new SelectList(Status, "StatusID", "Value", selectedStatus);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Products product_Products = db.product_Products.Find(id);
            if (product_Products == null)
            {
                return HttpNotFound();
            }
            return View(product_Products);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_Products product_Products = db.product_Products.Find(id);
            product_Products.share_Images.Clear();
            db.product_Products.Remove(product_Products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(IEnumerable<HttpPostedFileBase> files, int? IdProduct)
        {

            if (IdProduct == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Products product_Products = db.product_Products
               .Include(i => i.product_Categories)
               .Include(i => i.share_Images)
               .Where(i => i.ID == IdProduct)
               .Single();
            if (files != null)
            {
                foreach (var file in files)
                {

                    if (file.ContentLength > 0)
                    {
                        // width + height will force size, care for distortion
                        //Exmaple: ImageUpload imageUpload = new ImageUpload { Width = 800, Height = 700 };

                        // height will increase the width proportionally
                        //Example: ImageUpload imageUpload = new ImageUpload { Height= 600 };

                        // width will increase the height proportionally
                        ImageUpload imageUpload = new ImageUpload { Width = 600 };

                        // rename, resize, and upload
                        //return object that contains {bool Success,string ErrorMessage,string ImageName}
                        ImageResult imageResult = imageUpload.RenameUploadFile(file);
                        if (imageResult.Success)
                        {
                            //TODO: write the filename to the db
                            var photo = new share_Images
                            {
                                ImageName = imageResult.ImageName,
                                ImagePath = Path.Combine(ImageUpload.LoadPath, imageResult.ImageName)
                            };
                            if (product_Products.share_Images == null)
                            {
                                product_Products.share_Images = new List<share_Images>();
                            }
                            product_Products.share_Images.Add(photo);
                            if (product_Products.share_Images.Count() > 0)
                            {
                                product_Products.CoverImageID = product_Products.share_Images.ElementAt(0).ID;
                            }

                        }
                        else
                        {
                            // use imageResult.ErrorMessage to show the error
                            ViewBag.Error = imageResult.ErrorMessage;
                        }
                    }

                }
                db.SaveChanges();
            }
            LoadListImageProductPartialViewModels listImageViewModels = new LoadListImageProductPartialViewModels()
            {
                ProductId = product_Products.ID,
                Images = product_Products.share_Images
            };
            return PartialView("LoadListImageProduct", listImageViewModels);
        }

        public ActionResult LoadListImageProduct()
        {
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public void Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/[Your_Folder_Name]/"), fileName);

                file.SaveAs(path);
            }
        }

        /// <summary>
        /// Remove a image from list images of product
        /// </summary>
        /// <param name="Id">product Id</param>
        /// <returns>updated view of Cart</returns>
        public ActionResult DeleteImage(int productId, int imageId)
        {
            product_Products product_Products = db.product_Products
               .Include(i => i.product_Categories)
               .Include(i => i.share_Images)
               .Where(i => i.ID == productId)
               .SingleOrDefault();
            if (product_Products != null && product_Products.share_Images.Count() > 0)
            {
                share_Images image = product_Products.share_Images.Where(i => i.ID == imageId).SingleOrDefault();
                if (image != null)
                {
                    try
                    {
                        product_Products.share_Images.Remove(image);
                        var deleteImage = db.share_Images.First(i => i.ID == image.ID);
                        var path = deleteImage.ImagePath;
                        db.share_Images.Remove(deleteImage);
                        db.SaveChanges();
                        DeleteImageInFolder(path);
                    }
                    catch (RetryLimitExceededException)
                    {

                    }
                }
            }

            LoadListImageProductPartialViewModels listImageViewModels = new LoadListImageProductPartialViewModels()
            {
                ProductId = product_Products.ID,
                Images = product_Products.share_Images
            };
            return PartialView("LoadListImageProduct", listImageViewModels);
        }

        private bool DeleteImageInFolder(string path)
        {

            string filePath = Server.MapPath("~/" + path);
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
