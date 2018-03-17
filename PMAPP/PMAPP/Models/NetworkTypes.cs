namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;
    public partial class NetworkTypes
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public NetworkType()
        //{
        //    Parameters = new HashSet<Parameter>();
        //}

        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Notes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Parameter> Parameters { get; set; }
    }
}
