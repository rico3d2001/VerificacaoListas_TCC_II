using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class PlanilhaMap : ClassMap<Planilha>
    {
        public PlanilhaMap()
        {
            Table("LV_PLANILHA");
            Id(x => x.GUID);
            Map(x => x.NOME);
           
            //Map(x => x.GUID_TIPO).CustomSqlType("VARCHAR2(255 BYTE)"); ;
            Map(x => x.FUNCAO);
            Map(x => x.DESCRICAO);
            Map(x => x.VERIFICADOR_UNICO);

            References(x => x.Tipo, "GUID_TIPO")
                .Fetch.Join()
                .Cascade.SaveUpdate();


            HasMany(x => x.ListaGrupos).Table("LV_GRUPO").KeyColumns.Add("GUID_PLANILHA").Cascade.All();//.Fetch.Join();
        }
    }
}
