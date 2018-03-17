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
    public class ParemeterCalcGroupsController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/ParemeterCalcGroups/GetParemeterCalcGroups
        public IQueryable<ParemeterCalcGroup> GetParemeterCalcGroups()
        {
            return db.ParemeterCalcGroups;
        }

        // GET: api/ParemeterCalcGroups/5
        [ResponseType(typeof(ParemeterCalcGroup))]
        public IHttpActionResult GetParemeterCalcGroup(int id)
        {
            ParemeterCalcGroup paremeterCalcGroup = db.ParemeterCalcGroups.Find(id);
            if (paremeterCalcGroup == null)
            {
                return NotFound();
            }

            return Ok(paremeterCalcGroup);
        }

        // PUT: api/ParemeterCalcGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParemeterCalcGroup(int id, ParemeterCalcGroup paremeterCalcGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paremeterCalcGroup.ID)
            {
                return BadRequest();
            }

            db.Entry(paremeterCalcGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParemeterCalcGroupExists(id))
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

        // POST: api/ParemeterCalcGroups
        [ResponseType(typeof(ParemeterCalcGroup))]
        public IHttpActionResult PostParemeterCalcGroup(ParemeterCalcGroup paremeterCalcGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParemeterCalcGroups.Add(paremeterCalcGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = paremeterCalcGroup.ID }, paremeterCalcGroup);
        }

        // DELETE: api/ParemeterCalcGroups/5
        [ResponseType(typeof(ParemeterCalcGroup))]
        public IHttpActionResult DeleteParemeterCalcGroup(int id)
        {
            ParemeterCalcGroup paremeterCalcGroup = db.ParemeterCalcGroups.Find(id);
            if (paremeterCalcGroup == null)
            {
                return NotFound();
            }

            db.ParemeterCalcGroups.Remove(paremeterCalcGroup);
            db.SaveChanges();

            return Ok(paremeterCalcGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParemeterCalcGroupExists(int id)
        {
            return db.ParemeterCalcGroups.Count(e => e.ID == id) > 0;
        }
    }
}