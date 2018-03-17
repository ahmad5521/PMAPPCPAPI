using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPP.Models
{
    public class V_Project_totalAmounts_project_contractor
    {
        public string contractorName { get; set; }

        public int project_FKID { get; set; }

        public string projectName { get; set; }

        public double totalAmount { get; set; }

        public double acheivedAmount { get; set; }

        public double remaining { get; set; }
    }
}
