using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPP.Models
{
    public class WorkOrderParametersView
    {
        public int wopID { get; set; }

        public int? workOrder_FKID { get; set; }

        public string woNo { get; set; }
        
        public string ParameterName { get; set; }

        public double? Lenght { get; set; }

        public double? DoneLength { get; set; }

        public double? Amount { get; set; }

        public double? DoneAmount { get; set; }

        public int? parameter_FKID { get; set; }

        public bool isDone { get; set; }

        public int? weightAmount { get; set; }
        
        public int putID { get; set; }
        
        public string ParametersUnitType { get; set; }
        
        public int pcgID { get; set; }
        
        public string ParemeterCalcGroup { get; set; }
        
        public DateTime lastUpdate { get; set; }


        public string what { get; set; }
        public string data { get; set; }
        public string injaz { get; set; }
    }
}
