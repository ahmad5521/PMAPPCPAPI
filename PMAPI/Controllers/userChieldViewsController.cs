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
    public class userChieldViewsController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/userChieldViews/GetuserChieldViews
        public IQueryable<userChieldView> GetuserChieldViews()
        {
            return db.userChieldViews;
        }

        // GET: api/userChieldViews/5
        [ResponseType(typeof(userChieldView))]
        public IHttpActionResult GetuserChieldView(int id)
        {
            userChieldView userChieldView = db.userChieldViews.Find(id);
            if (userChieldView == null)
            {
                return NotFound();
            }

            return Ok(userChieldView);
        }

        // PUT: api/userChieldViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutuserChieldView(int id, userChieldView userChieldView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userChieldView.userID)
            {
                return BadRequest();
            }

            db.Entry(userChieldView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userChieldViewExists(id))
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

        // POST: api/userChieldViews
        [ResponseType(typeof(userChieldView))]
        public IHttpActionResult PostuserChieldView(userChieldView userChieldView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.userChieldViews.Add(userChieldView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (userChieldViewExists(userChieldView.userID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userChieldView.userID }, userChieldView);
        }

        // DELETE: api/userChieldViews/5
        [ResponseType(typeof(userChieldView))]
        public IHttpActionResult DeleteuserChieldView(int id)
        {
            userChieldView userChieldView = db.userChieldViews.Find(id);
            if (userChieldView == null)
            {
                return NotFound();
            }

            db.userChieldViews.Remove(userChieldView);
            db.SaveChanges();

            return Ok(userChieldView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userChieldViewExists(int id)
        {
            return db.userChieldViews.Count(e => e.userID == id) > 0;
        }
    }
}