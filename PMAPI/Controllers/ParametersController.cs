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
using System.Data.SqlClient;

namespace PMAPI.Controllers
{
    [BasicAuthentication]
    public class ParametersController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/Parameters/GetParameters
        public IQueryable<Parameter> GetParameters()
        {


            return db.Parameters;
        }


        // GET: api/Parameters/GetParameterbYCatagoryNO/5
        [ResponseType(typeof(Parameter))]
        public IHttpActionResult GetParameterbYCatagoryNO(int id)
        {
            List<Parameter> parameter = db.Parameters.Where(p => p.parameterCategory_FKID == id).ToList<Parameter>();
            if (parameter == null)
            {
                return NotFound();
            }

            return Ok(parameter);
        }

        // PUT: api/Parameters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParameter(int id, Parameter parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parameter.ID)
            {
                return BadRequest();
            }

            db.Entry(parameter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParameterExists(id))
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

        // POST: api/Parameters
        [ResponseType(typeof(Parameter))]
        public IHttpActionResult PostParameter(Parameter parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parameters.Add(parameter);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parameter.ID }, parameter);
        }

        // DELETE: api/Parameters/5
        [ResponseType(typeof(Parameter))]
        public IHttpActionResult DeleteParameter(int id)
        {
            Parameter parameter = db.Parameters.Find(id);
            if (parameter == null)
            {
                return NotFound();
            }

            db.Parameters.Remove(parameter);
            db.SaveChanges();

            return Ok(parameter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParameterExists(int id)
        {
            return db.Parameters.Count(e => e.ID == id) > 0;
        }
    }
}