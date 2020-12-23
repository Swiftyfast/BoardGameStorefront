using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class InventoryStatusController : Controller
    {
        private BoardGameStoreEntities db = new BoardGameStoreEntities();

        // GET: InventoryStatus
        public ActionResult Index()
        {
            return View(db.InventoryStatus.ToList());
        }

        // GET: InventoryStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryStatu inventoryStatu = db.InventoryStatus.Find(id);
            if (inventoryStatu == null)
            {
                return HttpNotFound();
            }
            return View(inventoryStatu);
        }

        // GET: InventoryStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventoryID,InventoryName")] InventoryStatu inventoryStatu)
        {
            if (ModelState.IsValid)
            {
                db.InventoryStatus.Add(inventoryStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventoryStatu);
        }

        // GET: InventoryStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryStatu inventoryStatu = db.InventoryStatus.Find(id);
            if (inventoryStatu == null)
            {
                return HttpNotFound();
            }
            return View(inventoryStatu);
        }

        // POST: InventoryStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryID,InventoryName")] InventoryStatu inventoryStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventoryStatu);
        }

        // GET: InventoryStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryStatu inventoryStatu = db.InventoryStatus.Find(id);
            if (inventoryStatu == null)
            {
                return HttpNotFound();
            }
            return View(inventoryStatu);
        }

        // POST: InventoryStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventoryStatu inventoryStatu = db.InventoryStatus.Find(id);
            db.InventoryStatus.Remove(inventoryStatu);
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
