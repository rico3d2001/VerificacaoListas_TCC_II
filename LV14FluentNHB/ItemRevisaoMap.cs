using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class ItemRevisaoMap : ClassMap<ItemRevisao>
    {
        public ItemRevisaoMap()
        {
            Table("LV_ITEM_REVISAO");
            Id(x => x.GUID);
            Map(x => x.DESCRICAO);
            //Map(x => x.GUID_GRUPO);
            Map(x => x.ORDENADOR);

            References(x => x.Grupo, "GUID_GRUPO")
               .Fetch.Join()
              .Cascade.SaveUpdate();

        }
    }
}
