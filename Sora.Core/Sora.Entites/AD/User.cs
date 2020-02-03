namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [StringLength(37)]
        public string UserID { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public DateTime? CreateDateTime { get; set; }

        [StringLength(37)]
        public string RoleID { get; set; }

        public int? Status { get; set; }

        [StringLength(255)]
        public string Avatar { get; set; }
    }
}
