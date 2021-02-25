using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class ListaVerificacaoMap : ClassMap<ListaVerificacao>
    {
        public ListaVerificacaoMap()
        {
            Table("LV_DOC");
            Id(x => x.GUID);
            Map(x => x.NUMERO);
            //Map(x => x.OBJETO);
            Map(x => x.DOC_VERIFICADO);
            //Map(x => x.GUID_PROJETO);

            References(x => x.Projeto, "GUID_PROJETO")
             .Fetch.Join()
             .Cascade.SaveUpdate();

            References(x => x.Planilha, "OBJETO")
                .Fetch.Join()
             .Cascade.SaveUpdate();

          


            //HasMany(x => x.ListaColunasRevisao).KeyColumn("GUID_DOCUMENTO").Fetch.Join(); 

            HasMany(x => x.ListaConfirmacoes).Table("LV_CONFIRMACAO").KeyColumns.Add("GUID_DOCUMENTO").Cascade.All();//.Fetch.Join();

            HasMany(x => x.ListaRevisoes).Table("LV_REVISAO").KeyColumns.Add("GUID_DOC_VERIFICACAO").Cascade.All();//.Fetch.Join();



        }
    }
}
