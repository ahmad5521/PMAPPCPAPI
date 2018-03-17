using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMCP2.Models;

namespace PMCP2.Controllers
{
    public class WorkOrderStatusController : Controller
    {
        private DATA db = new DATA();

        // GET: WorkOrderStatus
        public ActionResult Index()
        {
            return View(db.WorkOrderStatus.ToList());
        }

        // GET: WorkOrderStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderStatu workOrderStatu = db.WorkOrderStatus.Find(id);
            if (workOrderStatu == null)
            {
                return HttpNotFound();
            }
            return View(workOrderStatu);
        }

        // GET: WorkOrderStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkOrderStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Notes")] WorkOrderStatu workOrderStatu)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrderStatus.Add(workOrderStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workOrderStatu);
        }

        // GET: WorkOrderStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderStatu workOrderStatu = db.WorkOrderStatus.Find(id);
            if (workOrderStatu == null)
            {
                return HttpNotFound();
            }
            return View(workOrderStatu);
        }

        // POST: WorkOrderStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Notes")] WorkOrderStatu workOrderStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrderStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workOrderStatu);
        }

        // GET: WorkOrderStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderStatu workOrderStatu = db.WorkOrderStatus.Find(id);
            if (workOrderStatu == null)
            {
                return HttpNotFound();
            }
            return View(workOrderStatu);
        }

        // POST: WorkOrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrderStatu workOrderStatu = db.WorkOrderStatus.Find(id);
            db.WorkOrderStatus.Remove(workOrderStatu);
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
