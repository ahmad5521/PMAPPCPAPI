namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Contractors
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string contactName { get; set; }
        
        public string contactMobile { get; set; }
                
        public string contactEmail { get; set; }
        
        public string Notes { get; set; }
    }
}
