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
    public class V_Project_percentOfAchievement_contractorsController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/V_Project_percentOfAchievement_contractors/V_Project_percentOfAchievement_contractors
        [AcceptVerbs("GET")]
        public IQueryable<V_Project_percentOfAchievement_contractors> V_Project_percentOfAchievement_contractors()
        {
            return db.V_Project_percentOfAchievement_contractors;
        }

        // GET: api/V_Project_percentOfAchievement_contractors/GetV_Project_percentOfAchievement_contractors/5
        [ResponseType(typeof(V_Project_percentOfAchievement_contractors))]
        public IHttpActionResult GetV_Project_percentOfAchievement_contractors(int id)
        {
            List<V_Project_percentOfAchievement_contractors> ListOfV_Project_percentOfAchievement_contractors
                = (from p in db.V_Project_percentOfAchievement_contractors
                   where p.project_FKID == id
                   select p).ToList<V_Project_percentOfAchievement_contractors>();

            if (ListOfV_Project_percentOfAchievement_contractors == null)
            {
                return NotFound();
            }

            return Ok<List<V_Project_percentOfAchievement_contractors>>(ListOfV_Project_percentOfAchievement_contractors);
        }
    }
}
