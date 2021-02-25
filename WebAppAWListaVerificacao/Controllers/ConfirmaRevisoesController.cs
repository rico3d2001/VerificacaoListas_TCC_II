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
    public class ConfirmaRevisoesController : DefaultController
    {





        private bool passou;
        
        private string guidDoc;

        public ConfirmaRevisoesController()
        {
            passou = false;
            
        }
        public ActionResult Index(string guidDoc, bool isListaVerificaDupla)
        {

            Session["documentoalterado"] = false;

            ConfirmaColunaViewModel confirmaViewModel = new ConfirmaColunaViewModel();



            bool isListaConfimacaoDupla = false;

            bool naoTemRevisoesIndefinidas = false;
            bool houvePrimeiraConfiramcao = false;


            using (var contextoLV = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
            {
                contextoLV.Start();
                var listaVerificacao = contextoLV.ReturnByGUID(guidDoc);



                naoTemRevisoesIndefinidas = listaVerificacao.NaoTemRevisoesIndefinidas();

                if (naoTemRevisoesIndefinidas)
                {
                    isListaConfimacaoDupla = listaVerificacao.IsListaConfimacaoDupla();
                    if (isListaConfimacaoDupla)
                    {

                        if (!listaVerificacao.NuncaHouveConfimacaoNesteDocumento())
                        {
                            if (listaVerificacao.HouveSomentePrimeiraConfiramcao())
                            {
                                houvePrimeiraConfiramcao = true;
                            }
                        }




                    }
                }

            }



            confirmaViewModel.IsListaConfimacaoDupla = isListaConfimacaoDupla;

            confirmaViewModel.GuidDocumento = guidDoc;

            if (naoTemRevisoesIndefinidas)
            {
                if (confirmaViewModel.IsListaConfimacaoDupla)
                {

                    if (houvePrimeiraConfiramcao)
                    {
                        ViewBag.MensagemErro = "Esta é a segunda confirmação.";
                        return View(confirmaViewModel);
                    }
                    else
                    {
                        ViewBag.MensagemErro = "Será necessária a segunda confirmação.";
                        return View(confirmaViewModel);
                    }

                }
                else
                {
                    ViewBag.MensagemErro = "Esta é a confimação única.";
                    return View(confirmaViewModel);
                }
            }
            else
            {
                ViewBag.MensagemErro = "Defina todos os itens da lista antes de confirmar.";
                return View("MsgItensIndefinidos");
            }




        }




        //[HttpPost]
        public ActionResult Confirma(ConfirmaColunaViewModel confirmaViewModel)
        {

            //if ((bool)Session["documentoalterado"] == true)
            //{
            //    return Content("");
            //}

            if ((bool)Session["documentoalterado"] == false)
            {


                guidDoc = confirmaViewModel.GuidDocumento;
                string guidConfirmacao = Guid.NewGuid().ToString();

                bool isListaConfiguarcaoDupla = confirmaViewModel.IsListaConfimacaoDupla;
                int ordenador = confirmaViewModel.Ordenador;



                string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();
                var guidUsuario = getUsuario(login).GUID;




                using (var contextoDocumento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                {
                    contextoDocumento.Start();
                    var documento = contextoDocumento.ReturnByGUID(guidDoc);

                    var listaRevisoesNaoConfirmadas = documento.ListaRevisoes.Distinct().Where(x => x.CONFIRMADO == 0).ToList();
                    if (listaRevisoesNaoConfirmadas.Count > 0)
                    {
                        var indice = listaRevisoesNaoConfirmadas.Last().INDICE;

                        bool alterdoDocumento = documento.ConfirmaRevisoes(guidUsuario, isListaConfiguarcaoDupla, guidConfirmacao, ordenador,
                       indice);


                        if (alterdoDocumento)
                        {
                            contextoDocumento.Update(documento);

                            contextoDocumento.Commit();

                            using (var contextoNumeroDocSNCLavalin = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
                            {
                                contextoNumeroDocSNCLavalin.Start();
                                var numeroDoc = contextoNumeroDocSNCLavalin.ReturnByGUID(guidDoc);
                                numeroDoc.GUID_ULTIMA_CONFIRMACAO = guidConfirmacao;
                                contextoNumeroDocSNCLavalin.Update(numeroDoc);
                                contextoNumeroDocSNCLavalin.Commit();
                            }

                            Session["documentoalterado"] = true;

                           

                            passou = true;
                        }
                    }


                }

            }


            if (passou)
            {



                TempData["LayoutUsuario"] = "_LayoutDesconfirmar";

                var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri)
                {
                    Path = Url.Action("IndexLD", "ListaDocumento"),
                    Query = null,
                };

                //Uri uri = urlBuilder.Uri;
                string url = urlBuilder.ToString();

                string env = url + "?guidDocumento=" + guidDoc;
                return Content(env);


                //return RedirectToAction("IndexLD", "ListaDocumento", new { guidDocumento = guidDoc });
            }
            else
            {
                TempData["LayoutUsuario"] = "_LayoutDesconfirmar";
                //ViewBag.MensagemErro = "Falta preencher campos.";
                //return View(confirmaViewModel);
                return Content("");

            }


        }

        public ActionResult MsgItensIndefinidos()
        {
            return View();
        }


    }
}