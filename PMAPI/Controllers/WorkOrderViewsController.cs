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
using System.Web.Configuration;

namespace PMAPI.Controllers
{
    [BasicAuthentication]
    public class WorkOrderViewsController : ApiController
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["SERVER"].ConnectionString;

        private DATA db = new DATA();

        // GET: api/WorkOrderViews/GetWorkOrderViews
        public IQueryable<WorkOrderView> GetWorkOrderViews()
        {
            return db.WorkOrderViews;
        }

        // GET: api/WorkOrderViews/GetWorkOrderViewsByUserName/5
        [ResponseType(typeof(WorkOrderView))]
        public IHttpActionResult GetWorkOrderViewsByID(int iD)
        {
            cleanWorkOrderNoParammeters();

            var status = (from s in db.WorkOrderStatus where s.ID == 1 select s.Name).First();
            List<WorkOrderView> workOrderView = (
                from p in db.WorkOrderViews
                where p.userID == iD
                && p.WorkOrderStatus == status
                && p.WorkorderPercentage != null
                select p).ToList<WorkOrderView>();
            
            

            var loucv = db.userChieldViews.Where(u => u.userID == iD).ToList<userChieldView>();

            List <WorkOrderView> workOrderViewCield = new List<WorkOrderView>();
            if (loucv.Count() > 0)
            {
                for (int j = 0; j < loucv.Count(); j++)
                {
                    var cid = loucv[j].userChield;
                    workOrderViewCield = (
                    from p in db.WorkOrderViews
                    where p.userID == cid
                    && p.WorkOrderStatus == status
                    && p.WorkorderPercentage != null
                    select p).ToList<WorkOrderView>();
                    workOrderView.AddRange(workOrderViewCield);
                }
            }
            if (workOrderView == null)
            {
                return NotFound();
            }



            foreach (var item in workOrderView)
            {
                List<WorkOrdersParameter> a = new List<WorkOrdersParameter>( from p in db.WorkOrdersParameters
                    where p.workOrder_FKID == item.ID
                    select p);

                if (a.Count == 0)
                    workOrderView.Remove(item);
            }
            return Ok<List<WorkOrderView>>(workOrderView);
        }



        public void cleanWorkOrderNoParammeters()
        {
            string updateSQL;

            updateSQL = "Exec DB_A2CA3F_PM.dbo.deleteWorkordersWithNoParameters";


            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(updateSQL, con);
            //cmd.CommandType = CommandType.StoredProcedure;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception err)
            {
                var a = err.Message;
            }
        }

        // GET: api/WorkOrderViews/GetWorkOrderViewTodayByUserID/5
        [ResponseType(typeof(WorkOrderView))]
        public IHttpActionResult GetWorkOrderViewTodayByID(int iD)
        {
            cleanWorkOrderNoParammeters();

            var status = (from s in db.WorkOrderStatus where s.ID == 1 select s.Name).First();
            List<WorkOrderView> workOrderView = (
                from p in db.WorkOrderViews
                where p.userID == iD
                //&& (p.startingDate == (DateTime?)DateTime.Today.Date) 
                && p.WorkOrderStatus == status
                select p).ToList<WorkOrderView>();
            //List<UserChieldView> loucv = (
            //    from u in db.UserChieldView
            //    where (u.userID == iD)
            //    select u).ToList<UserChieldView>();
            //List<WorkOrderView> workOrderViewCield = new List<WorkOrderView>();
            //if (loucv.Count() > 0)
            //{
            //    for (int j = 0; j < loucv.Count(); j++)
            //    {
            //        var cid = loucv[j].userChield;
            //        workOrderViewCield = (
            //        from p in db.WorkOrderViews
            //        where p.userID == cid
            //        //&& (p.startingDate == (DateTime?)DateTime.Today.Date)
            //        && p.WorkOrderStatus == status
            //        select p).ToList<WorkOrderView>();
            //        workOrderView.AddRange(workOrderViewCield);
            //    }
            //}
            if (workOrderView == null)
            {
                return NotFound();
            }

            foreach (var item in workOrderView)
            {
                List<WorkOrdersParameter> a = new List<WorkOrdersParameter>(from p in db.WorkOrdersParameters
                                                                            where p.workOrder_FKID == item.ID
                                                                            select p);

                if (a.Count == 0)
                    workOrderView.Remove(item);
            }
            return Ok<List<WorkOrderView>>(workOrderView);
        }

        // PUT: api/WorkOrderViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkOrderView(int id, WorkOrderView workOrderView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOrderView.ID)
            {
                return BadRequest();
            }

            db.Entry(workOrderView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderViewExists(id))
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

        // POST: api/WorkOrderViews
        [ResponseType(typeof(WorkOrderView))]
        public IHttpActionResult PostWorkOrderView(WorkOrderView workOrderView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkOrderViews.Add(workOrderView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WorkOrderViewExists(workOrderView.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = workOrderView.ID }, workOrderView);
        }

        // DELETE: api/WorkOrderViews/5
        [ResponseType(typeof(WorkOrderView))]
        public IHttpActionResult DeleteWorkOrderView(int id)
        {
            WorkOrderView workOrderView = db.WorkOrderViews.Find(id);
            if (workOrderView == null)
            {
                return NotFound();
            }

            db.WorkOrderViews.Remove(workOrderView);
            db.SaveChanges();

            return Ok(workOrderView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrderViewExists(int id)
        {
            return db.WorkOrderViews.Count(e => e.ID == id) > 0;
        }
    }
}