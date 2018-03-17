using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPP.Models
{
    public class LookUPs
    {
        public List<Projects> projects { get; set; }

        public List<Contractors> contractors { get; set; }

        public List<Locations> locations { get; set; }

        public List<ParameterCategory> parameterCatagories { get; set; }

        public List<VoltageLevels> voltageLevels { get; set; }

        public List<Users> users { get; set; }
    }
}
