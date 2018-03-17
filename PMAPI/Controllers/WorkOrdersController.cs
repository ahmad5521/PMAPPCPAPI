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
    public class WorkOrdersController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/WorkOrders/GetWorkOrders
        public IQueryable<WorkOrder> GetWorkOrders()
        {
            return db.WorkOrders;
        }

        // GET: api/WorkOrders/5
        [ResponseType(typeof(WorkOrder))]
        public IHttpActionResult GetWorkOrder(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return Ok(workOrder);
        }


        [ResponseType(typeof(User))]
        public User GetUserByNumber(int id)
        {
            User workOrder = db.Users.Where(p => p.userName == id.ToString()).First();

            return workOrder;
            //if (workOrder == null)
            //{
            //    return NotFound();
            //}

            //return Ok(workOrder);
        }

        // GET: api/WorkOrders/GetWorkOrderByNumber/5
        [ResponseType(typeof(WorkOrder))]
        public IHttpActionResult GetWorkOrderByNumber(int id)
        {
            List< WorkOrder> workOrder = db.WorkOrders.Where(p => p.Number == id.ToString()).ToList();
            
            if (workOrder == null)
            {
                return NotFound();
            }

            return Ok(workOrder);
        }

        //PUT: api/WorkOrders/PutWorkOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkOrder(int id, WorkOrder workOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workOrder.ID)
            {
                return BadRequest();
            }

            db.Entry(workOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();

                //bool isOK = updateWOP(id, workOrder);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOrderExists(id))
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

        //private bool updateWOP(int id, WorkOrder workOrder)
        //{
        //    //List<WorkOrdersParameter> wopList = ;
        //    foreach (WorkOrdersParameter wop in db.WorkOrdersParameters.Where(p => p.workOrder_FKID == id).ToList())
        //        db.WorkOrdersParameters.Remove(wop);
        //    db.SaveChanges();
        //    List<Parameter> lofp = new List<Parameter>();
        //    foreach (Parameter parameter in db.Parameters.Where(p => p.parameterCategory_FKID == workOrder.ParameterCategory_FKID))
        //    {
        //        lofp.Add(parameter);
        //    }
        //    List<WorkOrdersParameter> lofwop = new List<WorkOrdersParameter>();
        //    WorkOrdersParameter newWOP = new WorkOrdersParameter();
        //    for (int i = 0; i < lofp.Count; i++)
        //    {
        //        newWOP.workOrder_FKID = workOrder.ID;
        //        newWOP.parameter_FKID = lofp[i].ID;
        //        newWOP.Lenght = workOrder.totalLenght;
        //        newWOP.lastUpdate = DateTime.Now;
        //        newWOP.isDone = false;

        //        db.WorkOrdersParameters.Add(newWOP);
        //        db.SaveChanges();
        //    }

        //    return true;
        //}

        // POST: api/WorkOrders
        [ResponseType(typeof(WorkOrder))]
        public IHttpActionResult PostWorkOrder(WorkOrder workOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.WorkOrders.Add(workOrder);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = workOrder.ID }, workOrder);
        }

        //// PUT: api/WorkOrders/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutWorkOrder(int id, WorkOrder workOrder)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != workOrder.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(workOrder).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WorkOrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/WorkOrders
        //[ResponseType(typeof(WorkOrder))]
        //public IHttpActionResult PostWorkOrder(WorkOrder workOrder)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.WorkOrders.Add(workOrder);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = workOrder.ID }, workOrder);
        //}




        // DELETE: api/WorkOrders/DeleteWorkOrder/5
        [ResponseType(typeof(WorkOrder))]
        public IHttpActionResult DeleteWorkOrder(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            db.WorkOrdersParameters.RemoveRange(db.WorkOrdersParameters.Where(p => p.workOrder_FKID == id));
            db.SaveChanges();
            db.WorkOrders.Remove(workOrder);
            db.SaveChanges();

            return Ok(workOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkOrderExists(int id)
        {
            return db.WorkOrders.Count(e => e.ID == id) > 0;
        }
    }
}