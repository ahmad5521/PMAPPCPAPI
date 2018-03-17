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
    public class VoltageLevelsController : Controller
    {
        private DATA db = new DATA();

        // GET: VoltageLevels
        public ActionResult Index()
        {
            return View(db.VoltageLevels.ToList());
        }

        // GET: VoltageLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoltageLevel voltageLevel = db.VoltageLevels.Find(id);
            if (voltageLevel == null)
            {
                return HttpNotFound();
            }
            return View(voltageLevel);
        }

        // GET: VoltageLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoltageLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Notes")] VoltageLevel voltageLevel)
        {
            if (ModelState.IsValid)
            {
                db.VoltageLevels.Add(voltageLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voltageLevel);
        }

        // GET: VoltageLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoltageLevel voltageLevel = db.VoltageLevels.Find(id);
            if (voltageLevel == null)
            {
                return HttpNotFound();
            }
            return View(voltageLevel);
        }

        // POST: VoltageLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Notes")] VoltageLevel voltageLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voltageLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voltageLevel);
        }

        // GET: VoltageLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoltageLevel voltageLevel = db.VoltageLevels.Find(id);
            if (voltageLevel == null)
            {
                return HttpNotFound();
            }
            return View(voltageLevel);
        }

        // POST: VoltageLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoltageLevel voltageLevel = db.VoltageLevels.Find(id);
            db.VoltageLevels.Remove(voltageLevel);
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
