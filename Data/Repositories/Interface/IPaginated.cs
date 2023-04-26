using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interface
{
    public interface IPaginated
    {
        public IEnumerable<T> Pagination<T>(List<T> list, int page, int perpage);
    }
}
