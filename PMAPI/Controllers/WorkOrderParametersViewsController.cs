using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PMAPI.Models;
using PMAPI.Functions;

namespace PMAPI.Controllers
{
    [BasicAuthentication]
    public class WorkOrderParametersViewsController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/WorkOrderParametersViews/GetWorkOrderParametersViews
        public IQueryable<WorkOrderParametersView> GetWorkOrderParametersViews()
        {
            return db.WorkOrderParametersViews;
        }

        // GET: api/WorkOrderParametersViews/GetWorkOrderParametersByWONoView/5
        [ResponseType(typeof(List<WorkOrderParametersView>))]
        public IHttpActionResult GetWorkOrderParametersByWONoView(int id)
        {
            List<WorkOrderParametersView> workOrderParametersView =
                (from p in db.WorkOrderParametersViews
                 where p.workOrder_FKID == id
                 select p).ToList<WorkOrderParametersView>();
            if (workOrderParametersView == null)
            {
                return NotFound();
            }

            return Ok(workOrderParametersView);
        }

        // GET: api/WorkOrderParametersViews/GetWorkOrderParametersView/5
        [ResponseType(typeof(WorkOrderParametersView))]
        public IHttpActionResult GetWorkOrderParametersView(int id)
        {
            //WorkOrderParametersView workOrderParametersView = db.WorkOrderParametersViews.Find(id);
            WorkOrderParametersView workOrderParametersView = db.WorkOrderParametersViews.SingleOrDefault(m => m.wopID == id);
            if (workOrderParametersView == null)
            {
                return NotFound();
            }

            return Ok(workOrderParametersView);
        }

        // PUT: api/WorkOrderParametersViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkOrderParametersView(int id, WorkOrderParametersView workOrderParametersView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOrderParametersView.wopID)
            {
                return BadRequest();
            }

            db.Entry(workOrderParametersView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderParametersViewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WorkOrderParametersViews
        [ResponseType(typeof(WorkOrderParametersView))]
        public IHttpActionResult PostWorkOrderParametersView(WorkOrderParametersView workOrderParametersView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkOrderParametersViews.Add(workOrderParametersView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WorkOrderParametersViewExists(workOrderParametersView.wopID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = workOrderParametersView.wopID }, workOrderParametersView);
        }

        // DELETE: api/WorkOrderParametersViews/5
        [ResponseType(typeof(WorkOrderParametersView))]
        public IHttpActionResult DeleteWorkOrderParametersView(int id)
        {
            WorkOrderParametersView workOrderParametersView = db.WorkOrderParametersViews.Find(id);
            if (workOrderParametersView == null)
            {
                return NotFound();
            }

            db.WorkOrderParametersViews.Remove(workOrderParametersView);
            db.SaveChanges();

            return Ok(workOrderParametersView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrderParametersViewExists(int id)
        {
            return db.WorkOrderParametersViews.Count(e => e.wopID == id) > 0;
        }
    }
}