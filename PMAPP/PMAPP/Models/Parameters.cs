namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Parameters
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
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
