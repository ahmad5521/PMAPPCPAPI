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
    public class WorkOrdersPhotoesController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/WorkOrdersPhotoes/GetWorkOrdersPhotos
        public IQueryable<WorkOrdersPhoto> GetWorkOrdersPhotos()
        {
            return db.WorkOrdersPhotos;
        }

        // GET: api/WorkOrdersPhotoes/GetWorkOrdersPhoto/5
        [ResponseType(typeof(WorkOrdersPhoto))]
        public IHttpActionResult GetWorkOrdersPhoto(int id)
        {
            List<WorkOrdersPhoto> workOrdersPhotos = db.WorkOrdersPhotos.Where(p => p.WOID_FK == id).ToList<WorkOrdersPhoto>();
            if (workOrdersPhotos == null)
            {
                return NotFound();
            }

            return Ok(workOrdersPhotos);
        }

        // PUT: api/WorkOrdersPhotoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkOrdersPhoto(int id, WorkOrdersPhoto workOrdersPhoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOrdersPhoto.id)
            {
                return BadRequest();
            }

            db.Entry(workOrdersPhoto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrdersPhotoExists(id))
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

        // POST: api/WorkOrdersPhotoes/PostWorkOrdersPhoto
        [ResponseType(typeof(WorkOrdersPhoto))]
        public IHttpActionResult PostWorkOrdersPhoto(WorkOrdersPhoto workOrdersPhoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkOrdersPhotos.Add(workOrdersPhoto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workOrdersPhoto.id }, workOrdersPhoto);
        }

        // DELETE: api/WorkOrdersPhotoes/DeleteWorkOrdersPhoto/5
        [ResponseType(typeof(WorkOrdersPhoto))]
        public IHttpActionResult DeleteWorkOrdersPhoto(int id)
        {
            WorkOrdersPhoto workOrdersPhoto = db.WorkOrdersPhotos.Find(id);
            if (workOrdersPhoto == null)
            {
                return NotFound();
            }

            db.WorkOrdersPhotos.Remove(workOrdersPhoto);
            db.SaveChanges();

            return Ok(workOrdersPhoto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrdersPhotoExists(int id)
        {
            return db.WorkOrdersPhotos.Count(e => e.id == id) > 0;
        }
    }
}