namespace PMCP2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ParametersUnitType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParametersUnitType()
        {
            Parameters = new HashSet<Parameter>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string unitType { get; set; }

        [StringLength(50)]
        public string unitMesurment { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public double? Distance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
