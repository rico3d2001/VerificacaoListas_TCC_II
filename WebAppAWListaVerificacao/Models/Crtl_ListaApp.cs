using AppExcel.AppWeb;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class Crtl_ListaApp// : ICrtlDocumento
    {
        //protected Documento _documento;
        ////private string _numeroDocumento;
        //protected string indiceRevCorrente;
        //protected int ordenadorRevCorrente;
        //protected bool totalmentePreenchidaUltimaListaRevisoes;



        //protected Confirmacao confirmador;

        //protected Template _planilhaEscolhida;
        //protected ListaColunasApp _listaColunasApp;
        //protected CabecalhoApp _cabecalhoApp;
        //protected ImagemStatusRevisaoApp _imagemStatusRevisaoApp;
        //protected ListaConfirmacoesDocumentoApp _listaConfirmacoesDocumentoApp;



        //public CrtlDocumentoApp(ImagemStatusRevisaoApp imagemStatusRevisaoApp, CabecalhoApp cabecalhoApp, ListaColunasApp listaColunasApp)
        //{
        //    _listaColunasApp = listaColunasApp;
        //    _cabecalhoApp = cabecalhoApp;
        //    _imagemStatusRevisaoApp = imagemStatusRevisaoApp;







        //    //listaRegistrosParaSalvar = new List<RegistroRevisao>();
        //    //this.navegador = navegador;
        //    //listaRevisoes = new List<ListaRegistrosPorColunas>();

        //    //if (planilhaEscolhida != null)
        //    //{
        //    //    _planilhaEscolhida = planilhaEscolhida;
        //    //}
        //}

        //public ImagemStatusViewModel GetImagemStatusViewModel()
        //{
        //    return _imagemStatusRevisaoApp.GetImagemStatusViewModel();
        //}



        ////internal List<ColunaRevisaoViewModel> GetListaColunasRevisaoViewModel()
        ////{
        ////    return _listaColunasApp.GetListaColunasRevisaoViewModel();
        ////}

        //public virtual CabecalhoViewModel GetCabecalhoViewModel()
        //{
        //    return _cabecalhoApp.GetCabecalhoViewModel();
        //}

        //public virtual List<ColunaRevisaoViewModel> GetListaColunasRevisaoViewModel()
        //{
        //    return _listaColunasApp.GetListaColunasRevisaoViewModel();
        //}











        ////public void DescarregaDocumento()
        ////{
        ////    _documento = null;
        ////}



        ////public string GetUltimoIndiceRevisaoLista()
        ////{
        ////    return this.listaRevisoes.Last().IndiceRevisao;
        ////}



        ////public virtual List<ConfirmacaoParaLista> GetListaConfimacaoParaLista()
        ////{
        ////    //_listaConfirmacoesDocumentoApp = new ListaConfirmacoesDocumentoApp(_documento.NumeroDocVerificado);

        ////    return null;
        ////}




        ////public bool UltimaRevisaoConfirmada()
        ////{
        ////    if (this.listaRevisoes.Count < 1) return false;

        ////    var listaOrdenada = this.listaRevisoes.OrderBy(x => x.Ordenador);

        ////    var lista = listaOrdenada.Last().ListaRegistros;

        ////    if (lista.FirstOrDefault(x => x.Confirmado == false) == null)
        ////    {
        ////        return true;
        ////    }

        ////    return false;
        ////}

        ////public void SalvaRegistros(DocumentoApp doc, UsuarioCorrente usuarioViewModel)
        ////{



        ////    if (this.documento == null)
        ////    {
        ////        this.documento =
        ////       new Documento(doc.NumeroDocumento, this.guidTipoItemCorrente);

        ////        this.documento.Abre();
        ////    }


        ////    foreach (var reg in this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros)
        ////    {
        ////        reg.SetDateTime(DateTime.Now);
        ////        reg.Verificador = usuarioViewModel.Sigla;
        ////        reg.PersisteDados(documento, usuarioViewModel.Guid);
        ////    };

        ////}



        ////internal bool Abre(DocViewModel doc)
        ////{
        ////    this.documento = new Documento(doc.NumeroDocumento, this.guidTipoDocCorrente);

        ////    this.documento.NumeroDocVerificado = doc.NumeroDocumento;

        ////    if (this.documento.Abre())
        ////    {
        ////        this.listaRevisoes = GetListaRevisoes();
        ////        return true;
        ////    }

        ////    return false;
        ////}

        ////public List<ListaRegistrosPorColunas> ListaRevisoes { get => this.listaRevisoes.OrderBy(x => x.Ordenador).ToList(); }



        ////public string GuidTipoDocCorrente { get => _planilhaEscolhida.Guid; }// set => _planilhaEscolhida.Guid = value; }
        ////public string GuidTipoItemCorrente { get => guidTipoItemCorrente; set => guidTipoItemCorrente = value; }
        ////public bool TotalmentePreenchidaUltimaListaRevisoes { get => totalmentePreenchidaUltimaListaRevisoes; set => totalmentePreenchidaUltimaListaRevisoes = value; }
        ////public Documento Documento { get => _documento; set => _documento = value; }
        ////public string IndiceRevCorrente { get => indiceRevCorrente; set => indiceRevCorrente = value; }
        ////public int OrdenadorRevCorrente { get => ordenadorRevCorrente; set => ordenadorRevCorrente = value; }





        ////public RegistroVerificacao RetornaRegistro(string guidTipo, string indiceRevisao)
        ////{
        ////    if (this.listaRevisoes.Count == 0)
        ////        return null;

        ////    return listaRevisoes.Find(x => x.IndiceRevisao == indiceRevisao).ListaRegistros.Find(x => x.GetGuidTipo() == guidTipo);
        ////}

        ////public bool ValidaRegistros()
        ////{
        ////    bool validados = false;

        ////    if (this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.Count < this.navegador.PlanilhaEscolhida.ContaItens())
        ////    {
        ////        return false;
        ////    }

        ////    foreach (var reg in this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros)
        ////    {
        ////        if (reg.TipoRevisao.Nome == "V" || reg.TipoRevisao.Nome == "ND" || reg.TipoRevisao.Nome == "NA" || reg.TipoRevisao.Nome == "X")
        ////        {
        ////            validados = true;
        ////        }
        ////        else
        ////        {
        ////            validados = false;
        ////            break;
        ////        }
        ////    };

        ////    return validados;
        ////}

        ////public bool IniciouListaRevisoes()
        ////{
        ////    return this.listaRevisoes.Count < 1 ? false : true;
        ////}

        ////public bool IniciouListaRegistros()
        ////{
        ////    return this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.Count() < 1 ? false : true;
        ////}

        ////public bool UltimaRevisaoEstaCompletaSalva()
        ////{
        ////    if (this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.Last().TipoRevisao != null)
        ////    {

        ////        if (this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.Last().IsSaved())
        ////        {
        ////            return true;
        ////        }
        ////    }

        ////    return false;
        ////}



        ////public bool CriaSeNaoExiste(DocViewModel doc)
        ////{
        ////    this.documento = new Documento(doc.NumeroDocumento, this.guidTipoDocCorrente);

        ////    this.documento.CriaSeNaoExisteDocumento();

        ////    if (this.documento == null || this.documento.GUID == "")
        ////        return false;

        ////    this.listaRevisoes = GetListaRevisoes();

        ////    return true;
        ////}

        ////private List<ListaRegistrosPorColunas> GetListaRevisoes()
        ////{
        ////    List<ListaRegistrosPorColunas> lista = new List<ListaRegistrosPorColunas>();

        ////    List<Revisao> listaRevisoes = Revisao.GetRevisoesDocumento(this.documento);

        ////    if (listaRevisoes.Count < 1)
        ////    {
        ////        return lista;
        ////    }

        ////    var queryIndices =
        ////        from rev in listaRevisoes
        ////        group rev by new { indice = rev.Indice, ordenador = rev.Ordenador };

        ////    foreach (var q in queryIndices.OrderBy(x => x.Key.ordenador))
        ////    {
        ////        lista.Add(new ListaRegistrosPorColunas(q.Key.indice, q.Key.ordenador));
        ////    }

        ////    foreach (var rev in lista)
        ////    {
        ////        var registros = listaRevisoes.Where(x => x.Indice == rev.IndiceRevisao);
        ////        foreach (var reg in registros)
        ////        {
        ////            rev.ListaRegistros.Add(new RegistroVerificacao(reg.GuidItemVerificacao, reg.Indice, reg.Ordenador, reg.GuidRev, reg.EstadoRevisao, reg.DataRev, reg.GuidUsuario, reg.Confirmado, reg.Salvo, reg.Emitido));
        ////        }
        ////    }

        ////    return lista;
        ////}

        ////public void IniciaListaRevisoes(string caracter)
        ////{
        ////this.listaRevisoes = new List<ListaRegistrosPorColunas>();

        ////int order = 0;

        ////this.indiceRevCorrente = caracter;

        ////this.listaRevisoes.Add(new ListaRegistrosPorColunas(caracter, order));

        ////this.ordenadorRevCorrente = this.listaRevisoes.Last().Ordenador;
        ////}

        ////public void AddRevisao(string caracter)
        ////{
        ////    var lastCol = this.listaRevisoes.OrderBy(x => x.Ordenador).Last();

        ////    int order = lastCol.Ordenador + 1;

        ////    this.indiceRevCorrente = caracter;

        ////    this.listaRevisoes.Add(new ListaRegistrosPorColunas(caracter, order));

        ////    this.ordenadorRevCorrente = this.listaRevisoes.OrderBy(x => x.Ordenador).Last().Ordenador;
        ////}

        ////internal string GetStatusRegistroUltimaRevisao(string guidTipoRev)
        ////{
        ////    int indicePenultima = (this.listaRevisoes.Count() - 2);
        ////    var penultimaListaRegistros = this.listaRevisoes[indicePenultima].ListaRegistros;

        ////    string status = penultimaListaRegistros.Find(x => x.GetGuidTipo().Equals(guidTipoRev)).TipoRevisao.Nome;

        ////    return status;
        ////}





        ////internal bool UltimosRegistrosCopiaveis(List<RegistroRevisao> listaRegistrosView)
        ////{
        ////    int indicePenultima = (this.listaRevisoes.Count() - 2);
        ////    var penultimaListaRegistros = this.listaRevisoes[indicePenultima].ListaRegistros;

        ////    return penultimaListaRegistros.Count() == listaRegistrosView.Count ? true : false;
        ////}

        ////internal void ConfirmaUltimaListaRegistros(List<RegistroRevisao> listaRegistrosView)
        ////{

        ////    if (confirmador.PermiteConfirmarRegistros())
        ////    {

        ////        var l = this.listaRevisoes.FirstOrDefault(x => x.IndiceRevisao == this.indiceRevCorrente).ListaRegistros;

        ////        if (l.Count < 1)
        ////        {
        ////            for (int i = 0; i < listaRegistrosView.Count; i++)
        ////            {
        ////                var registroVerificacaoViewModel =
        ////                    new RegistroVerificacao(listaRegistrosView[i].GuidTipoRev, this.indiceRevCorrente, 0, "");

        ////                registroVerificacaoViewModel.GuidVerificacao = listaRegistrosView[i].Guid;
        ////                registroVerificacaoViewModel.Confirmado = true;
        ////                registroVerificacaoViewModel.Emitido = listaRegistrosView[i].Emitido;
        ////                registroVerificacaoViewModel.Salvo = listaRegistrosView[i].Salvo;
        ////                registroVerificacaoViewModel.TipoRevisao = new EstadoRevisao(listaRegistrosView[i].Status);

        ////                l.Add(registroVerificacaoViewModel);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            for (int i = 0; i < listaRegistrosView.Count; i++)
        ////            {
        ////                l[i].GuidVerificacao = listaRegistrosView[i].Guid;
        ////                l[i].Confirmado = true;
        ////                l[i].Emitido = listaRegistrosView[i].Emitido;
        ////                l[i].Salvo = listaRegistrosView[i].Salvo;
        ////                l[i].TipoRevisao = new EstadoRevisao(listaRegistrosView[i].Status);
        ////            }
        ////        }

        ////    }
        ////}

        ////internal void MudaStatusNoDoc(Revisao revisao, string status)
        ////{

        ////    Documento doc = new Documento(this.NumeroDocumento);
        ////    var lvRevs = new Repository<LV_REVISAO>().GetByProperty("GUID_DOC_VERIFICACAO", doc.GUID);

        ////    var agrupado = lvRevs.GroupBy(x => x.ORDENADOR).ToList().OrderBy(x => x.Key);

        ////    this.listaRevisoes = new List<ListaRegistrosPorColunas>();

        ////    foreach (var g in agrupado)
        ////    {
        ////        var ind = g.First().INDICE;
        ////        this.listaRevisoes.Add(new ListaRegistrosPorColunas(ind,g.Key));

        ////        foreach (var v in g)
        ////        {
        ////            this.listaRevisoes.Last().AddRegistro(new RegistroVerificacao(v));
        ////        }


        ////    }

        ////    var lt = this.listaRevisoes.FirstOrDefault(x => x.IndiceRevisao == this.indiceRevCorrente).ListaRegistros;

        ////    if (lt.Count < 1)
        ////    {

        ////        Documento doc1 = new Documento(this.NumeroDocumento);

        ////        var lvRevs1 = new Repository<LV_REVISAO>().GetByProperty("GUID_DOC_VERIFICACAO", doc.GUID);


        ////        List<RegistroVerificacao> registroVerificacaos = new List<RegistroVerificacao>();

        ////        var listaRevsColuna = lvRevs.Where(x => x.INDICE.Equals(this.indiceRevCorrente));

        ////        foreach (var item in listaRevsColuna)
        ////        {
        ////            this.listaRevisoes.FirstOrDefault(x => x.IndiceRevisao == this.indiceRevCorrente).AddRegistro(new RegistroVerificacao(item.GUID, item.INDICE, item.ORDENADOR, item.GUID));
        ////        }





        ////    }

        ////    var l = this.listaRevisoes.FirstOrDefault(x => x.IndiceRevisao == this.indiceRevCorrente).ListaRegistros;


        ////    if (l.Count < 1)
        ////    {



        ////        var registroVerificacaoViewModel =
        ////            new RegistroVerificacao(revisao.GuidItemVerificacao, this.indiceRevCorrente, revisao.Ordenador,revisao.GuidRev);

        ////        registroVerificacaoViewModel.GuidVerificacao = revisao.GuidRev;
        ////        registroVerificacaoViewModel.Confirmado = revisao.Confirmado == 0 ? false : true;
        ////        registroVerificacaoViewModel.Emitido = revisao.Emitido == 0 ? false : true;
        ////        registroVerificacaoViewModel.Salvo = revisao.Salvo == 0 ? false : true;
        ////        registroVerificacaoViewModel.TipoRevisao = new EstadoRevisao(status);

        ////        l.Add(registroVerificacaoViewModel);
        ////    }
        ////    else
        ////    {
        ////        var revviwe = l.FirstOrDefault(x => x.GuidVerificacao == revisao.GuidRev);

        ////        if (revviwe != null)
        ////        {
        ////            revviwe.GuidVerificacao = revisao.GuidRev;
        ////            revviwe.Confirmado = revisao.Confirmado == 0 ? false : true;
        ////            revviwe.Emitido = revisao.Emitido == 0 ? false : true;
        ////            revviwe.Salvo = revisao.Salvo == 0 ? false : true;
        ////            revviwe.TipoRevisao = new EstadoRevisao(status);
        ////        }
        ////        else
        ////        {
        ////            var registroVerificacaoViewModel =
        ////                new RegistroVerificacao(revisao.GuidItemVerificacao, this.indiceRevCorrente, revisao.Ordenador, revisao.GuidRev);

        ////            registroVerificacaoViewModel.GuidVerificacao = revisao.GuidRev;
        ////            registroVerificacaoViewModel.Confirmado = revisao.Confirmado == 0 ? false : true;
        ////            registroVerificacaoViewModel.Emitido = revisao.Emitido == 0 ? false : true;
        ////            registroVerificacaoViewModel.Salvo = revisao.Salvo == 0 ? false : true;
        ////            registroVerificacaoViewModel.TipoRevisao = new EstadoRevisao(status);

        ////            l.Add(registroVerificacaoViewModel);
        ////        }
        ////    }
        ////}

        ////internal bool TodosConfirmadosNoBanco()
        ////{
        ////    return Documento.ConfirmaConfirmados(this.NumeroDocumento);
        ////}

        ////internal void SalvaRegistros()
        ////{
        ////    Documento.SalvaConfirmados(this.NumeroDocumento);
        ////}

        ////private void atualizaConfirmacaoRev(string guidRev)
        ////{
        ////    var l = this.listaRevisoes.OrderBy(x => x.Ordenador).Last().ListaRegistros.FirstOrDefault(x => x.GuidVerificacao == guidRev);
        ////    l.Confirmado = true;
        ////}

        ////internal void AddRevisoesDocumento(List<RegistroRevisao> listaRegistrosView)
        ////{
        ////    var l = this.listaRevisoes.FirstOrDefault(x => x.IndiceRevisao == this.indiceRevCorrente).ListaRegistros;

        ////    if (l.Count < 1)
        ////    {
        ////        for (int i = 0; i < listaRegistrosView.Count; i++)
        ////        {
        ////            var registroVerificacaoViewModel =
        ////                new RegistroVerificacao(listaRegistrosView[i].GuidTipoRev, this.indiceRevCorrente, 0, "");

        ////            registroVerificacaoViewModel.GuidVerificacao = listaRegistrosView[i].Guid;
        ////            registroVerificacaoViewModel.Confirmado = false;
        ////            registroVerificacaoViewModel.Emitido = listaRegistrosView[i].Emitido;
        ////            registroVerificacaoViewModel.Salvo = listaRegistrosView[i].Salvo;
        ////            registroVerificacaoViewModel.TipoRevisao = new EstadoRevisao(listaRegistrosView[i].Status);

        ////            l.Add(registroVerificacaoViewModel);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        for (int i = 0; i < listaRegistrosView.Count; i++)
        ////        {
        ////            l[i].GuidVerificacao = listaRegistrosView[i].Guid;
        ////            l[i].Confirmado = false;
        ////            l[i].Emitido = listaRegistrosView[i].Emitido;
        ////            l[i].Salvo = listaRegistrosView[i].Salvo;
        ////            l[i].TipoRevisao = new EstadoRevisao(listaRegistrosView[i].Status);

        ////        }
        ////    }
        ////}

        ////public void AtualizaIndiceRevisaoCorrenteConformeUltima()
        ////{
        ////    this.indiceRevCorrente = this.listaRevisoes.Last().IndiceRevisao;
        ////}






    }
}