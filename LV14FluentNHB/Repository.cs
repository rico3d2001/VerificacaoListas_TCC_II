using LV14FluentNHB.Service;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class Repository<T> : IRepository<T>
    {
        

        protected UnitOfWork _context;

        private ITransaction _transaction;

        protected ISession _currentSession;

        public Repository()
        {
           

        }

        public void Start(string banco)
        {
            if (string.IsNullOrEmpty(banco))
                throw new ArgumentNullException("unitOfWork");

            ConexaoFactory conexaoFactory = setConexao(banco);

            _context = conexaoFactory.GetConexao();//unitOfWork; // ; as MysqlDataContext;

            _transaction = _context.Secao.BeginTransaction();
        }

        public void Commit()
        {

            try
            {

                _transaction.Commit();
            }
            catch (Exception ex)
            {
                if (!_transaction.WasCommitted)
                {
                    _transaction.Rollback();
                }
                throw new Exception(ex.Message);
            }

        }



        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public T ReturnByGUID(string guid)
        {

            return _context.Secao.Get<T>(guid);

        }

        public T ReturnById(int id)
        {

            return _context.Secao.Get<T>(id);

        }

        public virtual void Delete(T entidade)
        {

            _context.Secao.Delete(entidade);

        }

        public virtual IList<T> GetByProperty(string property, object value)
        {

            StringBuilder hql = new StringBuilder();
            hql.Append(string.Format("FROM {0} a ", typeof(T).FullName));
            hql.Append(string.Format("WHERE a.{0} = ?", property));
            var obj = _context.Secao.CreateQuery(hql.ToString())
                .SetParameter(0, value)
                .List<T>();

            return obj;

        }

        public virtual void Insert(T entidade)
        {

            _context.Secao.Save(entidade);

        }

        public virtual bool IsSaved(string guid)
        {
            throw new NotImplementedException();
        }

        public virtual IList<T> Query()
        {

            return (from e in _context.Secao.Query<T>() select e).ToList();

        }

        public IQueryable<T> Query(Expression<Func<T, bool>> Where)
        {

            return _context.Secao.Query<T>();

        }


        public void Update(T entidade)
        {

            _context.Secao.Update(entidade);

        }

        private ConexaoFactory setConexao(string banco)
        {
            ConexaoFactory conexaoFactory = null;

            switch (banco)
            {
                case "Oracle":
                    conexaoFactory = new OracleFactory();
                    break;
                case "MySQL":
                    conexaoFactory = new MySqlFactory();
                    break;

            }

            return conexaoFactory;

        }


    }
}
