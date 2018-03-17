namespace PMCP2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkorderType")]
    public partial class WorkorderType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkorderType()
        {
            ParameterCategories = new HashSet<ParameterCategory>();
        }

        public int workorderTypeID { get; set; }

        [StringLength(100)]
        public string workorderTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParameterCategory> ParameterCategories { get; set; }
    }
}
