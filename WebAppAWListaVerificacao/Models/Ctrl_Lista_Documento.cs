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
    public class Ctrl_Lista_Documento //: Crtl_Lista_Template, ICrtlLista
    {



        //public Ctrl_Lista_Documento(
        //    ImagemStatusRevisaoApp imagemStatusRevisaoApp, CabecalhoApp cabecalhoApp,
        //    ListaColunasApp listaColunasApp, Template planilhaEscolhida, NumeroDocSNCLavalin numeroDocumento) :
        //    base(imagemStatusRevisaoApp, cabecalhoApp, listaColunasApp, planilhaEscolhida)
        //{


        //    _documento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Documento>>().GetByProperty("DOC_VERIFICADO", numeroDocumento).FirstOrDefault();
        //    //new Documento(numeroDocumento);

        //    _listaConfirmacoesDocumentoApp = new ListaConfirmacoesDocumentoApp(numeroDocumento);//documentoSession.NumeroDocumento);

        //    _planilhaViewModel.ListaColunasRevisaoViewModel = listaColunasApp.GetListaColunasRevisaoViewModel(navegadorSession.PlanilhaEscolhida, numeroDocumento);//documentoSession.NumeroDocumento);
        //    //_planilhaViewModel.CabecalhoViewModel = cabecalhoApp.GetCabecalhoViewModel(navegadorSession.PlanilhaEscolhida, numeroDocumento); //documentoSession.NumeroDocumento);

        //    //var listaConf = Mapper.Map<IEnumerable<ConfirmacaoViewModel>>(listaConfirmacoesDocumentoApp.ListaConfimacaoParaLista);

        //    //planilhaViewModel.ListaRegistroConfirmacoesViewModel = listaConf;



        //}

        ////public bool SetPlanilha() //DocViewModel doc, Template template)
        ////{
        ////    //var documento = new Documento(_numeroDocumento, _planilhaEscolhida.Guid);


        ////    //mudar
        ////    //_documento.NumeroDocVerificado = _numeroDocumento;

        ////    //if (_documento.Abre())
        ////    //{
        ////    //this.listaRevisoes = GetListaRevisoes();
        ////    //return true;
        ////    //}

        ////    //return false;

        ////    return _documento.Abre();


        ////}

        //public bool DocumentoCarregado()
        //{
        //    if (_documento == null)
        //        return false;

        //    return true;
        //}

        //public void DescarregaDocumento()
        //{
        //    _documento = null;

        //}

        ////public string NumeroDocumento
        ////{
        ////    get
        ////    {
        ////        if (_documento == null)
        ////        {
        ////            return null;
        ////        }

        ////        return _documento.NumeroDocVerificado;
        ////    }
        ////}

        ////internal void SalvaRegistros()
        ////{
        ////    Documento.SalvaConfirmados(this.NumeroDocumento);
        ////}

        ////internal bool TodosConfirmadosNoBanco()
        ////{
        ////    return Documento.ConfirmaConfirmados(this.NumeroDocumento);
        ////}

        ////public bool CriaSeNaoExiste(DocViewModel doc, Template template)//string guidTipoDocCorrente)
        ////{
        ////    _documento = new Documento(doc.NumeroDocumento, template.Guid);

        ////    _documento.CriaSeNaoExisteDocumento();

        ////    if (_documento == null || _documento.GUID == "")
        ////        return false;

        ////    string naoendoIsto = "";
        ////    //this.listaRevisoes = GetListaRevisoes();

        ////    return true;
        ////}

        ////public bool CriaDocumento(DocViewModel doc)
        ////{
        ////    _documento = new Documento(doc.NumeroDocumento, _planilhaEscolhida.Guid);
        ////    return _documento.Cria();
        ////}

        //public void AtualizaIndiceRevisaoCorrenteConformeUltima(ListaRevisoesDBPorColunas listaColunas)
        //{
        //    //this.indiceRevCorrente = this.listaRevisoes.Last().IndiceRevisao;
        //    this.indiceRevCorrente = listaColunas.GetUltimoIndice();
        //}

        //public ListaRegistrosPorColunas AddRevisao(string caracter, ListaRegistrosPorColunas lastCol)
        //{
        //    //var lastCol = listaColunas.GetUltimaColuna(); //this.listaRevisoes.OrderBy(x => x.Ordenador).Last();

        //    int order = lastCol.Ordenador + 1;

        //    this.indiceRevCorrente = caracter;

        //    //this.listaRevisoes.Add(new ListaRegistrosPorColunas(caracter, order));


        //    //this.ordenadorRevCorrente = this.listaRevisoes.OrderBy(x => x.Ordenador).Last().Ordenador;
        //    this.ordenadorRevCorrente = lastCol.Ordenador;


        //    return new ListaRegistrosPorColunas(caracter, order);
        //}


        //public void IniciaRevisao(string caracter)
        //{


        //    //ListaColunas listaColunas = new ListaColunas();

        //    int order = 0;

        //    this.indiceRevCorrente = caracter;


        //    //listaColunas.Add(new ListaRegistrosPorColunas(caracter, order));

        //    this.ordenadorRevCorrente = order;

        //    //return listaColunas;
        //}


        ////public override List<ColunaRevisaoViewModel> GetListaColunasRevisaoViewModel()
        ////{
        ////    return _listaColunasApp.GetListaColunasRevisaoViewModel(_planilhaEscolhida,_documento.NumeroDocVerificado);
        ////}

        ////public override CabecalhoViewModel GetCabecalhoViewModel()
        ////{
        ////    return _cabecalhoApp.GetCabecalhoViewModel(_planilhaEscolhida,_documento.NumeroDocVerificado);
        ////}


        ////public override List<ConfirmacaoParaLista> GetListaConfimacaoParaLista()
        ////{
        ////    _listaConfirmacoesDocumentoApp = new ListaConfirmacoesDocumentoApp(_documento.NumeroDocVerificado);

        ////    return _listaConfirmacoesDocumentoApp.ListaConfimacaoParaLista;
        ////}


        ////public bool TotalmentePreenchidaUltimaListaRevisoes { get => totalmentePreenchidaUltimaListaRevisoes; set => totalmentePreenchidaUltimaListaRevisoes = value; }
        ////public Documento Documento { get => _documento; set => _documento = value; }
        ////public string IndiceRevCorrente { get => indiceRevCorrente; set => indiceRevCorrente = value; }
        ////public int OrdenadorRevCorrente { get => ordenadorRevCorrente; set => ordenadorRevCorrente = value; }
    }
}