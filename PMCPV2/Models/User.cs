namespace PMCPV2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Users1 = new HashSet<User>();
            WorkOrders = new HashSet<WorkOrder>();
        }

        public int ID { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string userName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public Guid? token { get; set; }

        public bool? isActive { get; set; }
        
        [Display(Name = "SuperVisor Name")]
        public int? SuperVisor { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        [StringLength(50)]
        public string modirate { get; set; }

        [StringLength(50)]
        public string regon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users1 { get; set; }

        public virtual User User1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
