using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public interface IViewListRepository<T>
    {
        IList<T> GetByProperty(object value);
    }
}
