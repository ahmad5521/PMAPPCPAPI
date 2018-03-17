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
    public class ParameterCategoriesController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/ParameterCategories/GetParameterCategories
        public IQueryable<ParameterCategory> GetParameterCategories()
        {
            return db.ParameterCategories;
        }

        // GET: api/ParameterCategories/5
        [ResponseType(typeof(ParameterCategory))]
        public IHttpActionResult GetParameterCategory(int id)
        {
            ParameterCategory parameterCategory = db.ParameterCategories.Find(id);
            if (parameterCategory == null)
            {
                return NotFound();
            }

            return Ok(parameterCategory);
        }

        // PUT: api/ParameterCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParameterCategory(int id, ParameterCategory parameterCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parameterCategory.ID)
            {
                return BadRequest();
            }

            db.Entry(parameterCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParameterCategoryExists(id))
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

        // POST: api/ParameterCategories
        [ResponseType(typeof(ParameterCategory))]
        public IHttpActionResult PostParameterCategory(ParameterCategory parameterCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParameterCategories.Add(parameterCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parameterCategory.ID }, parameterCategory);
        }

        // DELETE: api/ParameterCategories/5
        [ResponseType(typeof(ParameterCategory))]
        public IHttpActionResult DeleteParameterCategory(int id)
        {
            ParameterCategory parameterCategory = db.ParameterCategories.Find(id);
            if (parameterCategory == null)
            {
                return NotFound();
            }

            db.ParameterCategories.Remove(parameterCategory);
            db.SaveChanges();

            return Ok(parameterCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParameterCategoryExists(int id)
        {
            return db.ParameterCategories.Count(e => e.ID == id) > 0;
        }
    }
}