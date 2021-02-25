using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB.Service
{
    public class OracleDataContext : UnitOfWork
    {
        protected override ISessionFactory createSession()
        {
            if (_sessionFactory != null)
            {
                return _sessionFactory;
            }

            string strConexao =
                "Data Source=swp;Persist Security Info=True;User ID=swp;Password=m1n3r11swp;Unicode=True";



            IPersistenceConfigurer dbConfig =
                OracleDataClientConfiguration.Oracle10.ConnectionString(strConexao)
                   .AdoNetBatchSize(100)
                   .Driver<NHibernate.Driver.OracleClientDriver>();

            var mapConfig = Fluently.Configure().Database(dbConfig).Mappings(c => c.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));//clientesMap>());

            _sessionFactory = mapConfig.BuildSessionFactory();

            return _sessionFactory;
        }
    }
}
