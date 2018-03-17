namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class WorkOrdersUsers
    {
        public int ID { get; set; }

        public int? workOrder_FKID { get; set; }

        public int? User_FKID { get; set; }

        public bool? isActive { get; set; }

        public DateTime? dateTimeStamp { get; set; }
        
        public DateTime? fromDate { get; set; }
        
        public DateTime? toDate { get; set; }
    }
}
