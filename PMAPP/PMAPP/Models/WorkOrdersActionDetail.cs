namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;
    public partial class WorkOrdersActionDetail
    {
        public int ID { get; set; }

        public int? WorkOrdersAction_FKID { get; set; }

        public int? WorkOrdersParameter_FKID { get; set; }

        public double? Amount { get; set; }

        public double? Amountchange { get; set; }
    }
}
