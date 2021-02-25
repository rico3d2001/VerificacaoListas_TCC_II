using LV14FluentNHB.Interface;
using LV14FluentNHB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppListaVerificacao.Interface
{
    public class AppServiceBase<T> : IAppServiceBase<T> where T : class
    {
       
        private readonly IServiceBase<T> _serviceBase;

        protected string _banco;

        public AppServiceBase(IServiceBase<T> serviceBase)
        {
            _serviceBase = serviceBase;
            _banco = "MySQL";//"Oracle";//"MySQL";

        }

        public virtual T ReturnByGUID(string guid)
        {
            return _serviceBase.ReturnByGUID(guid);
        }

        public T ReturnById(int id)
        {
            return _serviceBase.ReturnById(id);
        }

        public virtual IList<T> GetByProperty(string property, object value)
        {

            return _serviceBase.GetByProperty(property, value);
        }

        public virtual void Delete(T entidade)
        {
            _serviceBase.Delete(entidade);
        }

        public virtual void Insert(T entidade)
        {
            _serviceBase.Insert(entidade);
        }

     

        //public virtual bool IsSaved(string guid)
        //{
        //    return _serviceBase.IsSaved(guid);
        //}

        public virtual IList<T> Query()
        {
            return _serviceBase.Query();
        }

        public virtual void Update(T entidade)
        {
            _serviceBase.Update(entidade);
        }

        public void Start()
        {
            _serviceBase.Start(_banco);
        }

        public void Commit()
        {
            _serviceBase.Commit();
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }

       



    }
}
