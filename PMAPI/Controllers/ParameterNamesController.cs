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
    public class ParameterNamesController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/ParameterNames/GetParameterNames
        public IQueryable<ParameterName> GetParameterNames()
        {
            return db.ParameterNames;
        }

        // GET: api/ParameterNames/5
        [ResponseType(typeof(ParameterName))]
        public IHttpActionResult GetParameterName(int id)
        {
            ParameterName parameterName = db.ParameterNames.Find(id);
            if (parameterName == null)
            {
                return NotFound();
            }

            return Ok(parameterName);
        }

        // PUT: api/ParameterNames/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParameterName(int id, ParameterName parameterName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parameterName.parameterNameID)
            {
                return BadRequest();
            }

            db.Entry(parameterName).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParameterNameExists(id))
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

        // POST: api/ParameterNames
        [ResponseType(typeof(ParameterName))]
        public IHttpActionResult PostParameterName(ParameterName parameterName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParameterNames.Add(parameterName);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parameterName.parameterNameID }, parameterName);
        }

        // DELETE: api/ParameterNames/5
        [ResponseType(typeof(ParameterName))]
        public IHttpActionResult DeleteParameterName(int id)
        {
            ParameterName parameterName = db.ParameterNames.Find(id);
            if (parameterName == null)
            {
                return NotFound();
            }

            db.ParameterNames.Remove(parameterName);
            db.SaveChanges();

            return Ok(parameterName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParameterNameExists(int id)
        {
            return db.ParameterNames.Count(e => e.parameterNameID == id) > 0;
        }
    }
}