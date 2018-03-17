using PMAPI.Functions;
using PMAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PMAPI.Controllers
{
    [BasicAuthentication]
    public class V_Project_totalLengths_project_contractorController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/V_Project_totalLengths_project_contractor/V_Project_totalLengths_project_contractor
        [AcceptVerbs("GET")]
        public IQueryable<V_Project_totalLengths_project_contractor> V_Project_totalLengths_project_contractor()
        {
            return db.V_Project_totalLengths_project_contractor;
        }

        // GET: api/V_Project_totalLengths_project_contractor/GetV_Project_totalLengths_project_contractor/5
        [ResponseType(typeof(V_Project_totalLengths_project_contractor))]
        public IHttpActionResult GetV_Project_totalLengths_project_contractor(int id)
        {
            List<V_Project_totalLengths_project_contractor> ListOfV_Project_totalLengths_project_contractor = (from p in db.V_Project_totalLengths_project_contractor
                                                                         where p.project_FKID == id
                                                                         select p).ToList<V_Project_totalLengths_project_contractor>();
                
            if (ListOfV_Project_totalLengths_project_contractor == null)
            {
                return NotFound();
            }
            
            return Ok<List< V_Project_totalLengths_project_contractor>>(ListOfV_Project_totalLengths_project_contractor);
        }
    }
}
