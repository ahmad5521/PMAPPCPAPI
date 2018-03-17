namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParameterCategory")]
    public partial class ParameterCategory
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public int? workorderTypeID_FK { get; set; }

       

    }
}
