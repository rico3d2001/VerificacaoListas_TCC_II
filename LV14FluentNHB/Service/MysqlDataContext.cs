using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB.Service
{
    public class MysqlDataContext : UnitOfWork
    {


        private const string HOST = "localhost";
        private const string USER = "rico3d";
        private const string PASSWORD = "umsa45";
        private const string DB = "listaverificacao";


        public MysqlDataContext()
        {

        }



        protected override ISessionFactory createSession()
        {

            if (_sessionFactory != null)
                return _sessionFactory;

            //database configs
            FluentConfiguration _config = Fluently.Configure().Database(MySQLConfiguration.Standard.ConnectionString(
                                                                       x => x.Server(HOST).
                                                                          Username(USER).
                                                                          Password(PASSWORD).
                                                                          Database(DB)
                                                                        ))
                                                                        .Mappings(c => c.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                                                                        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));

            //var mapConfig = Fluently.Configure().Database(dbConfig).Mappings(c => c.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));

            _sessionFactory = _config.BuildSessionFactory();
            return _sessionFactory;
        }


    }
}
