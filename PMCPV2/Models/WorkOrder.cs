namespace PMCP2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkOrder()
        {
            WorkOrdersParameters = new HashSet<WorkOrdersParameter>();
        }

        public int ID { get; set; }

        [StringLength(15)]
        public string Number { get; set; }

        public int? project_FKID { get; set; }

        public int? location_FKID { get; set; }

        public int? contractors_FKID { get; set; }

        public int? ParameterCategory_FKID { get; set; }

        public double? totalLenght { get; set; }

        public int? voltageLevels_FKID { get; set; }

        public DateTime? startingDate { get; set; }

        public int? maxDueDays { get; set; }

        public DateTime? dateTimeStamp { get; set; }

        public int? workOrderStatus_FKID { get; set; }

        public int? user_FKID { get; set; }

        public long? value { get; set; }

        public virtual Contractor Contractor { get; set; }

        public virtual Location Location { get; set; }

        public virtual ParameterCategory ParameterCategory { get; set; }

        public virtual Project Project { get; set; }

        public virtual User User { get; set; }

        public virtual VoltageLevel VoltageLevel { get; set; }

        public virtual WorkOrderStatu WorkOrderStatu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrdersParameter> WorkOrdersParameters { get; set; }
    }
}
