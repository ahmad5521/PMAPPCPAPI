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
    public class WorkOrdersParametersController : Controller
    {
        private DATA db = new DATA();

        // GET: WorkOrdersParameters
        public ActionResult Index()
        {
            var workOrdersParameters = db.WorkOrdersParameters.Include(w => w.Parameter).Include(w => w.WorkOrder);
            return View(workOrdersParameters.ToList());
        }

        // GET: WorkOrdersParameters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrdersParameter workOrdersParameter = db.WorkOrdersParameters.Find(id);
            if (workOrdersParameter == null)
            {
                return HttpNotFound();
            }
            return View(workOrdersParameter);
        }

        // GET: WorkOrdersParameters/Create
        public ActionResult Create()
        {
            ViewBag.parameter_FKID = new SelectList(db.Parameters, "ID", "Name");
            ViewBag.workOrder_FKID = new SelectList(db.WorkOrders, "ID", "Number");
            return View();
        }

        // POST: WorkOrdersParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,workOrder_FKID,parameter_FKID,Lenght,Amount,isDone,lastUpdate,DoneLength,DoneAmount,weightAmount")] WorkOrdersParameter workOrdersParameter)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrdersParameters.Add(workOrdersParameter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.parameter_FKID = new SelectList(db.Parameters, "ID", "Name", workOrdersParameter.parameter_FKID);
            ViewBag.workOrder_FKID = new SelectList(db.WorkOrders, "ID", "Number", workOrdersParameter.workOrder_FKID);
            return View(workOrdersParameter);
        }

        // GET: WorkOrdersParameters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrdersParameter workOrdersParameter = db.WorkOrdersParameters.Find(id);
            if (workOrdersParameter == null)
            {
                return HttpNotFound();
            }
            ViewBag.parameter_FKID = new SelectList(db.Parameters, "ID", "Name", workOrdersParameter.parameter_FKID);
            ViewBag.workOrder_FKID = new SelectList(db.WorkOrders, "ID", "Number", workOrdersParameter.workOrder_FKID);
            return View(workOrdersParameter);
        }

        // POST: WorkOrdersParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,workOrder_FKID,parameter_FKID,Lenght,Amount,isDone,lastUpdate,DoneLength,DoneAmount,weightAmount")] WorkOrdersParameter workOrdersParameter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrdersParameter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.parameter_FKID = new SelectList(db.Parameters, "ID", "Name", workOrdersParameter.parameter_FKID);
            ViewBag.workOrder_FKID = new SelectList(db.WorkOrders, "ID", "Number", workOrdersParameter.workOrder_FKID);
            return View(workOrdersParameter);
        }

        // GET: WorkOrdersParameters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrdersParameter workOrdersParameter = db.WorkOrdersParameters.Find(id);
            if (workOrdersParameter == null)
            {
                return HttpNotFound();
            }
            return View(workOrdersParameter);
        }

        // POST: WorkOrdersParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrdersParameter workOrdersParameter = db.WorkOrdersParameters.Find(id);
            db.WorkOrdersParameters.Remove(workOrdersParameter);
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
