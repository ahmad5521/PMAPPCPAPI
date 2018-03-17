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
    public class ParametersController : Controller
    {
        private DATA db = new DATA();

        // GET: Parameters
        public ActionResult Index()
        {
            var parameters = db.Parameters.Include(p => p.ParameterCategory).Include(p => p.ParameterName).Include(p => p.ParametersUnitType).Include(p => p.ParemeterCalcGroup).OrderByDescending(p => p.parameterCategory_FKID);
            return View(parameters.ToList());
        }

        // GET: Parameters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parameter parameter = db.Parameters.Find(id);
            if (parameter == null)
            {
                return HttpNotFound();
            }
            return View(parameter);
        }

        // GET: Parameters/Create
        public ActionResult Create()
        {
            ViewBag.parameterCategory_FKID = new SelectList(db.ParameterCategories, "ID", "Name");
            ViewBag.parameterNameID_FK = new SelectList(db.ParameterNames, "parameterNameID", "parameterNameName");
            ViewBag.parameterUnitType_FKID = new SelectList(db.ParametersUnitTypes, "ID", "unitType");
            ViewBag.parameterCalcGroup_FKID = new SelectList(db.ParemeterCalcGroups, "ID", "Name");
            return View();
        }

        // POST: Parameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,parameterCategory_FKID,parameterCalcGroup_FKID,ParameterWight,parameterUnitType_FKID,networkType_FKID,isActive,isRquired,parameterNameID_FK")] Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                db.Parameters.Add(parameter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.parameterCategory_FKID = new SelectList(db.ParameterCategories, "ID", "Name", parameter.parameterCategory_FKID);
            ViewBag.parameterNameID_FK = new SelectList(db.ParameterNames, "parameterNameID", "parameterNameName", parameter.parameterNameID_FK);
            ViewBag.parameterUnitType_FKID = new SelectList(db.ParametersUnitTypes, "ID", "unitType", parameter.parameterUnitType_FKID);
            ViewBag.parameterCalcGroup_FKID = new SelectList(db.ParemeterCalcGroups, "ID", "Name", parameter.parameterCalcGroup_FKID);
            return View(parameter);
        }

        // GET: Parameters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parameter parameter = db.Parameters.Find(id);
            if (parameter == null)
            {
                return HttpNotFound();
            }
            ViewBag.parameterCategory_FKID = new SelectList(db.ParameterCategories, "ID", "Name", parameter.parameterCategory_FKID);
            ViewBag.parameterNameID_FK = new SelectList(db.ParameterNames, "parameterNameID", "parameterNameName", parameter.parameterNameID_FK);
            ViewBag.parameterUnitType_FKID = new SelectList(db.ParametersUnitTypes, "ID", "unitType", parameter.parameterUnitType_FKID);
            ViewBag.parameterCalcGroup_FKID = new SelectList(db.ParemeterCalcGroups, "ID", "Name", parameter.parameterCalcGroup_FKID);
            return View(parameter);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,parameterCategory_FKID,parameterCalcGroup_FKID,ParameterWight,parameterUnitType_FKID,networkType_FKID,isActive,isRquired,parameterNameID_FK")] Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parameter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.parameterCategory_FKID = new SelectList(db.ParameterCategories, "ID", "Name", parameter.parameterCategory_FKID);
            ViewBag.parameterNameID_FK = new SelectList(db.ParameterNames, "parameterNameID", "parameterNameName", parameter.parameterNameID_FK);
            ViewBag.parameterUnitType_FKID = new SelectList(db.ParametersUnitTypes, "ID", "unitType", parameter.parameterUnitType_FKID);
            ViewBag.parameterCalcGroup_FKID = new SelectList(db.ParemeterCalcGroups, "ID", "Name", parameter.parameterCalcGroup_FKID);
            return View(parameter);
        }

        // GET: Parameters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parameter parameter = db.Parameters.Find(id);
            if (parameter == null)
            {
                return HttpNotFound();
            }
            return View(parameter);
        }

        // POST: Parameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parameter parameter = db.Parameters.Find(id);
            db.Parameters.Remove(parameter);
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
