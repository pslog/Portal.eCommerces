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

namespace WebApplication.Controllers
{
    public class NewsController : Controller
    {
        private PortalEntities db = new PortalEntities();

        // GET: Category
        public async Task<ActionResult> CategoryIndex()
        {
            return View(await db.cms_Categories.ToListAsync());
        }

        // GET: Category/Details/5
        public async Task<ActionResult> CategoryDetails(int? id)
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
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory([Bind(Include = "ID,GUID,ParentID,Title,Description,Url,SortOrder,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] cms_Categories cms_Categories)
        {
            if (ModelState.IsValid)
            {
                db.cms_Categories.Add(cms_Categories);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cms_Categories);
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> EditCategory(int? id)
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
        public async Task<ActionResult> EditCategory([Bind(Include = "ID,GUID,ParentID,Title,Description,Url,SortOrder,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] cms_Categories cms_Categories)
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
        public async Task<ActionResult> DeleteCategory(int? id)
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
        public async Task<ActionResult> DeleteCategoryConfirmed(int id)
        {
            cms_Categories cms_Categories = await db.cms_Categories.FindAsync(id);
            db.cms_Categories.Remove(cms_Categories);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }





        // GET: News
        public async Task<ActionResult> Index()
        {
            var cms_News = db.cms_News.Include(c => c.cms_Categories);
            return View(await cms_News.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<ActionResult> Details(int? id)
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
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.cms_Categories, "ID", "Title");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,GUID,CategoryID,Title,SubTitle,ContentNews,Authors,Tags,TotalView,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] cms_News cms_News)
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
        public async Task<ActionResult> Edit(int? id)
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,GUID,CategoryID,Title,SubTitle,ContentNews,Authors,Tags,TotalView,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] cms_News cms_News)
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
        public async Task<ActionResult> Delete(int? id)
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
