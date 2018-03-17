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
    public class WorkOrderStatusController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/WorkOrderStatus/GetWorkOrderStatus
        public IQueryable<WorkOrderStatu> GetWorkOrderStatus()
        {
            return db.WorkOrderStatus;
        }

        // GET: api/WorkOrderStatus/5
        [ResponseType(typeof(WorkOrderStatu))]
        public IHttpActionResult GetWorkOrderStatu(int id)
        {
            WorkOrderStatu workOrderStatu = db.WorkOrderStatus.Find(id);
            if (workOrderStatu == null)
            {
                return NotFound();
            }

            return Ok(workOrderStatu);
        }

        // PUT: api/WorkOrderStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkOrderStatu(int id, WorkOrderStatu workOrderStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOrderStatu.ID)
            {
                return BadRequest();
            }

            db.Entry(workOrderStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderStatuExists(id))
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

        // POST: api/WorkOrderStatus
        [ResponseType(typeof(WorkOrderStatu))]
        public IHttpActionResult PostWorkOrderStatu(WorkOrderStatu workOrderStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkOrderStatus.Add(workOrderStatu);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workOrderStatu.ID }, workOrderStatu);
        }

        // DELETE: api/WorkOrderStatus/5
        [ResponseType(typeof(WorkOrderStatu))]
        public IHttpActionResult DeleteWorkOrderStatu(int id)
        {
            WorkOrderStatu workOrderStatu = db.WorkOrderStatus.Find(id);
            if (workOrderStatu == null)
            {
                return NotFound();
            }

            db.WorkOrderStatus.Remove(workOrderStatu);
            db.SaveChanges();

            return Ok(workOrderStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrderStatuExists(int id)
        {
            return db.WorkOrderStatus.Count(e => e.ID == id) > 0;
        }
    }
}