using System;
using System.Collections.Generic;

namespace LV14FluentNHB
{
    public interface IRepository<T> : IDisposable
    {
        void Insert(T entidade);
        void Update(T entidade);
        void Delete(T entidade);
        IList<T> Query();
        IList<T> GetByProperty(string propertyName, object value);
        //bool IsSaved(string guid);

        T ReturnByGUID(string guid);

        T ReturnById(int id);

        void Start(string banco);
        void Commit();
    }
}
