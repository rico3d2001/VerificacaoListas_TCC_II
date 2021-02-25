using FluentNHibernate.Mapping;
using LVModel;

namespace LV14FluentNHB
{
    public class ConfiguracaoMap : ClassMap<Configuracao>
    {
        public ConfiguracaoMap()
        {
            Table("LV_CONFIGURACAO");
            Id(x => x.GUID);
            Map(x => x.NOME);
            //Map(x => x.ID_DISCIPLINA);


            References(x => x.Disciplina, "ID_DISCIPLINA")
               .Fetch.Join()
               .Cascade.SaveUpdate();


            //HasMany(x => x.ListaLivros).KeyColumn("GUID_CONFIG").Fetch.Join();

            HasMany(x => x.ListaArquivos).Table("LV_TIPO").KeyColumns.Add("GUID_CONFIG").Cascade.All();//.Fetch.Join();

        }
    }
}
