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
    public class WorkOrdersController : Controller
    {
        private DATA db = new DATA();

        // GET: WorkOrders
        public ActionResult Index()
        {
            var workOrders = db.WorkOrders.Include(w => w.Contractor).Include(w => w.Location).Include(w => w.ParameterCategory).Include(w => w.Project).Include(w => w.User).Include(w => w.VoltageLevel).Include(w => w.WorkOrderStatu);
            return View(workOrders.ToList());
        }

        // GET: WorkOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public ActionResult Create()
        {
            ViewBag.contractors_FKID = new SelectList(db.Contractors, "ID", "Name");
            ViewBag.location_FKID = new SelectList(db.Locations, "ID", "Name");
            ViewBag.ParameterCategory_FKID = new SelectList(db.ParameterCategories, "ID", "Name");
            ViewBag.project_FKID = new SelectList(db.Projects, "ID", "Name");
            ViewBag.user_FKID = new SelectList(db.Users, "ID", "Name");
            ViewBag.voltageLevels_FKID = new SelectList(db.VoltageLevels, "ID", "Name");
            ViewBag.workOrderStatus_FKID = new SelectList(db.WorkOrderStatus, "ID", "Name");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Number,project_FKID,location_FKID,contractors_FKID,ParameterCategory_FKID,totalLenght,voltageLevels_FKID,startingDate,maxDueDays,dateTimeStamp,workOrderStatus_FKID,user_FKID,value")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.contractors_FKID = new SelectList(db.Contractors, "ID", "Name", workOrder.contractors_FKID);
            ViewBag.location_FKID = new SelectList(db.Locations, "ID", "Name", workOrder.location_FKID);
            ViewBag.ParameterCategory_FKID = new SelectList(db.ParameterCategories, "ID", "Name", workOrder.ParameterCategory_FKID);
            ViewBag.project_FKID = new SelectList(db.Projects, "ID", "Name", workOrder.project_FKID);
            ViewBag.user_FKID = new SelectList(db.Users, "ID", "Name", workOrder.user_FKID);
            ViewBag.voltageLevels_FKID = new SelectList(db.VoltageLevels, "ID", "Name", workOrder.voltageLevels_FKID);
            ViewBag.workOrderStatus_FKID = new SelectList(db.WorkOrderStatus, "ID", "Name", workOrder.workOrderStatus_FKID);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.contractors_FKID = new SelectList(db.Contractors, "ID", "Name", workOrder.contractors_FKID);
            ViewBag.location_FKID = new SelectList(db.Locations, "ID", "Name", workOrder.location_FKID);
            ViewBag.ParameterCategory_FKID = new SelectList(db.ParameterCategories, "ID", "Name", workOrder.ParameterCategory_FKID);
            ViewBag.project_FKID = new SelectList(db.Projects, "ID", "Name", workOrder.project_FKID);
            ViewBag.user_FKID = new SelectList(db.Users, "ID", "Name", workOrder.user_FKID);
            ViewBag.voltageLevels_FKID = new SelectList(db.VoltageLevels, "ID", "Name", workOrder.voltageLevels_FKID);
            ViewBag.workOrderStatus_FKID = new SelectList(db.WorkOrderStatus, "ID", "Name", workOrder.workOrderStatus_FKID);
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Number,project_FKID,location_FKID,contractors_FKID,ParameterCategory_FKID,totalLenght,voltageLevels_FKID,startingDate,maxDueDays,dateTimeStamp,workOrderStatus_FKID,user_FKID,value")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.contractors_FKID = new SelectList(db.Contractors, "ID", "Name", workOrder.contractors_FKID);
            ViewBag.location_FKID = new SelectList(db.Locations, "ID", "Name", workOrder.location_FKID);
            ViewBag.ParameterCategory_FKID = new SelectList(db.ParameterCategories, "ID", "Name", workOrder.ParameterCategory_FKID);
            ViewBag.project_FKID = new SelectList(db.Projects, "ID", "Name", workOrder.project_FKID);
            ViewBag.user_FKID = new SelectList(db.Users, "ID", "Name", workOrder.user_FKID);
            ViewBag.voltageLevels_FKID = new SelectList(db.VoltageLevels, "ID", "Name", workOrder.voltageLevels_FKID);
            ViewBag.workOrderStatus_FKID = new SelectList(db.WorkOrderStatus, "ID", "Name", workOrder.workOrderStatus_FKID);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
            db.WorkOrders.Remove(workOrder);
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
