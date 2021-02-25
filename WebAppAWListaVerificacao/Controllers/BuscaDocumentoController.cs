using AppExcel.AppWeb;
using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class BuscaDocumentoController : DefaultController
    {
        public ActionResult Index(string guidprojeto, string guidos, string guidarea, string iddisciplina, string guidtipo, string sequencial)
        {

            //ProjetoViewModel projetoViewModel = new ProjetoViewModel();


            NumeroDocSNCLavalin numeroDocSNCLavalin = null;

            bool documentoEncontrado = false;

            string guidDoc = "";
            string numeroProjeto = "";


            if (guidprojeto != null && guidos != null && guidarea != null && iddisciplina != null && guidtipo != null && sequencial != null)
            {


                //string numeroProjeto = "";

                using (var contextoProjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
                {
                    contextoProjeto.Start();

                    var proj = contextoProjeto.ReturnByGUID(guidprojeto);

                    numeroDocSNCLavalin = new NumeroDocSNCLavalin(proj.NUMERO, guidos,
                    guidarea, iddisciplina, guidtipo, sequencial);

                    

                    //numeroProjeto = proj.NUMERO;

                    //projetoViewModel.NumeroDocSNCLavalin = numeroDocSNCLavalin;
                }

                using (var contextoDoc = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                {
                   contextoDoc.Start();
                    var documento = contextoDoc.GetByProperty("DOC_VERIFICADO", numeroDocSNCLavalin.ToString()).ToList().FirstOrDefault();
                    if (documento != null)
                    {
                        documentoEncontrado = true;
                        guidDoc = documento.GUID;
                    }
                }



            }


            if (documentoEncontrado)
            {
                Session["GuidDoc"] = guidDoc;
                ViewBag.resp = true;
                ViewBag.NumeroDocumentoCorrente = numeroDocSNCLavalin.ToString();

                return PartialView("Index", numeroDocSNCLavalin);
            }
            else
            {
                //projetoViewModel = new ProjetoViewModel();
                ViewBag.resp = false;
                return PartialView("Index", numeroDocSNCLavalin);
            }





        }



        [HttpPost]
        public ActionResult Index(NumeroDocSNCLavalin numeroDocSNCLavalin)
        {
            //var numeroDocSNCLavalin = projetoViewModel.NumeroDocSNCLavalin;

            //string guidDoc = "";
            string guidDoc = (string)Session["GuidDoc"];//projetoViewModel.GuidDocumento;

            CabecalhoViewModel cabecalho = null;

            bool existemRevisoesNaoConfimadas = false;//projetoViewModel.ExistemRevisoesNaoConfimadas;
            bool existemRevisoesNesteDocumento = false;//projetoViewModel.ExistemRevisoesNesteDocumento;

            using (var contextoDoc = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
            {

                contextoDoc.Start();
                var documento = contextoDoc.ReturnByGUID(guidDoc);
                    //.GetByProperty("DOC_VERIFICADO", numeroDocSNCLavalin.ToString()).ToList().FirstOrDefault();


                
                    //Session["LV"] = documento;

                    cabecalho = new CabecalhoApp().GetCabecalhoViewModel(documento);

                    existemRevisoesNaoConfimadas = documento.ListaRevisoes.Distinct().ToList().Exists(x => x.CONFIRMADO == 0);
                    existemRevisoesNesteDocumento = documento.ListaRevisoes.Distinct().ToList().Count > 0 ? true : false;

                    //Session["LV"] = documento;

                    //projetoViewModel = new ProjetoViewModel()
                    //{
                    //    GUID = guidprojeto,
                    //    NUMERO = numeroProjeto,
                    //    NumeroDocumentoCorrente = numeroDocSNCLavalin.ToString(),
                    //    GuidDocumento = documento.GUID,
                    //    ExistemRevisoesNaoConfimadas = existemRevisoesNaoConfimadas,
                    //    ExistemRevisoesNesteDocumento = existemRevisoesNesteDocumento //,
                    //                                                                  //Documento = documento
                    //};

                    //documentoEncontrado = true;
                    guidDoc = documento.GUID;
                
               
            }


            ////////////////////////////////////////////////////////


            //bool existemRevisoesNaoConfimadas = projetoViewModel.ExistemRevisoesNaoConfimadas;
            //bool existemRevisoesNesteDocumento = projetoViewModel.ExistemRevisoesNesteDocumento;


            //string guidDoc = projetoViewModel.GuidDocumento;

            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            //

            var nivel = 3;
            //var guidTipoCorrente = projetoViewModel.GetGuidTipo();
            ViewBag.nivel = nivel;
            //ViewBag.guid = guidTipoCorrente;

            //var btns = new LayoutSession();
            //btns.SetDocumentoCarregado();

            //Session["BOTOES"] = btns;



            List<Revisao> listaRevisoes = null;

            bool isVerificador = false;



            isVerificador = getUsuario(login).ISVERIFICADOR == 1 ? true : false;
            //if (documentoEncontrado)
            //{
            if (!isVerificador)
            {
                TempData["LayoutUsuario"] = "_LayoutNoVerificador";
            }
            else
            {
                if (existemRevisoesNesteDocumento)
                {


                    if (existemRevisoesNaoConfimadas)
                    {
                        TempData["LayoutUsuario"] = "_LayoutAddRevisao";
                    }
                    else
                    {
                        TempData["LayoutUsuario"] = "_LayoutNoConfirm";
                    }
                }
                else
                {
                    TempData["LayoutUsuario"] = "_LayoutDocumentoNovo";
                }
            }



            return RedirectToAction("IndexLD", "ListaDocumento", new { guidDocumento = guidDoc });
            //}
            //else
            //{
            //    return PartialView("Index", projetoViewModel);
            //}




        }
    }
}