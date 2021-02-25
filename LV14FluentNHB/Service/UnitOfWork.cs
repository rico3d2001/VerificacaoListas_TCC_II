using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB.Service
{
    public abstract class UnitOfWork : IDisposable
    {
        
        protected static ISessionFactory _sessionFactory;

      

        protected ISession _currentSession;

        public ISession Secao => _currentSession;



        public UnitOfWork() 
        {
            _sessionFactory = createSession();
            _currentSession = _sessionFactory.OpenSession();
        }

        protected abstract ISessionFactory createSession();

        public void Dispose()
        {
            _currentSession.Close();
            GC.SuppressFinalize(this);
        }


    }
}
