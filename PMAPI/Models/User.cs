namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public Guid? token { get; set; }

        public bool? isActive { get; set; }

        public int? SuperVisor { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        [StringLength(50)]
        public string modirate { get; set; }

        [StringLength(50)]
        public string regon { get; set; }

    }
}
