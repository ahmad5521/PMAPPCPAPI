namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class WorkOrders
    {
        public int ID { get; set; }
        
        public string Number { get; set; }

        public int? project_FKID { get; set; }

        public int? location_FKID { get; set; }

        public int? contractors_FKID { get; set; }

        public int? ParameterCategory_FKID { get; set; }

        public double? totalLenght { get; set; }

        public int? voltageLevels_FKID { get; set; }
        
        public DateTime? startingDate { get; set; }

        public int? maxDueDays { get; set; }

        public DateTime? dateTimeStamp { get; set; }

        public int? workOrderStatus_FKID { get; set; }

        public int? user_FKID { get; set; }

        public long value { get; set; }

    }
}
