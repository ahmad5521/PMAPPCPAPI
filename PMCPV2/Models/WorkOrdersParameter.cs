namespace PMCP2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkOrdersParameter
    {
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

        public virtual Parameter Parameter { get; set; }

        public virtual WorkOrder WorkOrder { get; set; }
    }
}
