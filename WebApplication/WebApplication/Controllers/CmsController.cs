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
            if (string.IsNullOrEmpty(routeValue.ActionName) || string.IsNullOrEmpty(routeValue.ControllerName))
            {
                routeValue.ActionName = RouteName.CmsCategory.Index;
                routeValue.ControllerName = RouteName.CmsCategory.Controller;
            }

            return View(await Task.FromResult<CmsCategoryIndexView>(uow.CmsCategory.GetIndexView(routeValue)));
        }

        // GET: Category/Details/5
        public async Task<ActionResult> CmsCategoryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cms_Categories cmsCategory = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetById(id ?? 0));

            if (cmsCategory == null)
            {
                return HttpNotFound();
            }

            var parent = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetByGuid(cmsCategory.ParentID));

            ViewBag.ParentTitle = parent == null ? string.Empty : parent.Title;

            return View(cmsCategory);
        }

        // GET: Category/Create
        public async Task<ActionResult> CreateCmsCategory(int? parentID = null)
        {
            return View(await Task.FromResult<CmsCategoryCreateView>(uow.CmsCategory.GetCreateView(parentID)));
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCmsCategory([Bind(Exclude = "CmsCategory.ID, ParentID")]CmsCategoryCreateView createView)
        {
            if (ModelState.IsValid)
            {
                uow.CmsCategory.Create(uow.CmsCategory.GetNewCmsCategory(createView.CmsCategory, 0, 0));

                await uow.CommitAsync();
                
                return RedirectToAction(RouteName.CmsCategory.Index);
            }

            return View(createView);
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> EditCmsCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var editView = await Task.FromResult<CmsCategoryEditView>(uow.CmsCategory.GetEditView(id ?? 0));

            if (editView == null)
            {
                return HttpNotFound();
            }

            return View(editView);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCmsCategory(CmsCategoryEditView editView)
        {
            if (ModelState.IsValid)
            {
                uow.CmsCategory.Update(uow.CmsCategory.GetUpdateCmsCategory(editView.CmsCategory, 1));
                await uow.CommitAsync();
                return RedirectToAction(RouteName.CmsCategory.Index);
            }
            return View(editView);
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> DeleteCmsCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //cms_Categories cms_Categories = await db.cms_Categories.FindAsync(id);
            
            var cmsCategory = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetById(id ?? 0));

            if (cmsCategory == null)
            {
                return HttpNotFound();
            }
            return View(cmsCategory);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("DeleteCmsCategory")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCmsCategoryConfirmed(int id)
        {
            var cmsCategory = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetById(id));

            uow.CmsCategory.Delete(cmsCategory);

            await uow.CommitAsync();

            //cms_Categories cms_Categories = await db.cms_Categories.FindAsync(id);
            //db.cms_Categories.Remove(cms_Categories);
            //await db.SaveChangesAsync();
            return RedirectToAction(RouteName.CmsCategory.Index);
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
