using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("LV_USUARIO");
            Id(x => x.GUID);
            Map(x => x.NOME);
            Map(x => x.ISCONFIGURADOR);
            Map(x => x.ISVERIFICADOR);
            Map(x => x.ISGESTOR);
            Map(x => x.SIGLA);
            Map(x => x.SENHA);
        }
    }
}
