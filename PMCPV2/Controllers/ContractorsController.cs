﻿using System;
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
    public class ContractorsController : Controller
    {
        private DATA db = new DATA();

        // GET: Contractors
        public ActionResult Index()
        {
            return View(db.Contractors.ToList());
        }

        // GET: Contractors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // GET: Contractors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,contactName,contactMobile,contactEmail,Notes")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Contractors.Add(contractor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,contactName,contactMobile,contactEmail,Notes")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contractor contractor = db.Contractors.Find(id);
            db.Contractors.Remove(contractor);
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
