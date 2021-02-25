

using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace AppExcel.AppWeb
{
    public class ListaConfirmacoesDocumentoApp
    {
        ////private readonly List<ConfirmacaoParaLista> _listaConfimacaoParaLista;

        ////public List<ConfirmacaoParaLista> ListaConfimacaoParaLista { get => _listaConfimacaoParaLista; }

        //public ListaConfirmacoesDocumentoApp(NumeroDocSNCLavalin numeroDocumento)
        //{

        //    _listaConfimacaoParaLista = new List<ConfirmacaoParaLista>();


        //    Documento documento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Documento>>().GetByProperty("DOC_VERIFICADO", numeroDocumento).FirstOrDefault();
        //    //Documento documento = new Documento(numeroDocumento);



        //    //IAppServiceBase<Confirmacao> appConfirmacaoService = ;

        //    var lvConfirmacoes = 
        //        DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>()
        //        .GetByProperty("GUID_DOCUMENTO", documento.GUID).ToList();

        //    //var lvConfirmacoes = new Repository<LV_CONFIRMACAO>().GetByProperty("GUID_DOCUMENTO", documento.GUID).ToList();


        //    var lvConfirmacoesOrdenada = lvConfirmacoes.OrderBy(x => x.ORDENADOR);
        //    //lvConfirmacoes.ForEach(x => _confirmacoes.Add(new Confirmacao(x.GUID)));


        //    int newOrder = 0;
        //    foreach (var lv in lvConfirmacoesOrdenada)
        //    {
        //        if (lv.GUID_USUARIO1 == lv.GUID_USUARIO2 || (lv.GUID_USUARIO1 != null && lv.GUID_USUARIO2 == null))
        //        {

        //            Usuario usuario = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>().ReturnByGUID(lv.GUID_USUARIO1);//new Usuario();
        //            //usuario.SetByGuid();
        //            NovaConfimacaoParaListagem(newOrder, lv, usuario);

        //            newOrder++;
        //        }
        //        else
        //        {
        //            Usuario usuario1 = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>().ReturnByGUID(lv.GUID_USUARIO1);
        //            //usuario.SetByGuid(lv.GUID_USUARIO1);
        //            NovaConfimacaoParaListagem(newOrder, lv, usuario1);

        //            newOrder++;

        //            //var usuario2 = new Usuario();
        //            Usuario usuario2 = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>().ReturnByGUID(lv.GUID_USUARIO2);
        //            //usuario.SetByGuid(lv.GUID_USUARIO1);
        //            NovaConfimacaoParaListagem(newOrder, lv, usuario2);

        //            newOrder++;

        //        }
        //    }


        //    //ListaConfirmacoesByDocumento listaConfirmacoes = new ListaConfirmacoesByDocumento(documento);


        //}

        //private void NovaConfimacaoParaListagem(int newOrder, Confirmacao confirmacao, Usuario usuario)
        //{

        //    string strData = confirmacao.DATA.ToShortDateString();

        //    _listaConfimacaoParaLista.Add(new ConfirmacaoParaLista()
        //    {
        //        IndiceRevisao = confirmacao.INDICE_REV,
        //        GuidConfirmacao = confirmacao.GUID,
        //        Data = strData,
        //        Sigla = usuario.GUID,
        //        NomeVerificador = usuario.NOME,
        //        Ordenador = newOrder

        //    });
        //}

        //public ConfirmacaoParaLista GetUltimaConfirmacao()
        //{
        //    var lista = _listaConfimacaoParaLista.OrderBy(x => x.Ordenador);
        //    if (lista.Count() > 0)
        //    {
        //        return lista.Last();
        //    }

        //    return null;

        //}


        //public bool Vazia()
        //{
        //    return _listaConfimacaoParaLista.Count > 0 ? false : true;
        //}


    }
}
