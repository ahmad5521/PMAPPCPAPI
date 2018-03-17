namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkOrderParametersView")]
    public partial class WorkOrderParametersView
    {
        [Key]
        //[Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int wopID { get; set; }

        public int? workOrder_FKID { get; set; }

        [StringLength(15)]
        public string woNo { get; set; }

        [StringLength(150)]
        public string ParameterName { get; set; }

        public double? Lenght { get; set; }

        public double? DoneLength { get; set; }

        public double? Amount { get; set; }

        public double? DoneAmount { get; set; }

        public int? parameter_FKID { get; set; }

        public bool? isDone { get; set; }

        public int? weightAmount { get; set; }

        //[Key]
        //[Column(Order = 1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int putID { get; set; }

        [StringLength(50)]
        public string ParametersUnitType { get; set; }

        //[Key]
        //[Column(Order = 2)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pcgID { get; set; }

        [StringLength(50)]
        public string ParemeterCalcGroup { get; set; }

        //[Key]
        //[Column(Order = 3)]
        public DateTime lastUpdate { get; set; }
    }
}
