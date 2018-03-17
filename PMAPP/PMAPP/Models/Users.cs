namespace PMAPP.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Users
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string userName { get; set; }
        
        public string Password { get; set; }

        public Guid? token { get; set; }

        public bool? isActive { get; set; }

        public int? SuperVisor { get; set; }

        public string role { get; set; }

        public string modirate { get; set; }

        public string regon { get; set; }

    }
}
