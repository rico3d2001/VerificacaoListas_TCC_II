using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System.Linq;
using Unity;

namespace AppExcel.AppWeb
{
    public class ConfirmaRevisaoApp
    {
        private DocumentoApp _documentoApp;
        //private ListaConfirmacoes listaConfirmacoes;
        //private Confirmacao ultimaConfirmacao;
        //private List<Revisao> ultimaListaRevisoes;
        private string indiceRevCorrente;
        private int ordenadorCorrente;
        //private readonly bool isDefined;

        public ConfirmaRevisaoApp(DocumentoApp documentoApp)//, string indiceRevCorrente)
        {
            _documentoApp = documentoApp;
            ////this.ultimaConfirmacao = new ListaConfirmacoes(numeroDocumento).GetUltimaConfirmacao();



            ////if (!string.IsNullOrEmpty(indiceRevCorrente)
            ////    {




            ////this.documento = new Documento(numeroDocumento);

            ////this.indiceRevCorrente = indiceRevCorrente; //this.documento.GetUltimoIndiceRevisao();

            ////this.indiceRevCorrente = documento.GetUltimoIndiceRevisao();

            ////this.documento = new Documento(numeroDocumento);


            //IAppServiceBase<Revisao> appRevisaoService = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>();



            //var lvRev = appRevisaoService.GetByProperty("GUID_DOC_VERIFICACAO", documento.GUID);

            //var lvOrdenado = lvRev.OrderBy(x => x.ORDENADOR);

            //var rev = lvOrdenado.Last();


            //this.ordenadorCorrente = rev.ORDENADOR;
            //this.indiceRevCorrente = rev.INDICE;


            ////this.indiceRevCorrente = string.Empty;





            ////this.isDefined = true;
            ////this.ultimaListaRevisoes = this.documento.GetUltimaListaRevisoes();

            ////this.documento.IndiceRevCorrente = doc.ListaRevisoes.Last().IndiceRevisao;
            ////}
            ////else
            ////{
            ////    isDefined = false;
            //}



        }

        //public bool IsDefined()
        //{
        //    return !string.IsNullOrEmpty(this.indiceRevCorrente);
        //}

        //public bool PodeConfirmar(Documento documento)
        //{

        //    if (string.IsNullOrEmpty(this.indiceRevCorrente)) return false;



        //    //this.documento.SetByNumeroDoc();

        //    return documento.UltimaRevisaoPronta(this.indiceRevCorrente);





        //}

        //public void CriaConfirmacao(string guidUsuario, string guidPlalnilha, Documento documento)
        //{
        //    //var listaConfirmacoes = new ListaConfirmacoes(documento);

        //    ListaRevisoesDocumento listaRevisoesDocumento = new ListaRevisoesDocumento(documento);



        //    if (!listaRevisoesDocumento.Vazia(documento))//listaConfirmacoes.Vazia())
        //    {
        //        var ultimaRevisao = listaRevisoesDocumento.GetUltimaRevisao(documento);

        //        //this.indiceRevCorrente = ultimaConfirmacao.IndiceRevisao;
        //        this.ordenadorCorrente = ultimaRevisao.ORDENADOR;// + 1;
        //        var confirmacao = new Confirmacao();
        //        confirmacao.Inserir(guidUsuario, documento.NumeroDocVerificado, this.indiceRevCorrente, guidPlalnilha, this.ordenadorCorrente);

        //        //this.indiceRevCorrente = ultimaConfirmacao.IndiceRevisao;
        //    }
        //    else
        //    {
        //        this.ordenadorCorrente = 0;
        //        var confirmacao = new Confirmacao();
        //        confirmacao.Inserir(guidUsuario, documento.NumeroDocVerificado, this.indiceRevCorrente, guidPlalnilha, this.ordenadorCorrente);
        //    }

        //}



        //public void ConfirmaRegistros(Documento documento)
        //{

        //    documento.ConfirmaRegistros(this.indiceRevCorrente, this.ordenadorCorrente);

        //    //for (int i = 0; i < lista.Count; i++)
        //    //{
        //    //    lista[i].Confirmado = true;

        //    //    Revisao revisao = new Revisao(lista[i].Guid, confirmador.GuidConfirmacao);

        //    //    atualizaConfirmacaoRev(revisao.GuidRev);

        //    //    revisao.Confirma();
        //    //}


        //}


        //public bool ConfirmadaRevisaoAtual(Documento documento)
        //{
        //    return documento.ConfirmadaRevisaoAtual(this.indiceRevCorrente);
        //}

        //public void CriaConfirmacao(string guid1, string guid2, string indice)
        //{
        //    ultimaConfirmacao = new Confirmacao()
        //}



    }
}
