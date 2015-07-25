using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.Models;
using WebApplication.Common;
using Uow.Package.Data;
using WebApplication.Common.Constants;
using WebApplication.Models.ViewModels;
using System.Data.Entity.Validation;
using PagedList;
using WebApplication.Libraries.Extensions;

namespace WebApplication.Controllers
{
    public class CmsController : Controller
    {
        private IUnitOfWork uow = UnitOfWork.Begin();

        #region CmsCategory
        // GET: Category
        public async Task<ActionResult> CmsCategoryIndex(int? id, int pageNumber = 1, string searchKey = "")
        {
            try
            {
                ViewBag.RootCategories = await Task.FromResult<List<cms_Categories>>(uow.CmsCategory.GetCmsCategoriesByParentID(null).ToList());
                ViewBag.CategoryID = id;
                ViewBag.PageNumber = pageNumber;
                ViewBag.SearchKey = searchKey;

                if(id == null)
                {
                    return View(Enumerable.Empty<cms_Categories>().ToPagedList<cms_Categories>(pageNumber, Common.Constants.ConstValue.PageSize));
                }

                var cmsCategories = await Task.FromResult<List<cms_Categories>>(uow.CmsCategory.GetCmsCategoriesByParentID(id).ToList());

                if(!string.IsNullOrEmpty(searchKey))
                {
                    cmsCategories = cmsCategories.Where(c => c.Title.ToLower().Contains(searchKey.ToLower()) || c.Description.ToLower().Contains(searchKey.ToLower())).ToList();
                }

                return View(cmsCategories.OrderByDescending(c => c.CreatedDate).ToPagedList<cms_Categories>(pageNumber, Common.Constants.ConstValue.PageSize));
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        // GET: Category/Create
        public async Task<ActionResult> CreateCmsCategory(int? parentID = null)
        {
            try
            {
                var parent = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetById(parentID ?? 0));

                if (parent != null)
                {
                    ViewBag.ParentTitle = parent.Title;
                }

                ViewBag.ParentID = parentID;

                return View();
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCmsCategory([Bind(Exclude = "ID")]cms_Categories cmsCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    uow.CmsCategory.Create(uow.CmsCategory.GetNewCmsCategory(cmsCategory, 0, 0));

                    await uow.CommitAsync();

                    return RedirectToAction(RouteName.CmsCategoryAction.Index);
                }

                return View(cmsCategory);
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); ;
            }
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> EditCmsCategory(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var category = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetById((int)id));

                if (category == null)
                {
                    return HttpNotFound();
                }

                if(category.ParentID != null)
                {
                    var parents = await Task.FromResult<List<cms_Categories>>(uow.CmsCategory.GetCmsCategoriesByParentID(null).ToList());
                    ViewBag.ParentTitle = category.cms_Categories2.Title;
                    ViewBag.Parents = new SelectList(parents, "ID", "Title", category.ParentID);
                }

                return View(category);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCmsCategory(cms_Categories cmsCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (uow.CmsCategory.Update(uow.CmsCategory.GetUpdateCmsCategory(cmsCategory, 1), "ParentID", "Title", "Description", "ModifiedBy", "ModifiedDate"))
                    {
                        await uow.CommitAsync();
                        return RedirectToAction(RouteName.CmsCategoryAction.Index);
                    }
                }

                return View(cmsCategory);
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> DeleteCmsCategory(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var cmsCategory = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetById(id ?? 0));

                if (cmsCategory == null)
                {
                    return HttpNotFound();
                }
                return View(cmsCategory);
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("DeleteCmsCategory")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCmsCategoryConfirmed(int id)
        {
            try
            {
                var cmsCategory = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetById(id));

                uow.CmsCategory.Delete(cmsCategory);

                await uow.CommitAsync();

                return RedirectToAction(RouteName.CmsCategoryAction.Index);
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
      
    #endregion


        public async Task<ActionResult> AdminCmsNewsIndex(int? categoryID, int pageNumber = 1, string searchKey = "")
        {
            try
            {
                string rootCagetoryTitle = string.Empty;
                var cmsNews = await Task.FromResult<List<cms_News>>(uow.CmsNews.GetCmsNewsByCategoryID(categoryID, uow.CmsCategory, out rootCagetoryTitle, searchKey.ToLower()).ToList());

                ViewBag.CategoryID = categoryID;
                ViewBag.CategoryTitle = rootCagetoryTitle.ToLower();
                ViewBag.PageNumber = pageNumber;
                ViewBag.SearchKey = searchKey;

                return View(cmsNews.ToPagedList(pageNumber, Common.Constants.ConstValue.PageSize));
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public async Task<ActionResult> GuestCmsNewsIndex(int? categoryID, int pageNumber = 1, string searchKey = "")
        {
            try
            {
                var cate = new cms_Categories();

                string rootCagetoryTitle = string.Empty;
                var cmsNews = await Task.FromResult<List<cms_News>>(uow.CmsNews.GetCmsNewsByCategoryID(categoryID ?? 0, uow.CmsCategory, out rootCagetoryTitle, searchKey.ToLower()).ToList());

                ViewBag.CmsCategories = await Task.FromResult<List<cms_Categories>>(uow.CmsCategory.GetCmsCategoriesByParentID(categoryID).ToList());
                ViewBag.CategoryParents = await Task.FromResult<List<cms_Categories>>(uow.CmsCategory.GetById(categoryID ?? 0).GetParents<cms_Categories>("cms_Categories2"));
                ViewBag.CategoryID = categoryID;
                ViewBag.CategoryTitle = rootCagetoryTitle.ToUpper();
                ViewBag.PageNumber = pageNumber;
                ViewBag.SearchKey = searchKey;

                return View(cmsNews.ToPagedList(pageNumber, 5));
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult CmsNewsIndex(int? categoryID)
        {
            ViewBag.CategoryID = categoryID;

            return View();
        }

        // GET: News/Create
        public async Task<ActionResult> CreateCmsNews(int? categoryID)
        {
            try
            {
                var categories = await Task.FromResult<IEnumerable<cms_Categories>>(uow.CmsCategory.GetAll());
                ViewBag.CmsCategories = new SelectList(categories, "ID", "Title", categoryID ?? 0);
                return View();

            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateCmsNews([Bind(Exclude = "ID, GUID")] cms_News cmsNews)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    uow.CmsNews.Create(uow.CmsNews.GetNewCmsNews(cmsNews, 0, 0));
                    await uow.CommitAsync();
                    return RedirectToAction("EditCmsCategory", new { id = cmsNews.CategoryID });
                }

                var categories = await Task.FromResult<IEnumerable<cms_Categories>>(uow.CmsCategory.GetAll());
                ViewBag.CmsCategories = new SelectList(categories, "ID", "Title", cmsNews.CategoryID);
                return View(cmsNews);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: News/Details/5
        public async Task<ActionResult> CmsNewsDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var cmsNews = await Task.FromResult<cms_News>(uow.CmsNews.GetById((int)id));

                if (cmsNews == null)
                {
                    return HttpNotFound();
                }

                return View(cmsNews);
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: News/Edit/5
        public async Task<ActionResult> EditCmsNews(int? id, int? categoryID)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var cmsNews = await Task.FromResult<cms_News>(uow.CmsNews.GetById(id ?? 0));

                if (cmsNews == null)
                {
                    return HttpNotFound();
                }

                var categories = await Task.FromResult<IEnumerable<cms_Categories>>(uow.CmsCategory.GetAll());
                ViewBag.CmsCategories = new SelectList(categories, "ID", "Title", cmsNews.CategoryID);
                ViewBag.CategoryID = categoryID;
                return View(cmsNews);
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> EditCmsNews(cms_News cmsNews)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (uow.CmsNews.Update(uow.CmsNews.GetUpdateCmsNews(cmsNews, 1), "CategoryID", "Title", "SubTitle", "ContentNews", "Authors"))
                    {
                        await uow.CommitAsync();
                        return RedirectToAction("EditCmsCategory", new { id = cmsNews.CategoryID });
                    }
                }

                var categories = await Task.FromResult<IEnumerable<cms_Categories>>(uow.CmsCategory.GetAll());
                ViewBag.CmsCategories = new SelectList(categories, "ID", "Title", cmsNews.CategoryID);

                return View(cmsNews);
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: News/Delete/5
        public async Task<ActionResult> DeleteCmsNews(int? id, int? categoryID)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var cmsNews = await Task.FromResult<cms_News>(uow.CmsNews.GetById(id ?? 0));

                if (cmsNews == null)
                {
                    return HttpNotFound();
                }

                ViewBag.CategoryID = categoryID;

                return View(cmsNews);
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("DeleteCmsNews")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCmsNewsConfirmed(int id, int? categoryID)
        {
            try
            {
                var cmsNews = await Task.FromResult<cms_News>(uow.CmsNews.GetById(id));

                uow.CmsNews.Delete(cmsNews);

                await uow.CommitAsync();

                return RedirectToAction("AdminCmsNewsIndex", new { categoryID = categoryID });
            }
            catch(Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
