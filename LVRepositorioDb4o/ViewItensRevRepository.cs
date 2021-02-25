using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class ViewItensRevRepository: IViewListRepository<LV_VIEW_ITENS_REV>
    {


        public ViewItensRevRepository()
        {

        }


        public IList<LV_VIEW_ITENS_REV> GetByProperty(object value)
        {

            IList<LV_VIEW_ITENS_REV> lV_VIEW_ITENS_REVs = new List<LV_VIEW_ITENS_REV>();

            string guidPlanilha = value.ToString();
            var grupos = new RepositoryGUID<LV_GRUPO>().GetByProperty("GUID_PLANILHA", guidPlanilha);

            foreach (var grupo in grupos)
            {

                var itens = new RepositoryGUID<LV_ITEM_REVISAO>().GetByProperty("GUID_GRUPO", grupo.GUID);

                foreach (var item in itens)
                {

                    LV_VIEW_ITENS_REV view = new LV_VIEW_ITENS_REV();

                    view.DESCRICAO = item.DESCRICAO;
                    view.GUID_GRUPO = grupo.GUID;
                    view.GUID_ITEM = item.GUID;
                    view.GUID_PLANILHA = guidPlanilha;
                    view.NOME_GRUPO = grupo.NOME;
                    view.ORDENADOR_GRUPO = grupo.ORDENADOR;
                    view.ORDENADOR_ITEM = item.ORDENADOR;

                    lV_VIEW_ITENS_REVs.Add(view);

                }
 

            }

            return lV_VIEW_ITENS_REVs;
        }
    }
}
