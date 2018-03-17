using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAPI.Models
{
    public class V_Project_percentOfAchievement_contractors
    {
        public long ID { get; set; }

        public int workorderTypeID { get; set; }

        public int project_FKID { get; set; }

        public string contractorName { get; set; }

        public string projectName { get; set; }

        public decimal averagePercentageOfAchievementBasedOnWeights { get; set; }
    }
}