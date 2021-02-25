using AppExcel.AppWeb;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaColunasApp
    {

        ////ListaRegsRevSession _listaRegsRevSession;
        ////Template _templateEscolhido;
        ////ListaColunasRevisaoViewModel _listaColunasRevisaoViewModel;
        ////List<ColunaRevisaoViewModel> _listaColunasRevisaoViewModel;
        ////ListaRevisoesDBPorColunas _listaRevisoes;

        ////public List<ColunaRevisaoViewModel> ListaColunasRevisaoViewModel { get => _listaColunasRevisaoViewModel; }

        ////public ListaColunasApp(Template templateEscolhido, string guidUsuario, string numeroDocumento)// ListaRegsRevSession listaRegsRevSession)//, ListaColunas listaRevisoes)
        ////{

        ////    _listaRevisoes = new ListaColunas(templateEscolhido, numeroDocumento);

        ////    _templateEscolhido = templateEscolhido;
        ////    _listaRegsRevSession = new ListaRegsRevSession(_templateEscolhido, guidUsuario);
        ////    //_listaRevisoes = listaRevisoes;
        ////    _listaColunasRevisaoViewModel = new List<ColunaRevisaoViewModel>();
        ////}

        ////public ListaColunasApp(Template templateEscolhido, string guidUsuario)
        ////{
        ////    _listaRegsRevSession = new ListaRegsRevSession(templateEscolhido, guidUsuario);

        ////}

        //public List<ColunaRevisaoViewModel> GetListaColunasRevisaoViewModel(Template templateEscolhido)
        //    {

        //        List<ColunaRevisaoViewModel> listaColunasRevisaoViewModel = new List<ColunaRevisaoViewModel>();

        //        listaSemRevisoes(templateEscolhido, listaColunasRevisaoViewModel);

        //        return listaColunasRevisaoViewModel;
        //    }


        //    public List<ColunaRevisaoViewModel> GetListaColunasRevisaoViewModel(Template templateEscolhido, string numeroDocumento)//, ListaRegsRevSession listaRegsRevSession)
        //    {
        //        List<ColunaRevisaoViewModel> listaColunasRevisaoViewModel = new List<ColunaRevisaoViewModel>();

        //        var listaRegsRevSession = new ListaRegsRevSession(templateEscolhido);

        //        //var listaRevisoes = new ListaRevisoesDBPorColunas(numeroDocumento); //templateEscolhido, numeroDocumento);

        //        //var listaRegistrosPorColunas = new List<ListaRegistrosPorColunas>();

        //        //ListaColunas listaRegistrosPorColunas = new ListaColunas(_templateEscolhido);

        //        //if (!string.IsNullOrEmpty(numeroDocumento))//documento.DocumentoCarregado())
        //        //{
        //        //listaRegistrosPorColunas = listaCarregar; //documento.ListaRevisoes;

        //        if (listaRevisoes.IsVazia())//isListaColunasVazia)//listaRegistrosPorColunas.Vazia())//listaRegistrosPorColunas.Count < 1)
        //        {
        //            listaSemRevisoes(templateEscolhido, listaColunasRevisaoViewModel);
        //        }
        //        else
        //        {

        //            //setListaRevisoesColunasViewModel(listaRegistrosPorColunas);
        //            defColunas(listaRevisoes, listaColunasRevisaoViewModel);//listaRegistrosPorColunas);

        //            setColunasRevisaoViewModel(listaColunasRevisaoViewModel, templateEscolhido);
        //            //UA
        //            for (int i = 0; i < listaRevisoes.GetUltimaColuna().ListaRegistros.Count; i++)//documento.ListaRevisoes.Last().ListaRegistros.Count; i++)
        //            {
        //                listaRegsRevSession[i].Guid = listaRevisoes.GetUltimaColuna().ListaRegistros[i].GuidVerificacao;//documento.ListaRevisoes.Last().ListaRegistros[i].GuidVerificacao;
        //                if (listaRegsRevSession[i].Confirmado && !listaRevisoes.GetUltimaColuna().ListaRegistros[i].Confirmado)//documento.ListaRevisoes.Last().ListaRegistros[i].Confirmado)
        //                {
        //                    //documento.ListaRevisoes.Last().ListaRegistros[i].Confirmado = listaRegsRevSession[i].Confirmado;
        //                    listaRevisoes.GetUltimaColuna().ListaRegistros[i].Confirmado = listaRegsRevSession[i].Confirmado;
        //                }
        //                else
        //                {
        //                    //listaRegsRevSession[i].Confirmado = documento.ListaRevisoes.Last().ListaRegistros[i].Confirmado;
        //                    listaRegsRevSession[i].Confirmado = listaRevisoes.GetUltimaColuna().ListaRegistros[i].Confirmado;
        //                }

        //                //listaRegsRevSession[i].Emitido = documento.ListaRevisoes.Last().ListaRegistros[i].Emitido;
        //                //listaRegsRevSession[i].Salvo = documento.ListaRevisoes.Last().ListaRegistros[i].Salvo;
        //                //listaRegsRevSession[i].Status = documento.ListaRevisoes.Last().ListaRegistros[i].GetLetraStatus();
        //                listaRegsRevSession[i].Emitido = listaRevisoes.GetUltimaColuna().ListaRegistros[i].Emitido;
        //                listaRegsRevSession[i].Salvo = listaRevisoes.GetUltimaColuna().ListaRegistros[i].Salvo;
        //                listaRegsRevSession[i].Status = listaRevisoes.GetUltimaColuna().ListaRegistros[i].GetLetraStatus();

        //                //var lastCol = _listaColunasRevisaoViewModel.UltimaListaGrupos;//.Last();

        //                foreach (var grp in listaColunasRevisaoViewModel.Last().ListaGrupos)
        //                {
        //                    //var resp = grp.ListaLinhas.FirstOrDefault(x => x.GuidTipo == documento.ListaRevisoes.Last().ListaRegistros[i].GetGuidTipo());
        //                    var resp = grp.ListaLinhas.FirstOrDefault(x => x.GuidTipo == listaRevisoes.GetUltimaColuna().ListaRegistros[i].GetGuidTipo());
        //                    if (resp != null)
        //                    {
        //                        //resp.GuidRevisao = documento.ListaRevisoes.Last().ListaRegistros[i].GuidVerificacao;
        //                        resp.GuidRevisao = listaRevisoes.GetUltimaColuna().ListaRegistros[i].GuidVerificacao;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        //}
        //        //else
        //        //{
        //        //    listaSemRevisoes(templateEscolhido);
        //        //}

        //        return listaColunasRevisaoViewModel;
        //    }

        //    public List<ColunaRevisaoViewModel> GetListaColunasRevisaoViewModel()
        //    {
        //        List<ColunaRevisaoViewModel> listaColunasRevisaoViewModel = new List<ColunaRevisaoViewModel>();


        //        var linhas1 = new List<LinhaRevisao>()
        //    {
        //        new LinhaRevisao("1.1","Descricao item","null","0")
        //    };

        //        if (listaColunasRevisaoViewModel.Count == 0) //planilhaViewModel.ListaColunas.Count == 0)
        //        {
        //            listaColunasRevisaoViewModel.Add(new ColunaRevisaoViewModel("", "00/00/00", "XXX", "XXX", 0, "XXX"));

        //        }

        //        foreach (var coluna in listaColunasRevisaoViewModel)
        //        {
        //            coluna.AddGrupo(new GrupoRevisoes("1", "NOME GRUPO", linhas1));
        //        }

        //        return listaColunasRevisaoViewModel;
        //    }

        //    //public List<ColunaRevisaoViewModel> GetListaColunasRevisaoViewModel(List<ColunaRevisaoViewModel> listaColunasRevisaoViewModel)
        //    //{
        //    //    var linhas1 = new List<LinhaRevisao>()
        //    //    {
        //    //        new LinhaRevisao("1.1","Descricao item","null","0")
        //    //    };

        //    //    if (listaColunasRevisaoViewModel.Count == 0) //planilhaViewModel.ListaColunas.Count == 0)
        //    //    {
        //    //        listaColunasRevisaoViewModel.Add(new ColunaRevisaoViewModel("", "00/00/00", "XXX", "XXX", 0, "XXX"));

        //    //    }

        //    //    foreach (var coluna in listaColunasRevisaoViewModel)
        //    //    {
        //    //        coluna.AddGrupo(new GrupoRevisoes("1", "NOME GRUPO", linhas1));
        //    //    }

        //    //    return listaColunasRevisaoViewModel;


        //    //}



        //    private void setColunasRevisaoViewModel(List<ColunaRevisaoViewModel> listaColunasRevisaoViewModel, Template templateEscolhido)
        //    {
        //        foreach (var coluna in listaColunasRevisaoViewModel)
        //        {
        //            foreach (var gp in templateEscolhido.ListaGrupos)
        //            {
        //                coluna.AddGrupo(new GrupoRevisoes(
        //                    gp.ORDENADOR.ToString(),
        //                    gp.NOME,
        //                    geraListaLinhas(gp.GetListaItens(), gp.ORDENADOR, coluna.IndiceRevisao)
        //                    ));
        //            }
        //        }
        //    }

        //    private List<LinhaRevisao> geraListaLinhas(List<ItemVerificacao> listaItens, int ordenadorGrupo, string indiceRevisao)
        //    {
        //        List<LinhaRevisao> lista = new List<LinhaRevisao>();

        //        foreach (var ln in listaItens)
        //        {
        //            string itemLinha = ordenadorGrupo.ToString() + "." + ln.ORDENADOR.ToString();
        //            lista.Add(new LinhaRevisao(itemLinha, ln.DESCRICAO, ln.GUID, indiceRevisao));
        //        }

        //        return lista;
        //    }

        //    private void defColunas(ListaRevisoesDBPorColunas listaParaColunas, List<ColunaRevisaoViewModel> listaColunasRevisaoViewModel)//List<ListaRegistrosPorColunas> listaParaColunas)
        //    {


        //        for (int i = 0; i < listaParaColunas.Comprimento; i++)
        //        {
        //            listaColunasRevisaoViewModel.Add(new ColunaRevisaoViewModel(
        //                listaParaColunas[i].IndiceRevisao,
        //                listaParaColunas[i].DataRevisaoExistente,
        //                listaParaColunas[i].NomeVerificadorRevisaoExistente,
        //                listaParaColunas[i].SiglaVerificadorRevisaoExistente,
        //                listaParaColunas[i].Ordenador,
        //                listaParaColunas[i].IdVerificador));
        //        }


        //        listaColunasRevisaoViewModel.OrderBy(x => x.Ordenador);


        //    }

        //    private void listaSemRevisoes(Template planilha, List<ColunaRevisaoViewModel> listaColunasRevisaoViewModel)
        //    {
        //        listaColunasRevisaoViewModel.Add(new ColunaRevisaoViewModel("0", "00/00/00", "XXX", "XXX", 0, "XXX"));

        //        foreach (var coluna in listaColunasRevisaoViewModel)
        //        {
        //            foreach (var gp in planilha.ListaGrupos)
        //            {
        //                coluna.AddGrupo(new GrupoRevisoes(
        //                    gp.ORDENADOR.ToString(),
        //                    gp.NOME,
        //                    geraListaLinhas(gp.GetListaItens(), gp.ORDENADOR, coluna.IndiceRevisao)
        //                    ));
        //            }
        //        }
        //    }







        }
    
}