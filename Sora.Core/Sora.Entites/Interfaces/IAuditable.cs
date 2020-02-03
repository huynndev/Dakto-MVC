using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Entites.Interfaces
{
    public interface IAuditable
    {
        string AAStatus { set; get; }
        DateTime? AACreatedDate { set; get; }
        string AACreatedUser { set; get; }
        DateTime? AAUpdatedDate { set; get; }
        string AAUpdatedUser { set; get; }
    }
}
