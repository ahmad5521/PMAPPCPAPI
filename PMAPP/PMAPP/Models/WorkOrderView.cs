namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkOrderView
    {
        public int ID { get; set; }
        
        public string workOrderNo { get; set; }
        
        public string ProjectName { get; set; }
        
        public string Location { get; set; }
        
        public string Contractor { get; set; }

        public string ParametarCatagory { get; set; }

        public string VoltageLevels { get; set; }
        
        public DateTime? startingDate { get; set; }

        public int? maxDueDays { get; set; }
        
        public string WorkOrderStatus { get; set; }
        
        public string userName { get; set; }

        public int? SuperVisor { get; set; }

        public DateTime? dateTimeStamp { get; set; }

        public int Duration { get; set; }

        public long value { get; set; }

        public int? userID { get; set; }

        public Decimal WorkorderPercentage { get; set; }
    }
}
