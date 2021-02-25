using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB.Service
{
    public abstract class ConexaoFactory
    {
        public abstract UnitOfWork GetConexao();
    }
}
