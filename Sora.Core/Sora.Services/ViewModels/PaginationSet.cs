using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sora.Services.ViewModels
{
    public class PaginationSet<T>
    {
        public int PageIndex { set; get; }

        public int PageSize { get; set; }

        public int TotalRow { set; get; }

        public IEnumerable<T> Items { set; get; }
    }
}
