namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class WorkOrdersActions
    {
        public int ID { get; set; }

        public int? WorkOrder_FKID { get; set; }

        public DateTime? dateTimeStamp { get; set; }

        public int? User_FKID { get; set; }
        
    }
}
