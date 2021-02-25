using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LV14FluentNHB
{
    public class ProjetoMap : ClassMap<Projeto>
    {
        public ProjetoMap()
        {
            Table("LV_PROJETO");
            Id(x => x.GUID);
            Map(x => x.NUMERO);

            HasMany(x => x.ListaAreas).Table("LV_AREA").KeyColumns.Add("GUID_PROJETO").Cascade.All();//.Fetch.Join();
            HasMany(x => x.ListaOSs).Table("LV_OS").KeyColumns.Add("GUID_PROJETO").Cascade.All();//.Fetch.Join();
            //HasMany(x => x.ListaDocumentos).Table("LV_DOC").KeyColumns.Add("GUID_PROJETO").Cascade.All();//.Fetch.Join();
            HasMany(x => x.ListaNumerosSNC).Table("LV_NUMERO_SNC").KeyColumns.Add("PROJETO").Cascade.All();//.Fetch.Join();
        }
    }
}
