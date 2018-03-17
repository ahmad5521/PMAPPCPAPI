using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMAPI.Models
{
    [Table("V_Project_totalLengths_project_contractor")]
    public class V_Project_totalLengths_project_contractor
    {
        public long ID { get; set; }

        public string contractorName { get; set; }

        public int project_FKID { get; set; }

        public string projectName { get; set; }

        public double totalLength { get; set; }

        public double acheivedLength { get; set; }

        public double remaining { get; set; }
    }
}