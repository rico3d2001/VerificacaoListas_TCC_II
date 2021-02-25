using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class NumeroDocSNCLavalinMap : ClassMap<NumeroDocSNCLavalin>
    {
        public NumeroDocSNCLavalinMap()
        {
            Table("LV_NUMERO_SNC");
            Id(x => x.GUID);
            Map(x => x.NUMERO);
            Map(x => x.PROJETO);
            Map(x => x.OS);
            Map(x => x.AREA);
            Map(x => x.DISCIPLINA);
            Map(x => x.TIPO);
            Map(x => x.SEQUENCIAL);
            Map(x => x.GUID_ULTIMA_CONFIRMACAO);
        }
    }
}
