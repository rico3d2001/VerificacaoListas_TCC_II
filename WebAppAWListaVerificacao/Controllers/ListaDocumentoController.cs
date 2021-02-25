using AppExcel.AppWeb;
using AutoMapper;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAWListaVerificacao.Models;
using Unity;
using LV_DI;
using AppListaVerificacao.Interface;
using AppListaVerificacao;
using LV14FluentNHB.Service;

namespace WebAppAWListaVerificacao.Controllers
{
    public class ListaDocumentoController : ListaTemplateController
    {


        public ActionResult IndexLD(string guidDocumento)
        {
            //string guidDoc = (string)Session["GuidDoc"];
            //var documento = (ListaVerificacao)Session["LV"];

            ListaVerificacao documento = null;

            //bool documentoContemRevisoes = false;
            //bool existemRevisoesNaoConfirmadas = false;

            ViewBag.Layout = (string)TempData["LayoutUsuario"];

            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            //var usuario = getUsuario(login);
            var isVerificador = getUsuario(login).GetBoolIsVerificador();

            List<Revisao> listaRevisoesDocumento = new List<Revisao>();
            List<ColunaRevisaoViewModel> lisaColunaRevisaoViewModels = null;
            //ListaVerificacao documento = null;

            //using (var contextoRevisao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>())
            //{
            //contextoRevisao.Start();

            //listaRevisoesDocumento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>()
            //.GetByProperty("GUID_DOC_VERIFICACAO", guidDocumento).ToList();

            using (var contextoListaVericacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
            {
                contextoListaVericacao.Start();

                documento = contextoListaVericacao.ReturnByGUID(guidDocumento);



                listaRevisoesDocumento = documento.ListaRevisoes.Distinct().ToList();

                TempData["ImagemStatusViewModel"] = _imagemStatusViewModel;

                ViewBag.CabecalhoViewModel = new CabecalhoApp().GetCabecalhoViewModel(documento);




                if (listaRevisoesDocumento.Count > 0)
                {
                    List<ListaRegistrosPorColunas> listaCadastroRevisoes = new ListaCadastroRevisoes().GetListaRevisoes(listaRevisoesDocumento);

                    Session["ListaRevisoes"] = listaCadastroRevisoes;

                    ListaColunasTemplateRevisoes agupamentoTemplateRevisoes =
                    new ListaColunasTemplateRevisoes(documento, listaCadastroRevisoes);


                    //bool isVerificador = (usuario != null && usuario.ISVERIFICADOR == 1) ? true : false;

                    lisaColunaRevisaoViewModels = agupamentoTemplateRevisoes.ObtemLista_ColunaRevisaoDocumento(documento.GUID, isVerificador, listaRevisoesDocumento);

                    //using (var contextoConfirmacao = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>())
                    //{
                    //contextoConfirmacao.Start();

                    List<Confirmacao> listaConfirmacao = documento.ListaConfirmacoes.Distinct().ToList();

                    //documentoContemRevisoes = true; ;

                    //existemRevisoesNaoConfirmadas = listaRevisoesDocumento.Exists(x => x.CONFIRMADO == 0);


                    //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>()
                    //.GetByProperty("GUID_DOCUMENTO", guidDocumento).ToList();


                    TempData["ListaRegistroConfirmacoesViewModel"] = listaConfirmacao;



                    ViewBag.PossuiRevisoes = true;
                }
                else
                {
                    ViewBag.PossuiRevisoes = false;

                    lisaColunaRevisaoViewModels = new ListaColunasTemplate(documento.Planilha).ObtemLista_ColunaRevisaoDocumento(); //.GetPlanilha()).ObtemLista_ColunaRevisaoDocumento();

                    ////documentoContemRevisoes = false; 

                    //existemRevisoesNaoConfirmadas = listaRevisoesDocumento.Exists(x => x.CONFIRMADO == 0);


                }

            }

            //string guid_logPC = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();
            //bool isVerficador = getUsuario(guid_logPC).GetBoolIsVerificador();



            //if (!isVerficador)
            //{
            //    TempData["LayoutUsuario"] = "_LayoutNoVerificador";
            //}
            //else
            //{
            //    if (documentoContemRevisoes)
            //    {
            //        if (existemRevisoesNaoConfirmadas)
            //        {

            //            TempData["LayoutUsuario"] = "_LayoutAddRevisao";
            //        }
            //        else
            //        {
            //            TempData["LayoutUsuario"] = "_LayoutNoConfirm";
            //        }
            //    }
            //    else
            //    {
            //        TempData["LayoutUsuario"] = "_LayoutDocumentoNovo";
            //    }
            //}

            // }

            ViewBag.List_ColunaRevisaoViewModel = lisaColunaRevisaoViewModels;

            ViewBag.DocumentoAtivado = true;

            ViewBag.GuidDocumento = documento.GUID;

            TempData["GuidDocumento"] = documento.GUID;

            ViewBag.SiglaUser = login;

            //var isVerificador1 = usuario.PodeVerificar();
            ViewBag.IsVerificador = isVerificador; //usuario == null || usuario.ISVERIFICADOR == 0 ? false : true;

            ViewBag.IsListaVerificacaoDupla = documento.Planilha.VERIFICADOR_UNICO == 1 ? false : true;
            ViewBag.GuidDoc = documento.GUID;

            return View();
        }
    }
}