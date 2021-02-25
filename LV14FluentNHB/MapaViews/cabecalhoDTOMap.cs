using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerificacaoListas.DTO;

namespace LV14FluentNHB.MapaViews
{
    public class CabecalhoDTOMap : ClassMap<CabecalhoDTO>
    {
        public CabecalhoDTOMap()
        {

            Table("LV_VIEW_PLANILHA");
            Id(x => x.GUID);
            Map(x => x.NOME);
            Map(x => x.DESCRICAO);
            Map(x => x.FUNCAO);
            Map(x => x.NOME_CONFIG);
            Map(x => x.NOME_DISCIPLINA);
            Map(x => x.NOME_TIPO);
            Map(x => x.SIGLA_DISCIPLINA);

        }
    }
}
