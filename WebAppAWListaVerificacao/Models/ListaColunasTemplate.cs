using AppExcel.AppWeb;
using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaColunasTemplate
    {
        protected Planilha _planilha;
        protected List<ColunaRevisaoViewModel> _listaColunaRevisaoDocumento;
      

        public ListaColunasTemplate(Planilha planilha)
        {
            
            _planilha = planilha;

            _listaColunaRevisaoDocumento = new List<ColunaRevisaoViewModel>();


        }



        public List<ColunaRevisaoViewModel> ObtemLista_ColunaRevisaoDocumento()
        {
            _listaColunaRevisaoDocumento.Add(new ColunaRevisaoViewModel("0", "00/00/00", "XXX", "XXX", 0, "XXX"));

            foreach (var coluna in _listaColunaRevisaoDocumento)
            {
                foreach (var gp in _planilha.ListaGrupos.Distinct().OrderBy(x => x.ORDENADOR))
                {


                    coluna.AddGrupo(new GrupoRevisoes(
                        gp.ORDENADOR.ToString(),
                        gp.NOME,
                        geraListaLinhas(gp.ListaItens.Distinct().OrderBy(x => x.ORDENADOR).ToList(), gp.ORDENADOR, coluna.IndiceRevisao)
                        ));
                }
            }

            return _listaColunaRevisaoDocumento;
        }

       

     

        protected List<LinhaRevisao> geraListaLinhas(List<ItemRevisao> listaItens, int ordenadorGrupo, string indiceRevisao)
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