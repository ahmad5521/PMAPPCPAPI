namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contractor
    {

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string contactName { get; set; }

        [StringLength(50)]
        public string contactMobile { get; set; }

        [StringLength(50)]
        public string contactEmail { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

    }
}
