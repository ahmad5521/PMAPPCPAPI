using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPP.Models
{
    public class V_Contractor_noOfAchievedWorkorders_workorderType
    {
        public string contractorName { get; set; }

        public string workorderTypeName { get; set; }

        public int noOfCompleted { get; set; }

        public int noOfNotCompleted { get; set; }
    }
}
