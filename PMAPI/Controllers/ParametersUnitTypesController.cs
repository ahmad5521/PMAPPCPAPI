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
    public class ParametersUnitTypesController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/ParametersUnitTypes/GetParametersUnitTypes
        public IQueryable<ParametersUnitType> GetParametersUnitTypes()
        {
            return db.ParametersUnitTypes;
        }

        // GET: api/ParametersUnitTypes/5
        [ResponseType(typeof(ParametersUnitType))]
        public IHttpActionResult GetParametersUnitType(int id)
        {
            ParametersUnitType parametersUnitType = db.ParametersUnitTypes.Find(id);
            if (parametersUnitType == null)
            {
                return NotFound();
            }

            return Ok(parametersUnitType);
        }

        // PUT: api/ParametersUnitTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParametersUnitType(int id, ParametersUnitType parametersUnitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parametersUnitType.ID)
            {
                return BadRequest();
            }

            db.Entry(parametersUnitType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametersUnitTypeExists(id))
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

        // POST: api/ParametersUnitTypes
        [ResponseType(typeof(ParametersUnitType))]
        public IHttpActionResult PostParametersUnitType(ParametersUnitType parametersUnitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParametersUnitTypes.Add(parametersUnitType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parametersUnitType.ID }, parametersUnitType);
        }

        // DELETE: api/ParametersUnitTypes/5
        [ResponseType(typeof(ParametersUnitType))]
        public IHttpActionResult DeleteParametersUnitType(int id)
        {
            ParametersUnitType parametersUnitType = db.ParametersUnitTypes.Find(id);
            if (parametersUnitType == null)
            {
                return NotFound();
            }

            db.ParametersUnitTypes.Remove(parametersUnitType);
            db.SaveChanges();

            return Ok(parametersUnitType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParametersUnitTypeExists(int id)
        {
            return db.ParametersUnitTypes.Count(e => e.ID == id) > 0;
        }
    }
}