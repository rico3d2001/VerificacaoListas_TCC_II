using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public interface IRepositoryGUID<T>:IRepository<T>
    {
        T ReturnByGUID(string guid);
    }
}
