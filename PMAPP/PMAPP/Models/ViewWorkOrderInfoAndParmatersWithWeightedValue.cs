using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPP.Models
{
    public class ViewWorkOrderInfoAndParmatersWithWeightedValue
    {
        public int ID { get; set; }

        public int? workOrder_FKID { get; set; }
        
        public string Number { get; set; }

        public int? parameter_FKID { get; set; }

        public double? Lenght { get; set; }

        public double? Amount { get; set; }

        public bool? isDone { get; set; }
        
        public DateTime lastUpdate { get; set; }

        public double? ParameterWight { get; set; }

        public double? ParameterDoneWeight { get; set; }

        public int? contractors_FKID { get; set; }
        
        public string contractorName { get; set; }

        public int? user_FKID { get; set; }
        
        public string userName { get; set; }
    }
}
