using FluentNHibernate.Mapping;
using LVModel;

namespace LV14FluentNHB
{
    public class ArquivoListasMap : ClassMap<ArquivoListas>
    {


        public ArquivoListasMap()
        {
            Table("LV_TIPO");
            Id(x => x.GUID);
            Map(x => x.NOME);
            //Map(x => x.GUID_CONFIG);
            Map(x => x.SIGLA);

           References(x => x.Configuracao, "GUID_CONFIG")
            .Fetch.Join()
            .Cascade.SaveUpdate();

            HasMany(x => x.ListaPlanilhas).Table("LV_PLANILHA").KeyColumns.Add("GUID_TIPO").Cascade.All();//.Fetch.Join();
        }
    }
}
