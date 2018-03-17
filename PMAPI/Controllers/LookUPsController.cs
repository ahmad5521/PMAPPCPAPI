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
    public class LookUPsController : ApiController
    {
        private DATA db = new DATA();

        // GET: api/LookUPs/GetLookUPs
        public LookUPs GetLookUPs()
        {
            var lookups = new LookUPs();
            lookups.projects = db.Projects.ToList();
            lookups.contractors = db.Contractors.ToList();
            lookups.locations = db.Locations.ToList();
            lookups.parameterCatagories = db.ParameterCategories.ToList();
            lookups.voltageLevels = db.VoltageLevels.ToList();
            lookups.users = db.Users.ToList();

            return lookups;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }       
    }
}