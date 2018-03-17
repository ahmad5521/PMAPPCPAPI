using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPP.Models
{
    public class V_Project_percentOfAchievement_contractors
    {
        public int workorderTypeID { get; set; }

        public int project_FKID { get; set; }

        public string contractorName { get; set; }

        public string projectName { get; set; }

        public decimal averagePercentageOfAchievementBasedOnWeights { get; set; }

        public decimal remaining{ get; set; }
    }
}
