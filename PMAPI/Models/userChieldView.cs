namespace PMAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userChieldView")]
    public partial class userChieldView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long? ID { get; set; }

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userID { get; set; }

        public int? userChield { get; set; }
    }
}
