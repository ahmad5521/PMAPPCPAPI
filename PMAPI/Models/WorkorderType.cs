namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkorderType")]
    public partial class WorkorderType
    {
        [Key]
        public int workorderTypeID { get; set; }

        [StringLength(100)]
        public string workorderTypeName { get; set; }
        
    }
}
