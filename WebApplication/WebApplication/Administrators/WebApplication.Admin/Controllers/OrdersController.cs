using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private PortalEntities db = new PortalEntities();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.product_Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Orders product_Orders = db.product_Orders.Find(id);
            if (product_Orders == null)
            {
                return HttpNotFound();
            }
            return View(product_Orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,OrderCode,TotalPrice,FeeShip,TotalOrder,OrderStatus,OrderNote,NameOfRecipient,PhoneOfRecipient,AddressOfRecipient")] product_Orders product_Orders)
        {
            if (ModelState.IsValid)
            {
                product_Orders.GUID = System.Guid.NewGuid();
                db.product_Orders.Add(product_Orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_Orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Orders product_Orders = db.product_Orders.Find(id);
            if (product_Orders == null)
            {
                return HttpNotFound();
            }
            return View(product_Orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,OrderCode,TotalPrice,FeeShip,TotalOrder,OrderStatus,OrderNote,NameOfRecipient,PhoneOfRecipient,AddressOfRecipient")] product_Orders product_Orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Orders).State = EntityState.Modified;
                product_Orders.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_Orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_Orders product_Orders = db.product_Orders.Find(id);
            if (product_Orders == null)
            {
                return HttpNotFound();
            }
            return View(product_Orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_Orders product_Orders = db.product_Orders.Find(id);
            db.product_Orders.Remove(product_Orders);
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
