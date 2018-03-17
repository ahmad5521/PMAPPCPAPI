using PMAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PMAPI.Functions;

namespace PMAPI.Controllers
{
    [BasicAuthentication]
    public class V_Contractor_noOfAchievedWorkorders_workorderTypeController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/V_Contractor_noOfAchievedWorkorders_workorderType/GetV_Contractor_noOfAchievedWorkorders_workorderType
        public IQueryable<V_Contractor_noOfAchievedWorkorders_workorderType> GetV_Contractor_noOfAchievedWorkorders_workorderType()
        {
            return db.V_Contractor_noOfAchievedWorkorders_workorderType;
        }
    }
}
