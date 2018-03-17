namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class WorkOrdersParameters
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public WorkOrdersParameter()
        //{
        //    WorkOrdersActionDetails = new HashSet<WorkOrdersActionDetail>();
        //}

        public int ID { get; set; }

        public int? workOrder_FKID { get; set; }

        public int? parameter_FKID { get; set; }

        public double? Lenght { get; set; }

        public double? Amount { get; set; }

        public bool? isDone { get; set; }

        public DateTime lastUpdate { get; set; }

        public double? DoneLength { get; set; }

        public double? DoneAmount { get; set; }

        public int? weightAmount { get; set; }
    }
}
