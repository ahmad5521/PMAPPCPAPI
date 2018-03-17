using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMAPI.Models
{
    [Table("V_Contractor_noOfAchievedWorkorders_workorderType")]
    public class V_Contractor_noOfAchievedWorkorders_workorderType
    {
        public long ID { get; set; }

        public string contractorName { get; set; }

        public string workorderTypeName { get; set; }

        public int noOfCompleted { get; set; }

        public int noOfNotCompleted { get; set; }

    }
}