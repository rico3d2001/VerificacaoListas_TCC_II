using FluentNHibernate.Mapping;
using LVModel;

namespace LV14FluentNHB
{
    public class RevisaoMap : ClassMap<Revisao>
    {
        public RevisaoMap()
        {
            Table("LV_REVISAO");
            Id(x => x.GUID);
            Map(x => x.GUID_LV_ITEM);
            Map(x => x.GUID_LV_VERIFICADOR);
            Map(x => x.DATA_VERICACAO);
            Map(x => x.ID_ESTADO);
            Map(x => x.GUID_DOC_VERIFICACAO);
            Map(x => x.INDICE);
            Map(x => x.ORDENADOR);
            Map(x => x.CONFIRMADO);
            Map(x => x.SALVO);
            Map(x => x.EMITIDO);
            Map(x => x.GUID_CONFIRMADO);

            //References(x => x.Disciplina, "ID_DISCIPLINA").Fetch.Join().Cascade.SaveUpdate();


        }
    }
}
