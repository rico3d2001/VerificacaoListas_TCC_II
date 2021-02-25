using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LV14FluentNHB
{
    public class ConfirmacaoMap : ClassMap<Confirmacao>
    {
        public ConfirmacaoMap()
        {
            Table("LV_CONFIRMACAO");
            Id(x => x.GUID);
            Map(x => x.GUID_USUARIO1);
            Map(x => x.GUID_USUARIO2);
            Map(x => x.INDICE_REV);
            Map(x => x.DATA);
            Map(x => x.GUID_DOCUMENTO);
            Map(x => x.ORDENADOR);

            //HasMany(x => x.ListaRevisoes).Table("LV_REVISAO").KeyColumns.Add("GUID_CONFIRMADO").Cascade.All(); //.Fetch.Join();
        }
    }
}
