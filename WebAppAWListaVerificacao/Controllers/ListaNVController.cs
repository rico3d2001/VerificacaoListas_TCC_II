using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class ListaNVController : ListaTemplateController
    {
        // GET: ListaNV
        public ActionResult IndexNaoVerificador(string guidDocumento)
        {
            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            Usuario usuario = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>()
                .GetByProperty("SIGLA", login).FirstOrDefault();


            var documento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>()
                    .ReturnByGUID(guidDocumento);


            TempData["ImagemStatusViewModel"] = _imagemStatusViewModel;



            ViewBag.CabecalhoViewModel = new CabecalhoApp().GetCabecalhoViewModel(documento);



            List<ColunaRevisaoViewModel> lisaColunaRevisaoViewModels = null;



            var listaRevisoesDocumento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>()
                    .GetByProperty("GUID_DOC_VERIFICACAO", documento.GUID).ToList();



            if (listaRevisoesDocumento.Count > 0)
            {
                List<ListaRegistrosPorColunas> listaCadastroRevisoes = new ListaCadastroRevisoes().GetListaRevisoes(listaRevisoesDocumento);

                Session["ListaRevisoes"] = listaCadastroRevisoes;

                ListaColunasTemplateRevisoes agupamentoTemplateRevisoes =
                new ListaColunasTemplateRevisoes(documento, listaCadastroRevisoes);

                bool isVerficador = usuario.ISVERIFICADOR == 1 ? true : false;

                lisaColunaRevisaoViewModels = agupamentoTemplateRevisoes.ObtemLista_ColunaRevisaoDocumento(guidDocumento, isVerficador, listaRevisoesDocumento);



                List<Confirmacao> listaConfirmacao =
                DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>()
                .GetByProperty("GUID_DOCUMENTO", documento.GUID).ToList();




                TempData["ListaRegistroConfirmacoesViewModel"] = listaConfirmacao;

                ViewBag.PossuiRevisoes = true;

            }
            else
            {
                ViewBag.PossuiRevisoes = false;

                lisaColunaRevisaoViewModels = new ListaColunasTemplate(documento.Planilha).ObtemLista_ColunaRevisaoDocumento();//.GetPlanilha()).ObtemLista_ColunaRevisaoDocumento();
            }

            ViewBag.List_ColunaRevisaoViewModel = lisaColunaRevisaoViewModels;


            ViewBag.DocumentoAtivado = true;


            ViewBag.GuidDocumento = guidDocumento;
            TempData["GuidDocumento"] = guidDocumento;

            ViewBag.SiglaUser = login;

            TempData["LayoutUsuario"] = "_LayoutProjeto";
            return View();
        }
    }
}