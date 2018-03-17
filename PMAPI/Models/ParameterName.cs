namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParameterName")]
    public partial class ParameterName
    {
        [Key]
        public int parameterNameID { get; set; }

        [StringLength(150)]
        public string parameterNameName { get; set; }

    }
}
