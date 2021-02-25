
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
    public class AddRevisoesController : DefaultController
    {
        private bool _inserido;
        private bool _incluido;

        public AddRevisoesController()
        {
            _incluido = false;
            _inserido = false;
        }

        public ActionResult Index(string guidDoc)
        {

            AddRevisaoViewModel addRevisaoViewModel = new AddRevisaoViewModel()
            {
                GuidDocumento = guidDoc
            };

            ViewBag.DocAdd = guidDoc;

            return PartialView("Index", addRevisaoViewModel);

        }




        public ActionResult Adiciona(AddRevisaoViewModel addRevisaoViewModel, string guidDocu)
        {


            bool existemRevisoesNaoConfirmadas = false;
            bool documentoContemRevisoes = false;

            if (string.IsNullOrEmpty(addRevisaoViewModel.Nome)
                || !ValidaAddRevisao(addRevisaoViewModel.GuidDocumento, addRevisaoViewModel.Nome)
                || addRevisaoViewModel.Nome.Count() > 2)
            {

                return Content("");
            }


            if (_inserido == false)
            {

                guidDocu = addRevisaoViewModel.GuidDocumento;


                string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();
                var usuario = getUsuario(login);
                var isVerificador = usuario.GetBoolIsVerificador();
                var guidUsuarioCorrente = usuario.GUID;




                using (var contextoDocumentoRevisoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                {
                    contextoDocumentoRevisoes.Start();

                    var documento = contextoDocumentoRevisoes.ReturnByGUID(guidDocu);


                    if (documento.PodeAcrescentarRevisao(addRevisaoViewModel.Nome))
                    {

                        if (isVerificador)
                        {

                            _incluido = documento.AddRevisao(addRevisaoViewModel.Nome, guidUsuarioCorrente);

                            Session["LV"] = documento;

                            if (_incluido)
                            {
                                contextoDocumentoRevisoes.Update(documento);

                                contextoDocumentoRevisoes.Commit();

                                documentoContemRevisoes = true;
                                existemRevisoesNaoConfirmadas = documento.ListaRevisoes.Distinct().ToList().Exists(x => x.CONFIRMADO == 0);

                                _inserido = true;
                            }

                        }
                    }
                }

            }

            string guid_logPC = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();
            bool isVerficador = getUsuario(guid_logPC).GetBoolIsVerificador();

            if (!isVerficador)
            {
                TempData["LayoutUsuario"] = "_LayoutNoVerificador";
            }
            else
            {
                if (documentoContemRevisoes)
                {
                    if (existemRevisoesNaoConfirmadas)
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


            if (_inserido)
            {
    
                var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri)
                {
                    Path = Url.Action("IndexLD", "ListaDocumento"),
                    Query = null,
                };

                //Uri uri = urlBuilder.Uri;
                string url = urlBuilder.ToString();

                string env = url + "?guidDocumento=" + addRevisaoViewModel.GuidDocumento;
                return Content(env);

            }
            else
            {
                return Content("");
            }



        }


        public ActionResult MsgAdicaoErrada(string msg)
        {

            ViewBag.MsgAdicaoErrada = msg;

            return View();
        }


        public bool ValidaAddRevisao(string GuidDocumento, string Nome)
        {
            bool resp = false;


            List<Revisao> listaRevisoes = null;
            using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>())
            {
                contextoObjeto.Start();
                listaRevisoes = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Revisao>>()
                .GetByProperty("GUID_DOC_VERIFICACAO", GuidDocumento).ToList();

            }

            if (listaRevisoes.Exists(x => x.INDICE == Nome))
            {
                return false;
            }
            else
            {
                return true;
            }

            //return Json(resp, JsonRequestBehavior.AllowGet);
        }








    }
}