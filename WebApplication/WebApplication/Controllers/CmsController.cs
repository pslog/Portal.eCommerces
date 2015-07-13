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

namespace WebApplication.Controllers
{
    public class CmsController : Controller
    {
        private PortalEntities db = new PortalEntities();

        private IUnitOfWork uow = UnitOfWork.Begin();

        // GET: Category
        public async Task<ActionResult> CmsCategoryIndex(PagingRouteValue routeValue = null)
        {
            //ViewBag.SearchKey = searchKey;
            //ViewBag.OrderBy = orderBy;
            //ViewBag.OrderByDesc = orderByDesc;
            //ViewBag.PageNumber = pageNumber;
            //ViewBag.TotalPages = totalPages;

            if (string.IsNullOrEmpty(routeValue.ActionName) || string.IsNullOrEmpty(routeValue.ControllerName))
            {
                routeValue.ActionName = RouteName.CmsCategory.CmsCategoryIndex;
                routeValue.ControllerName = RouteName.CmsCategory.Controller;
            }

            return View(await Task.FromResult<CmsCategoryView>(uow.CmsCategory.SearchCategories(routeValue)));
        }

        // GET: Category/Details/5
        public async Task<ActionResult> CmsCategoryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cms_Categories cms_Categories = await db.cms_Categories.FindAsync(id);
            if (cms_Categories == null)
            {
                return HttpNotFound();
            }
            return View(cms_Categories);
        }

        // GET: Category/Create
        public ActionResult CreateCmsCategory(int? cmsCategoryID = null)
        {
            var parent = uow.CmsCategory.GetById(cmsCategoryID ?? -1);

            if (parent != null)
            {
                ViewBag.ParentID = parent.GUID;
                ViewBag.ParentTitle = parent.Title;
            }

            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCmsCategory([Bind(Exclude = ExcludeProperties.CmsCategory)] cms_Categories cmsCategory)
        {
            if (ModelState.IsValid)
            {
                uow.CmsCategory.Create(uow.CmsCategory.GetCmsCategory(cmsCategory, 0, 0));

                await uow.CommitAsync();
                
                return RedirectToAction("CmsCategoryIndex");
            }

            return View(cmsCategory);
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> EditCmsCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cms_Categories cms_Categories = await db.cms_Categories.FindAsync(id);
            if (cms_Categories == null)
            {
                return HttpNotFound();
            }
            return View(cms_Categories);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCmsCategory([Bind(Include = "ID,GUID,ParentID,Title,Description,Url,SortOrder,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] cms_Categories cms_Categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cms_Categories).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cms_Categories);
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> DeleteCmsCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cms_Categories cms_Categories = await db.cms_Categories.FindAsync(id);
            if (cms_Categories == null)
            {
                return HttpNotFound();
            }
            return View(cms_Categories);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCmsCategoryConfirmed(int id)
        {
            cms_Categories cms_Categories = await db.cms_Categories.FindAsync(id);
            db.cms_Categories.Remove(cms_Categories);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: News
        public async Task<ActionResult> CmsNewsIndex()
        {
            var cms_News = db.cms_News.Include(c => c.cms_Categories);
            return View(await cms_News.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<ActionResult> CmsNewsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cms_News cms_News = await db.cms_News.FindAsync(id);
            if (cms_News == null)
            {
                return HttpNotFound();
            }
            return View(cms_News);
        }

        // GET: News/Create
        public ActionResult CreateCmsNews()
        {
            ViewBag.CategoryID = new SelectList(db.cms_Categories, "ID", "Title");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCmsNews([Bind(Include = "ID,GUID,CategoryID,Title,SubTitle,ContentNews,Authors,Tags,TotalView,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] cms_News cms_News)
        {
            if (ModelState.IsValid)
            {
                db.cms_News.Add(cms_News);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.cms_Categories, "ID", "Title", cms_News.CategoryID);
            return View(cms_News);
        }

        // GET: News/Edit/5
        public async Task<ActionResult> EditCmsNews(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cms_News cms_News = await db.cms_News.FindAsync(id);
            if (cms_News == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.cms_Categories, "ID", "Title", cms_News.CategoryID);
            return View(cms_News);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCmsNews([Bind(Include = "ID,GUID,CategoryID,Title,SubTitle,ContentNews,Authors,Tags,TotalView,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] cms_News cms_News)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cms_News).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.cms_Categories, "ID", "Title", cms_News.CategoryID);
            return View(cms_News);
        }

        // GET: News/Delete/5
        public async Task<ActionResult> DeleteCmsNews(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cms_News cms_News = await db.cms_News.FindAsync(id);
            if (cms_News == null)
            {
                return HttpNotFound();
            }
            return View(cms_News);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            cms_News cms_News = await db.cms_News.FindAsync(id);
            db.cms_News.Remove(cms_News);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
