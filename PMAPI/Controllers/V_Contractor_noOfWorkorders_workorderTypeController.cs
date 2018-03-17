using PMAPI.Functions;
using PMAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PMAPI.Controllers
{
    [BasicAuthentication]
    public class V_Contractor_noOfWorkorders_workorderTypeController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/V_Contractor_noOfWorkorders_workorderType/GetV_Contractor_noOfWorkorders_workorderTypeViews
        public IQueryable<V_Contractor_noOfWorkorders_workorderType> GetV_Contractor_noOfWorkorders_workorderTypeViews()
        {
            return db.V_Contractor_noOfWorkorders_workorderType;
        }
    }
}
