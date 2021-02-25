using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB.Service
{
    public class MySqlFactory : ConexaoFactory
    {
        public override UnitOfWork GetConexao()
        {
            return new MysqlDataContext();
        }
    }
}
