namespace PMCPV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Parameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Parameter()
        {
            WorkOrdersParameters = new HashSet<WorkOrdersParameter>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? parameterCategory_FKID { get; set; }

        public int? parameterCalcGroup_FKID { get; set; }

        public double? ParameterWight { get; set; }

        public int? parameterUnitType_FKID { get; set; }

        public int? networkType_FKID { get; set; }

        public bool? isActive { get; set; }

        public bool? isRquired { get; set; }

        public int? parameterNameID_FK { get; set; }

        public virtual ParameterCategory ParameterCategory { get; set; }

        public virtual ParameterName ParameterName { get; set; }

        public virtual ParametersUnitType ParametersUnitType { get; set; }

        public virtual ParemeterCalcGroup ParemeterCalcGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrdersParameter> WorkOrdersParameters { get; set; }
    }
}
