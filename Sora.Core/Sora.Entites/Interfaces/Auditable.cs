using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Entites.Interfaces
{
    public abstract class Auditable : IAuditable
    {
        public string AAStatus { get; set; }
        public DateTime? AACreatedDate { get; set; }
        public string AACreatedUser { get; set; }
        public DateTime? AAUpdatedDate { get; set; }
        public string AAUpdatedUser { get; set; }
    }
}
