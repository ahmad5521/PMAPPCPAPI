using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAPI.Models
{
    public class LookUPs
    {
        public List<Project> projects { get; set; }

        public List<Contractor> contractors { get; set; }

        public List<Location> locations { get; set; }

        public List<ParameterCategory> parameterCatagories { get; set; }

        public List<VoltageLevel> voltageLevels { get; set; }

        public List<User> users { get; set; }

    }
}