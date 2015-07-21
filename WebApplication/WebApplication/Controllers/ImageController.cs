using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.Models;

namespace WebApplication.Admin.Controllers
{
    public class ImageController : Controller
    {
        private PortalEntities db = new PortalEntities();

        // GET: Image
        public ActionResult Index()
        {
            return View(db.share_Images.ToList());
        }

        // GET: Image/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            share_Images share_Images = db.share_Images.Find(id);
            if (share_Images == null)
            {
                return HttpNotFound();
            }
            return View(share_Images);
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GUID,ImageName,ImagePath,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] share_Images share_Images)
        {
            if (ModelState.IsValid)
            {
                db.share_Images.Add(share_Images);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(share_Images);
        }

        // GET: Image/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            share_Images share_Images = db.share_Images.Find(id);
            if (share_Images == null)
            {
                return HttpNotFound();
            }
            return View(share_Images);
        }

        // POST: Image/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GUID,ImageName,ImagePath,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] share_Images share_Images)
        {
            if (ModelState.IsValid)
            {
                db.Entry(share_Images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(share_Images);
        }

        // GET: Image/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            share_Images share_Images = db.share_Images.Find(id);
            if (share_Images == null)
            {
                return HttpNotFound();
            }
            return View(share_Images);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            share_Images share_Images = db.share_Images.Find(id);
            db.share_Images.Remove(share_Images);
            db.SaveChanges();
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
