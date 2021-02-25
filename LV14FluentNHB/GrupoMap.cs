using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class GrupoMap : ClassMap<Grupo>
    {
        public GrupoMap()
        {
            Table("LV_GRUPO");
            Id(x => x.GUID);
            Map(x => x.NOME);
            //Map(x => x.GUID_PLANILHA);
            Map(x => x.ORDENADOR);

            References(x => x.Planilha, "GUID_PLANILHA")
                .Fetch.Join()
                .Cascade.SaveUpdate();


            HasMany(x => x.ListaItens).Table("LV_ITEM_REVISAO").KeyColumns.Add("GUID_GRUPO").Cascade.All();
            //HasMany(x => x.ListaItens).KeyColumn("GUID_GRUPO").Fetch.Join();

         

        }
    }
}
