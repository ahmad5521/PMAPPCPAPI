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
    public class ParemeterCalcGroupsController : Controller
    {
        private DATA db = new DATA();

        // GET: ParemeterCalcGroups
        public ActionResult Index()
        {
            return View(db.ParemeterCalcGroups.ToList());
        }

        // GET: ParemeterCalcGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParemeterCalcGroup paremeterCalcGroup = db.ParemeterCalcGroups.Find(id);
            if (paremeterCalcGroup == null)
            {
                return HttpNotFound();
            }
            return View(paremeterCalcGroup);
        }

        // GET: ParemeterCalcGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParemeterCalcGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Notes")] ParemeterCalcGroup paremeterCalcGroup)
        {
            if (ModelState.IsValid)
            {
                db.ParemeterCalcGroups.Add(paremeterCalcGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paremeterCalcGroup);
        }

        // GET: ParemeterCalcGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParemeterCalcGroup paremeterCalcGroup = db.ParemeterCalcGroups.Find(id);
            if (paremeterCalcGroup == null)
            {
                return HttpNotFound();
            }
            return View(paremeterCalcGroup);
        }

        // POST: ParemeterCalcGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Notes")] ParemeterCalcGroup paremeterCalcGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paremeterCalcGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paremeterCalcGroup);
        }

        // GET: ParemeterCalcGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParemeterCalcGroup paremeterCalcGroup = db.ParemeterCalcGroups.Find(id);
            if (paremeterCalcGroup == null)
            {
                return HttpNotFound();
            }
            return View(paremeterCalcGroup);
        }

        // POST: ParemeterCalcGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParemeterCalcGroup paremeterCalcGroup = db.ParemeterCalcGroups.Find(id);
            db.ParemeterCalcGroups.Remove(paremeterCalcGroup);
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
