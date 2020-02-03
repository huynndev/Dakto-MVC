namespace Sora.Entites.IC
{
    using Sora.Entites.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ICContactMessages")]
    public partial class ICContactMessage : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ICContactMessageID { get; set; }

        [StringLength(50)]
        public string ICContactMessageNo { get; set; }

        [StringLength(256)]
        public string ICContactMessageSubject { get; set; }

        [StringLength(256)]
        public string ICContactMessageContactName { get; set; }

        [StringLength(256)]
        public string ICContactMessageContactEmail { get; set; }

        [StringLength(50)]
        public string ICContactMessageContactPhone { get; set; }

        [StringLength(1024)]
        public string ICContactMessageContent { get; set; }
    }
}
