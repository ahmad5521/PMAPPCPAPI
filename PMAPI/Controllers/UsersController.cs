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
    //[BasicAuthentication]
    public class UsersController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/Users/GetUsers
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [Route("api/Users/GetUserByUserNameAndPassword/{username}/{password}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserByUserNameAndPassword(string username, string password)
        {
            User user = db.Users.Where(u => u.userName.Equals(username) && u.Password.Equals(password)).First();
            if (user == null)
            {
                return NotFound();
            }
            if (user.SuperVisor == null)
                user.role = "head";
            else
            if (isHeSuperVisor(user.ID))
                user.role = "supervisor";
            else
                user.role = "normal";

            return Ok(user);
        }

        private bool isHeSuperVisor(int iD)
        {
            var emps = db.Users.Where(u => u.SuperVisor == iD);
            if (emps.Count() == 0)
                return false;
            else
                return true;
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}