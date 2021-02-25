using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace LV_PresenterAPI.Service
{
    public class ListaColunasTemplate
    {
        protected PlanilhaLVVM _planilha;
        protected List<ColunaRevisaoViewModel> _listaColunaRevisaoDocumento;


        public ListaColunasTemplate(PlanilhaLVVM planilha)
        {

            _planilha = planilha;

            _listaColunaRevisaoDocumento = new List<ColunaRevisaoViewModel>();


        }



        public List<ColunaRevisaoViewModel> ObtemLista_ColunaRevisaoDocumento()
        {
            _listaColunaRevisaoDocumento.Add(new ColunaRevisaoViewModel("0", "00/00/00", "XXX", "XXX", 0, "XXX"));

            foreach (var coluna in _listaColunaRevisaoDocumento)
            {
                foreach (var gp in _planilha.Grupos.OrderBy(x => x.ORDENADOR))
                {


                    coluna.AddGrupo(new GrupoRevisoes(
                        gp.ORDENADOR.ToString(),
                        gp.NOME,
                        geraListaLinhas(gp.Itens.OrderBy(x => x.ORDENADOR).ToList(), gp.ORDENADOR, coluna.IndiceRevisao)
                        ));
                }
            }

            return _listaColunaRevisaoDocumento;
        }





        protected List<LinhaRevisao> geraListaLinhas(List<ItemVM> listaItens, int ordenadorGrupo, string indiceRevisao)
        {
            List<LinhaRevisao> lista = new List<LinhaRevisao>();

            foreach (var ln in listaItens)
            {
                string itemLinha = ordenadorGrupo.ToString() + "." + ln.ORDENADOR.ToString();
                lista.Add(new LinhaRevisao(itemLinha, ln.DESCRICAO, ln.GUID, indiceRevisao));
            }

            return lista;
        }


    }
}