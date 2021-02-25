using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using LVModel.ObjetosValor;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaColunasTemplateRevisoes : ListaColunasTemplate
    {
        ListaVerificacao _documento;
        ListaRegsRevSession _listaRegsRevSession;

        List<ListaRegistrosPorColunas> _listaRevisoes;



        public ListaColunasTemplateRevisoes(ListaVerificacao documento, List<ListaRegistrosPorColunas> listaCadastradaRevisoes) : base(documento.Planilha)
        {
            _documento = documento;

            _listaColunaRevisaoDocumento = new List<ColunaRevisaoViewModel>();





            _listaRegsRevSession = new ListaRegsRevSession(_documento.Planilha);


         

            _listaRevisoes = listaCadastradaRevisoes;
 


            defColunas(); 



            setColunasRevisaoViewModel();
        }


        public List<ColunaRevisaoViewModel> ObtemLista_ColunaRevisaoDocumento(string guidDoc, bool isVerificador, List<Revisao> listaRevisoesDocumento)
        {



            //var listaRevisoesDocumento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>()
            //        .GetByProperty("GUID_DOC_VERIFICACAO", guidDoc).ToList();

            //List<EstadoRevisao> listaStatus = null;
            //using (var contextoConfirmacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<EstadoRevisao>>())
            //{
            //    contextoConfirmacao.Start();
            //    listaStatus = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<EstadoRevisao>>().Query().ToList();

            //}

            int ordenadorRevisoes = 0;

            foreach (var coluna in _listaColunaRevisaoDocumento)
            {

                if (NaoConfirmada(coluna) && isVerificador)
                {

                    var listaColuna = listaRevisoesDocumento.Where(x => x.ORDENADOR == ordenadorRevisoes).ToList();

                    foreach (var grupo in coluna.ListaGrupos)
                    {
                        foreach (var linha in grupo.ListaLinhas)
                        {
                            var rev = listaColuna.First(x => x.GUID_LV_ITEM == linha.GuidTipo);
                            linha.Status = StatusRevisao.ObtemStatusRevisao(rev.ID_ESTADO).Name;//listaStatus.First(x => x.ID_ESTADO == rev.ID_ESTADO).NOME;
                            linha.Confirmado = rev.CONFIRMADO < 1 ? false : true;
                            linha.Emitido = rev.EMITIDO < 1 ? false : true;
                            linha.Salvo = rev.SALVO < 1 ? false : true;
                            linha.GuidRevisao = rev.GUID;
                            linha.Guid = rev.GUID;
                        }
                    }

                    ordenadorRevisoes++;
                }
            }


            return _listaColunaRevisaoDocumento;

        }

       

        private static bool NaoConfirmada(ColunaRevisaoViewModel coluna)
        {
            return !(coluna.ListaGrupos.First().ListaLinhas.Where(x => x.Confirmado == false).Count() > 0);
        }

        private void defColunas()
        {


            for (int i = 0; i < _listaRevisoes.Count; i++)
            {
                _listaColunaRevisaoDocumento.Add(new ColunaRevisaoViewModel(
                    _listaRevisoes[i].IndiceRevisao,
                    _listaRevisoes[i].DataRevisaoExistente,
                    _listaRevisoes[i].NomeVerificadorRevisaoExistente,
                    _listaRevisoes[i].SiglaVerificadorRevisaoExistente,
                    _listaRevisoes[i].Ordenador,
                    _listaRevisoes[i].IdVerificador));
            }


            _listaColunaRevisaoDocumento.OrderBy(x => x.Ordenador);


        }

        private void setColunasRevisaoViewModel()//Template templateEscolhido)
        {

 
            foreach (var coluna in _listaColunaRevisaoDocumento)
            {
                foreach (var gp in _documento.Planilha.ListaGrupos.Distinct().OrderBy(x => x.ORDENADOR))//.Planilha.GetListaGrupos())//templateEscolhido.ListaGrupos)
                {




                    coluna.AddGrupo(new GrupoRevisoes(
                        gp.ORDENADOR.ToString(),
                        gp.NOME,
                        geraListaLinhas(gp.ListaItens.Distinct().OrderBy(x => x.ORDENADOR).ToList(), gp.ORDENADOR, coluna.IndiceRevisao)
                        ));
                }
            }



        }

        //private List<Grupo> listagemGrupos(List<ViewItensRev> listaItens)
        //{

        //    List<Grupo> listaGrupo = new List<Grupo>();

        //    var results = listaItens.GroupBy(
        //                    p => p.GUID_GRUPO,
        //                    p => p.ORDENADOR_GRUPO,
        //                    (key, g) => new { guid = key, ordenador = g });






        //    foreach (var grupo in results)
        //    {
        //        var listaItensGrupo = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ViewItensRev>>().GetByProperty("GUID_GRUPO", grupo.guid).ToList();
        //        //var listaItensGrupo = new Repository<LV_VIEW_ITENS_REV>().GetByProperty("GUID_GRUPO", grupo.guid) as List<LV_VIEW_ITENS_REV>;

        //        listaGrupo.Add(new Grupo(listaItensGrupo.First().ORDENADOR_GRUPO, listaItensGrupo.First().NOME_GRUPO, listaItensGrupo));

        //    }

        //    return listaGrupo;

        //}







    }
}