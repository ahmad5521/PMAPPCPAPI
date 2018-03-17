namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkOrdersPhotos")]
    public partial class WorkOrdersPhoto
    {
        public int id { get; set; }

        public int? WOID_FK { get; set; }

        public byte[] imageData { get; set; }

        public DateTime? date { get; set; }
    }
}
