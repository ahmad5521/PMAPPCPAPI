namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParametersView")]
    public partial class Parameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        
    }
}
