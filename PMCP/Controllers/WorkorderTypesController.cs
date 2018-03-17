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
    public class WorkorderTypesController : Controller
    {
        private DATA db = new DATA();

        // GET: WorkorderTypes
        public ActionResult Index()
        {
            return View(db.WorkorderTypes.ToList());
        }

        // GET: WorkorderTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkorderType workorderType = db.WorkorderTypes.Find(id);
            if (workorderType == null)
            {
                return HttpNotFound();
            }
            return View(workorderType);
        }

        // GET: WorkorderTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkorderTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "workorderTypeID,workorderTypeName")] WorkorderType workorderType)
        {
            if (ModelState.IsValid)
            {
                db.WorkorderTypes.Add(workorderType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workorderType);
        }

        // GET: WorkorderTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkorderType workorderType = db.WorkorderTypes.Find(id);
            if (workorderType == null)
            {
                return HttpNotFound();
            }
            return View(workorderType);
        }

        // POST: WorkorderTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "workorderTypeID,workorderTypeName")] WorkorderType workorderType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workorderType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workorderType);
        }

        // GET: WorkorderTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkorderType workorderType = db.WorkorderTypes.Find(id);
            if (workorderType == null)
            {
                return HttpNotFound();
            }
            return View(workorderType);
        }

        // POST: WorkorderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkorderType workorderType = db.WorkorderTypes.Find(id);
            db.WorkorderTypes.Remove(workorderType);
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
