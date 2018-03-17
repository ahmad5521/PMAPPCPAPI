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
    public class ParametersUnitTypesController : Controller
    {
        private DATA db = new DATA();

        // GET: ParametersUnitTypes
        public ActionResult Index()
        {
            return View(db.ParametersUnitTypes.ToList());
        }

        // GET: ParametersUnitTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParametersUnitType parametersUnitType = db.ParametersUnitTypes.Find(id);
            if (parametersUnitType == null)
            {
                return HttpNotFound();
            }
            return View(parametersUnitType);
        }

        // GET: ParametersUnitTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParametersUnitTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,unitType,unitMesurment,Notes,Distance")] ParametersUnitType parametersUnitType)
        {
            if (ModelState.IsValid)
            {
                db.ParametersUnitTypes.Add(parametersUnitType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parametersUnitType);
        }

        // GET: ParametersUnitTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParametersUnitType parametersUnitType = db.ParametersUnitTypes.Find(id);
            if (parametersUnitType == null)
            {
                return HttpNotFound();
            }
            return View(parametersUnitType);
        }

        // POST: ParametersUnitTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,unitType,unitMesurment,Notes,Distance")] ParametersUnitType parametersUnitType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parametersUnitType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parametersUnitType);
        }

        // GET: ParametersUnitTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParametersUnitType parametersUnitType = db.ParametersUnitTypes.Find(id);
            if (parametersUnitType == null)
            {
                return HttpNotFound();
            }
            return View(parametersUnitType);
        }

        // POST: ParametersUnitTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParametersUnitType parametersUnitType = db.ParametersUnitTypes.Find(id);
            db.ParametersUnitTypes.Remove(parametersUnitType);
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
