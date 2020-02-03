namespace Sora.Entites.GE
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("District")]
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string LatiLongTude { get; set; }

        public int ProvinceId { get; set; }

        public int? SortOrder { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsDeleted { get; set; }

        public virtual Province Province { get; set; }

        public virtual ICollection<Ward> Wards { get; set; }
    }
}