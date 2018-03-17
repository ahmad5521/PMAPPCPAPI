using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMCPV2.Models;

namespace PMCPV2.Controllers
{
    public class ParameterCategoriesController : Controller
    {
        private DATA db = new DATA();

        // GET: ParameterCategories
        public ActionResult Index()
        {
            var parameterCategories = db.ParameterCategories.Include(p => p.WorkorderType);
            return View(parameterCategories.ToList());
        }

        // GET: ParameterCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParameterCategory parameterCategory = db.ParameterCategories.Find(id);
            if (parameterCategory == null)
            {
                return HttpNotFound();
            }
            return View(parameterCategory);
        }

        // GET: ParameterCategories/Create
        public ActionResult Create()
        {
            ViewBag.workorderTypeID_FK = new SelectList(db.WorkorderTypes, "workorderTypeID", "workorderTypeName");
            return View();
        }

        // POST: ParameterCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Notes,workorderTypeID_FK")] ParameterCategory parameterCategory)
        {
            if (ModelState.IsValid)
            {
                db.ParameterCategories.Add(parameterCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.workorderTypeID_FK = new SelectList(db.WorkorderTypes, "workorderTypeID", "workorderTypeName", parameterCategory.workorderTypeID_FK);
            return View(parameterCategory);
        }

        // GET: ParameterCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParameterCategory parameterCategory = db.ParameterCategories.Find(id);
            if (parameterCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.workorderTypeID_FK = new SelectList(db.WorkorderTypes, "workorderTypeID", "workorderTypeName", parameterCategory.workorderTypeID_FK);
            return View(parameterCategory);
        }

        // POST: ParameterCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Notes,workorderTypeID_FK")] ParameterCategory parameterCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parameterCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.workorderTypeID_FK = new SelectList(db.WorkorderTypes, "workorderTypeID", "workorderTypeName", parameterCategory.workorderTypeID_FK);
            return View(parameterCategory);
        }

        // GET: ParameterCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParameterCategory parameterCategory = db.ParameterCategories.Find(id);
            if (parameterCategory == null)
            {
                return HttpNotFound();
            }
            return View(parameterCategory);
        }

        // POST: ParameterCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParameterCategory parameterCategory = db.ParameterCategories.Find(id);
            db.ParameterCategories.Remove(parameterCategory);
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
