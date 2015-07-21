using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.Models;
using WebApplication.Models.ViewModels;
using WebApplication.BusinessLogic.BusinessLogic;
using WebApplication.BusinessLogic.Repositories;

namespace WebApplication.Admin.Controllers
{
    public class CategoryController : Controller
    {
        #region fields
        public CategoryRepository _categoryRepository;
        private PortalEntities db = new PortalEntities();

        #endregion


        public CategoryController()
        {
            _categoryRepository = new CategoryRepository();
        }
        // GET: Category
        public ActionResult Index()
        {
            return View(db.product_Categories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Categories product_Categories = db.product_Categories.Find(id);
            if (product_Categories == null)
            {
                return HttpNotFound();
            }
            DetailsCategoryViewModels detailsCategoryViewModels = product_Categories.ConvertToDetailsCategoryViewModels();
            return View(detailsCategoryViewModels);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            PopulateCategoriesDropDownList();
            PopulateStatusDropDownList();
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Url,SortOrder,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] product_Categories product_Categories, string ParentID)
        {
            if (ModelState.IsValid)
            {
                if (ParentID.Trim().Equals(""))
                {
                    product_Categories.ParentID = Guid.Empty;
                }
                else
                {
                    product_Categories.ParentID = Guid.Parse(ParentID);
                }
                product_Categories.GUID = System.Guid.NewGuid();
                db.product_Categories.Add(product_Categories);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_Categories);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Categories product_Categories = db.product_Categories.Find(id);
            if (product_Categories == null)
            {
                return HttpNotFound();
            }
            if (product_Categories.ParentID == Guid.Empty)
            {
                PopulateCategoriesDropDownList();
            }
            else
            {
                PopulateCategoriesDropDownList(product_Categories.ParentID);
            }
            PopulateStatusDropDownList(product_Categories.Status);
            return View(product_Categories);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GUID,Title,Description,Url,SortOrder,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] product_Categories product_Categories, string ParentID)
        {
            if (ModelState.IsValid)
            {
                if (ParentID.Trim().Equals(""))
                {
                    product_Categories.ParentID = Guid.Empty;
                }
                else
                {
                    product_Categories.ParentID = Guid.Parse(ParentID);
                }
                db.Entry(product_Categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_Categories);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Categories product_Categories = db.product_Categories.Find(id);
            if (product_Categories == null)
            {
                return HttpNotFound();
            }
            DetailsCategoryViewModels detailsCategoryViewModels = product_Categories.ConvertToDetailsCategoryViewModels();
            return View(detailsCategoryViewModels);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_Categories product_Categories = db.product_Categories.Find(id);
            List<product_Categories> temp = new List<product_Categories>();
            temp = db.product_Categories.Where(c => c.ParentID == product_Categories.GUID).ToList();
            List<product_Products> temp2 = new List<product_Products>();
            temp2 = db.product_Products.Where(p => p.CategoryID == id).ToList();
            if (temp.Count == 0 && temp2.Count()==0)
            {
                db.product_Categories.Remove(product_Categories);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateCategoriesDropDownList(object selectedDepartment = null)
        {
            var CategoriesQuery = from c in db.product_Categories
                                  orderby c.Title
                                  select c;
            ViewBag.ParentID = new SelectList(CategoriesQuery, "GUID", "Title", selectedDepartment);
        }

        private void PopulateStatusDropDownList(object selectedStatus = null)
        {
            var Status = StatusCategoryViewModel.GetListStatusOptions();
            ViewBag.Status = new SelectList(Status, "StatusID", "Value", selectedStatus);
        }

        public ActionResult ListCategoriesLeftMenu()
        {
            IList<ListCategoriesLeftMenuViewModels> listCategoriesLeftMenuViewModels = new List<ListCategoriesLeftMenuViewModels>();
            var categories = _categoryRepository.GetAllRootCategory();
            if (categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    ListCategoriesLeftMenuViewModels cate = new ListCategoriesLeftMenuViewModels();
                    cate = category.ConvertToCategoriesLeftMenuViewModels();
                    IList<product_Categories> childCategories = _categoryRepository.GetChildCategory(category.GUID);
                    IList<ListCategoriesLeftMenuViewModels> childsCate = new List<ListCategoriesLeftMenuViewModels>();
                    childsCate = childCategories.ConvertToListCategoriesLeftMenuViewModels();
                    cate.Childs = childsCate;
                    listCategoriesLeftMenuViewModels.Add(cate);
                }
            }

            return PartialView("_CategoryProducts_LeftSideBar_Partial", listCategoriesLeftMenuViewModels);
        }
    }
}
