using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMAPI.Models
{
    [Table("V_Project_totalAmounts_project_contractor_worktype1")]
    public class V_Project_totalAmounts_project_contractor_worktype1
    {
        public long ID { get; set; }

        public string contractorName { get; set; }

        public int project_FKID { get; set; }

        public string projectName { get; set; }

        public double totalAmount { get; set; }

        public double acheivedAmount { get; set; }

        public double remaining { get; set; }
    }
}