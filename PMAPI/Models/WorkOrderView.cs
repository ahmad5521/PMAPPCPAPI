namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkOrderView")]
    public partial class WorkOrderView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(15)]
        public string workOrderNo { get; set; }

        [StringLength(50)]
        public string ProjectName { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(50)]
        public string Contractor { get; set; }

        [StringLength(50)]
        public string ParametarCatagory { get; set; }

        [StringLength(50)]
        public string VoltageLevels { get; set; }

        public DateTime? startingDate { get; set; }

        public int? maxDueDays { get; set; }

        [StringLength(50)]
        public string WorkOrderStatus { get; set; }

        public DateTime? dateTimeStamp { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        public int? SuperVisor { get; set; }

        public long? value { get; set; }

        public int? userID { get; set; }

        public decimal? WorkorderPercentage { get; set; } 
    }
}
