using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public interface IRepositoryID<T>:IRepository<T>
    {
        T ReturnById(int id);
    }
}
