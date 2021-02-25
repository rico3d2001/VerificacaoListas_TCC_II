using LV14FluentNHB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppListaVerificacao.Interface
{
    public interface IAppServiceBase<T> : IDisposable
    {
        void Insert(T entidade);
        void Update(T entidade);
        void Delete(T entidade);
        IList<T> Query();
        IList<T> GetByProperty(string property, object value);
        //bool IsSaved(string guid);

        T ReturnByGUID(string guid);

        T ReturnById(int id);

        void Start();
        void Commit();
    }
}
