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

namespace WebApplication.Controllers
{
    public class CmsController : Controller
    {
        private IUnitOfWork uow = UnitOfWork.Begin();

        // GET: Category
        public async Task<ActionResult> CmsCategoryIndex(PagingRouteValue routeValue = null)
        {
            if (string.IsNullOrEmpty(routeValue.ActionName) || string.IsNullOrEmpty(routeValue.ControllerName))
            {
                routeValue.ActionName = RouteName.CmsCategoryAction.Index;
                routeValue.ControllerName = RouteName.Controller.Cms;
            }

            return View(await Task.FromResult<PagingView<cms_Categories>>(uow.CmsCategory.GetIndexView(routeValue)));
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

            //var parent = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetByGuid(cmsCategory.ParentID));
            var parent = await Task.FromResult<cms_Categories>(uow.CmsCategory.GetById(cmsCategory.ParentID ?? 0));

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
        [ValidateInput(false)]
        public async Task<ActionResult> CreateCmsCategory([Bind(Exclude = "CmsCategory.ID, ParentID")]CmsCategoryCreateView createView)
        {
            if (ModelState.IsValid)
            {
                uow.CmsCategory.Create(uow.CmsCategory.GetNewCmsCategory(createView.CmsCategory, 0, 0));

                await uow.CommitAsync();
                
                return RedirectToAction(RouteName.CmsCategoryAction.Index);
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
        [ValidateInput(false)]
        public async Task<ActionResult> EditCmsCategory(CmsCategoryEditView editView)
        {
            if (ModelState.IsValid)
            {
                if (uow.CmsCategory.Update(uow.CmsCategory.GetUpdateCmsCategory(editView.CmsCategory, 1), "ParentID", "Title", "Description", "ModifiedBy", "ModifiedDate"))
                {
                    await uow.CommitAsync();
                    return RedirectToAction(RouteName.CmsCategoryAction.Index);
                }
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
            
            return RedirectToAction(RouteName.CmsCategoryAction.Index);
        }
      
        public ActionResult CmsNewsIndex(int? categoryID)
        {
            ViewBag.CategoryID = categoryID;

            return View();
        }

        public ActionResult GetCmsCategories(int? parentID)
        {
            var cmsCategories = uow.CmsCategory.GetCmsCategories(parentID);

            return View(cmsCategories);
        }

        public async Task<ActionResult> GetCmsNews(CmsNewsIndexViewDTO indexView)
        {
            bool isAuthenticate = false;

            indexView.CategoryID = indexView.CategoryID == 0 ? null : indexView.CategoryID;

            if(indexView.RouteValue == null)
            {
                indexView.RouteValue = new PagingRouteValue("GetCmsNews", "Cms", new { CategoryID = indexView.CategoryID }, new System.Web.Mvc.Ajax.AjaxOptions { UpdateTargetId = "cms_news_wrapper" });
            }

            indexView.RouteValue.OptionValues = new { CategoryID = indexView.CategoryID };
            indexView.RouteValue.RouteValuePrefix = "RouteValue";

            var cmsNews = await Task.FromResult<PagingView<cms_News>>(uow.CmsNews.GetPagingView(indexView, uow.CmsCategory, isAuthenticate ? 0 : 5));

            return isAuthenticate ? View(cmsNews) : View("GetCmsNewsForGuest", cmsNews);
        }

        // GET: News/Details/5
        public async Task<ActionResult> CmsNewsDetails(int? id)
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

        // GET: News/Create
        public async Task<ActionResult> CreateCmsNews(int? categoryID)
        {
            return View(await Task.FromResult<CmsNewsDTO>(uow.CmsNews.GetCmsNewsDTO(categoryID, uow.CmsCategory)));
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateCmsNews([Bind(Exclude = "CmsNews.ID, CmsNews.GUID")] CmsNewsDTO cmsNewsDTO)
        {
            if (ModelState.IsValid)
            {
                var cmsNews = uow.CmsNews.GetNewCmsNews(cmsNewsDTO.CmsNews, 0, 0);

                uow.CmsNews.Create(cmsNews);

                await uow.CommitAsync();

                return RedirectToAction("CmsNewsIndex", new { categoryID = cmsNews.CategoryID });
            }

            return View(await Task.FromResult<CmsNewsDTO>(uow.CmsNews.GetCmsNewsDTO(cmsNewsDTO.CmsNews.CategoryID ?? 0, uow.CmsCategory, cmsNewsDTO.CmsNews)));
        }

        // GET: News/Edit/5
        public async Task<ActionResult> EditCmsNews(int? id)
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

            return View(await Task.FromResult<CmsNewsDTO>(uow.CmsNews.GetCmsNewsDTO(cmsNews.CategoryID, uow.CmsCategory, cmsNews)));
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> EditCmsNews(CmsNewsDTO cmsNewsDTO)
        {
            if (ModelState.IsValid)
            {
                if (uow.CmsNews.Update(uow.CmsNews.GetUpdateCmsNews(cmsNewsDTO.CmsNews, 1), "CategoryID", "Title", "SubTitle", "Authors", "Tags", "ContentNews", "ModifiedBy", "ModifiedDate"))
                {
                    await uow.CommitAsync();
                    return RedirectToAction("CmsNewsIndex", new { categoryID = cmsNewsDTO.CmsNews.CategoryID });
                }
            }

            return View(await Task.FromResult<CmsNewsDTO>(uow.CmsNews.GetCmsNewsDTO(cmsNewsDTO.CmsNews.CategoryID ?? 0, uow.CmsCategory, cmsNewsDTO.CmsNews)));
        }

        // GET: News/Delete/5
        public async Task<ActionResult> DeleteCmsNews(int? id)
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

            return View(cmsNews);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("DeleteCmsNews")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCmsNewsConfirmed(int id)
        {
            var cmsNews = await Task.FromResult<cms_News>(uow.CmsNews.GetById(id));
            int? categoryID = cmsNews.CategoryID;

            uow.CmsNews.Delete(cmsNews);

            await uow.CommitAsync();

            return RedirectToAction("CmsNewsIndex", new { categoryID = categoryID });
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
