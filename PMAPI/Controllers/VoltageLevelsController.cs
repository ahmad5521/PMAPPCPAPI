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
    public class VoltageLevelsController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/VoltageLevels/GetVoltageLevels
        public IQueryable<VoltageLevel> GetVoltageLevels()
        {
            return db.VoltageLevels;
        }

        // GET: api/VoltageLevels/5
        [ResponseType(typeof(VoltageLevel))]
        public IHttpActionResult GetVoltageLevel(int id)
        {
            VoltageLevel voltageLevel = db.VoltageLevels.Find(id);
            if (voltageLevel == null)
            {
                return NotFound();
            }

            return Ok(voltageLevel);
        }

        // PUT: api/VoltageLevels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVoltageLevel(int id, VoltageLevel voltageLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voltageLevel.ID)
            {
                return BadRequest();
            }

            db.Entry(voltageLevel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoltageLevelExists(id))
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

        // POST: api/VoltageLevels
        [ResponseType(typeof(VoltageLevel))]
        public IHttpActionResult PostVoltageLevel(VoltageLevel voltageLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VoltageLevels.Add(voltageLevel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = voltageLevel.ID }, voltageLevel);
        }

        // DELETE: api/VoltageLevels/5
        [ResponseType(typeof(VoltageLevel))]
        public IHttpActionResult DeleteVoltageLevel(int id)
        {
            VoltageLevel voltageLevel = db.VoltageLevels.Find(id);
            if (voltageLevel == null)
            {
                return NotFound();
            }

            db.VoltageLevels.Remove(voltageLevel);
            db.SaveChanges();

            return Ok(voltageLevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoltageLevelExists(int id)
        {
            return db.VoltageLevels.Count(e => e.ID == id) > 0;
        }
    }
}