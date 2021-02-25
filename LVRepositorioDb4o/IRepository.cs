using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public interface IRepository<T>
    {
        void Insert(T entidade);
        void Update(T entidade);
        void Delete(T entidade);
        IList<T> Query();
        IList<T> GetByProperty(string propertyName, object value);
        bool IsSaved(string guid);
    }
}
