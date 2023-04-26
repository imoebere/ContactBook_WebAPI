using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.Interface;

namespace Data.Repositories
{
    public class Paginated : IPaginated
    {
        public IEnumerable<T> Pagination<T>(List<T> list, int page, int perpage)
        {
            page = page < 1 ? 1 : page;
            perpage = perpage < 1 ? 5 : perpage;

            if (list.Count > 0)
            {
                var paginated = list.Skip((page - 1) * perpage).Take(perpage).ToList();
                return paginated;
            }
            return new List<T>(list);
        }
    }
}
