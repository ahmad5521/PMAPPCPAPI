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
    public class WorkorderTypesController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/WorkorderTypes/GetWorkorderTypes
        public IQueryable<WorkorderType> GetWorkorderTypes()
        {
            return db.WorkorderTypes;
        }

        // GET: api/WorkorderTypes/5
        [ResponseType(typeof(WorkorderType))]
        public IHttpActionResult GetWorkorderType(int id)
        {
            WorkorderType workorderType = db.WorkorderTypes.Find(id);
            if (workorderType == null)
            {
                return NotFound();
            }

            return Ok(workorderType);
        }

        // PUT: api/WorkorderTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkorderType(int id, WorkorderType workorderType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workorderType.workorderTypeID)
            {
                return BadRequest();
            }

            db.Entry(workorderType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkorderTypeExists(id))
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

        // POST: api/WorkorderTypes
        [ResponseType(typeof(WorkorderType))]
        public IHttpActionResult PostWorkorderType(WorkorderType workorderType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkorderTypes.Add(workorderType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workorderType.workorderTypeID }, workorderType);
        }

        // DELETE: api/WorkorderTypes/5
        [ResponseType(typeof(WorkorderType))]
        public IHttpActionResult DeleteWorkorderType(int id)
        {
            WorkorderType workorderType = db.WorkorderTypes.Find(id);
            if (workorderType == null)
            {
                return NotFound();
            }

            db.WorkorderTypes.Remove(workorderType);
            db.SaveChanges();

            return Ok(workorderType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkorderTypeExists(int id)
        {
            return db.WorkorderTypes.Count(e => e.workorderTypeID == id) > 0;
        }
    }
}