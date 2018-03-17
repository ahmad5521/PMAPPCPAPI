namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Projects
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Notes { get; set; }

        public DateTime? dateTimeStamp { get; set; }
    }
}
