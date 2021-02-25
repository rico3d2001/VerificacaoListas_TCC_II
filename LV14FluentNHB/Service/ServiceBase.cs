using LV14FluentNHB.Interface;
using System.Collections.Generic;


namespace LV14FluentNHB.Service
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {

        protected IRepository<T> _repository;


        public ServiceBase(IRepository<T> repository)
        {

            _repository = repository;
        }

        public IList<T> GetByProperty(string property, object value)
        {
            return _repository.GetByProperty(property, value);
        }


        public virtual void Insert(T entidade)
        {

            _repository.Insert(entidade);

        }

        public virtual T ReturnByGUID(string guid)
        {
            return _repository.ReturnByGUID(guid);


        }

        public virtual T ReturnById(int id)
        {
            return _repository.ReturnById(id);


        }

        //public virtual bool IsSaved(string guid)
        //{
        //    return _repository.IsSaved(guid);
        //}


        public virtual void Delete(T entidade)
        {
            _repository.Delete(entidade);

        }


        public virtual IList<T> Query()
        {

            return _repository.Query();


        }


        public virtual void Update(T entidade)
        {

            _repository.Update(entidade);
            //Mapper.Reset();
        }

        public void Start(string banco)
        {
            _repository.Start(banco);
        }

        public void Commit()
        {
            _repository.Commit();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }


    }
}
