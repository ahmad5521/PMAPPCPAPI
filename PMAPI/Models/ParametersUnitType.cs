namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ParametersUnitType
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string unitType { get; set; }

        [StringLength(50)]
        public string unitMesurment { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public double? Distance { get; set; }
        
    }
}
