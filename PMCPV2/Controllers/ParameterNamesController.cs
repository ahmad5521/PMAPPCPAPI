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
    [Authorize]
    public class ParameterNamesController : Controller
    {
        private DATA db = new DATA();

        // GET: ParameterNames
        public ActionResult Index()
        {
            return View(db.ParameterNames.ToList());
        }

        // GET: ParameterNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParameterName parameterName = db.ParameterNames.Find(id);
            if (parameterName == null)
            {
                return HttpNotFound();
            }
            return View(parameterName);
        }

        // GET: ParameterNames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParameterNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "parameterNameID,parameterNameName")] ParameterName parameterName)
        {
            if (ModelState.IsValid)
            {
                db.ParameterNames.Add(parameterName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parameterName);
        }

        // GET: ParameterNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParameterName parameterName = db.ParameterNames.Find(id);
            if (parameterName == null)
            {
                return HttpNotFound();
            }
            return View(parameterName);
        }

        // POST: ParameterNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "parameterNameID,parameterNameName")] ParameterName parameterName)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parameterName).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parameterName);
        }

        // GET: ParameterNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParameterName parameterName = db.ParameterNames.Find(id);
            if (parameterName == null)
            {
                return HttpNotFound();
            }
            return View(parameterName);
        }

        // POST: ParameterNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParameterName parameterName = db.ParameterNames.Find(id);
            db.ParameterNames.Remove(parameterName);
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
