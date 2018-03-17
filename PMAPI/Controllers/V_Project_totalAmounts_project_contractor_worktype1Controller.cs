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
    public class V_Project_totalAmounts_project_contractor_worktype1Controller : ApiController
    {
        private DATA db = new DATA();

        // GET: api/V_Project_totalAmounts_project_contractor_worktype1/V_Project_totalAmounts_project_contractor_worktype1
        [AcceptVerbs("GET")]
        public IQueryable<V_Project_totalAmounts_project_contractor_worktype1> V_Project_totalAmounts_project_contractor_worktype1()
        {
            return db.V_Project_totalAmounts_project_contractor_worktype1;
        }

        // GET: api/V_Project_totalAmounts_project_contractor_worktype1/GetV_Project_totalAmounts_project_contractor_worktype1/5
        [ResponseType(typeof(V_Project_totalAmounts_project_contractor_worktype1))]
        public IHttpActionResult GetV_Project_totalAmounts_project_contractor_worktype1(int id)
        {
            List<V_Project_totalAmounts_project_contractor_worktype1> ListOfV_Project_totalAmounts_project_contractor_worktype1
                = (from p in db.V_Project_totalAmounts_project_contractor_worktype1
                   where p.project_FKID == id
                   select p).ToList<V_Project_totalAmounts_project_contractor_worktype1>();

            if (ListOfV_Project_totalAmounts_project_contractor_worktype1 == null)
            {
                return NotFound();
            }

            return Ok<List<V_Project_totalAmounts_project_contractor_worktype1>>(ListOfV_Project_totalAmounts_project_contractor_worktype1);
        }
    }
}
