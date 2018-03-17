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
using System.Threading;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace PMAPI.Controllers
{
    [BasicAuthentication]
    public class WorkOrdersParametersController : ApiController
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["SERVER"].ConnectionString;

        private DATA db = new DATA();

        // GET: api/WorkOrdersParameters/GetWorkOrdersParameters
        public IQueryable<WorkOrdersParameter> GetWorkOrdersParameters()
        {
            return db.WorkOrdersParameters;
        }

        // GET: api/WorkOrdersParameters/5
        [ResponseType(typeof(WorkOrdersParameter))]
        public IHttpActionResult GetWorkOrdersParameter(int id)
        {
            WorkOrdersParameter workOrdersParameter = db.WorkOrdersParameters.Find(id);
            if (workOrdersParameter == null)
            {
                return NotFound();
            }

            return Ok(workOrdersParameter);
        }

        // PUT: api/WorkOrdersParameters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkOrdersParameter(int id, WorkOrdersParameter workOrdersParameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOrdersParameter.ID)
            {
                return BadRequest();
            }

            db.Entry(workOrdersParameter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                runHMZPrusedure(workOrdersParameter);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrdersParameterExists(id))
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

        private void runHMZPrusedure(WorkOrdersParameter wop)
        {
            int userID = db.Users.First(p => p.userName == Thread.CurrentPrincipal.Identity.Name).ID;

            string a1, a2, a3;


            if(wop.isDone == null)
            {
                a1 = "NULL";
            }
            else
            {
                a1 = wop.isDone.ToString();
            }
            if (wop.DoneLength == null)
            {
                a2 = "NULL";
            }
            else
            {
                a2 = wop.DoneLength.ToString();
            }
            if (wop.DoneAmount == null)
            {
                a3 = "NULL";
            }
            else
            {
                a3 = wop.DoneAmount.ToString();
            }


            string updateSQL;

            updateSQL = "Exec PMDB.dbo.InsertIntoWorkordersParametersActions @userID_FK=" + userID
                        + " ,@workordersParametersID_FK=" + wop.ID
                        + " ,@isDone=" + a1
                        + " ,@DoneLength=" + a2
                        + " ,@DoneAmount=" + a3;

            //updateSQL = "SELECT * FROM dbo.[WorkOrderStatus]";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(updateSQL, con);
            //cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception err)
            {
                var a = err.Message;
            }
        }

        // POST: api/WorkOrdersParameters/PostWorkOrdersParameter
        [ResponseType(typeof(WorkOrdersParameter))]
        public IHttpActionResult PostWorkOrdersParameter(List<WorkOrdersParameter> workOrdersParameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var item in workOrdersParameters)
            {
                db.WorkOrdersParameters.Add(item);
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workOrdersParameters.Last().ID}, workOrdersParameters.Last());
        }

        // DELETE: api/WorkOrdersParameters/5
        [ResponseType(typeof(WorkOrdersParameter))]
        public IHttpActionResult DeleteWorkOrdersParameter(int id)
        {
            WorkOrdersParameter workOrdersParameter = db.WorkOrdersParameters.Find(id);
            if (workOrdersParameter == null)
            {
                return NotFound();
            }

            db.WorkOrdersParameters.Remove(workOrdersParameter);
            db.SaveChanges();

            return Ok(workOrdersParameter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrdersParameterExists(int id)
        {
            return db.WorkOrdersParameters.Count(e => e.ID == id) > 0;
        }
    }
}