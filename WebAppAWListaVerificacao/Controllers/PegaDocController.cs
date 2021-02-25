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
using WebAppAWListaVerificacao.Validator;

namespace WebAppAWListaVerificacao.Controllers
{
    public class PegaDocController : DefaultController
    {
        private bool _passou;

        public PegaDocController()
        {
            _passou = false;
        }

        public ActionResult Index(string guid_planilha)
        {


            string siglaPlanilha = "";
            using (var contextoPlanilha = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>())
            {
                contextoPlanilha.Start();
                var planilha = contextoPlanilha.ReturnByGUID(guid_planilha);
                siglaPlanilha = planilha.Tipo.Configuracao.Disciplina.SIGLA;

            }

            DocViewModel docViewModel = new DocViewModel();

            docViewModel.SiglaDisciplina = siglaPlanilha;

            docViewModel.GuidPlanilha = guid_planilha;

            return PartialView("Index", docViewModel);
        }


        public ActionResult MsgDocIndefinido()
        {
            return View();
        }


        public ActionResult Salva(DocViewModel docViewModel)
        {

            //if (ModelState.IsValid)
            //{



            var p_guid_planilha = docViewModel.GuidPlanilha;
            var p_projeto = docViewModel.Projeto;
            var p_os = docViewModel.OS;
            var p_area = docViewModel.Area;
            var p_disciplina = docViewModel.SiglaDisciplina;
            var p_tipodoc = docViewModel.TipoDocumento;
            var p_sequencial = docViewModel.Sequencial;



            bool documentoContemRevisoes = false;
            bool existemRevisoesNaoConfirmadas = false;
            string docGuid = "";

            var validador = new DocViewModelValidator();

            var result = validador.Validate(docViewModel);

       
            if (!result.IsValid)
            {
                //return View(docViewModel);
                //return View("MsgDocIndefinido");
                //return Json(new
                //{
                //    status = "failure"
                //});
                return Content("");
            }
            else
            {

                if(_passou == false)
                {

               

                var numeroDocSNCLavalin =
                    new NumeroDocSNCLavalin(
                        p_projeto,
                        p_os,
                        p_area,
                        p_disciplina,
                        p_tipodoc,
                        p_sequencial);

                TempData["IsVerificador"] = true;

                string numeroDesenho = numeroDocSNCLavalin.ToString();



                ListaVerificacao documento = null;

                    using (var contextoDocumento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>())
                    {
                        contextoDocumento.Start();
                        documento = contextoDocumento.GetByProperty("DOC_VERIFICADO", numeroDesenho).FirstOrDefault();

                        Projeto projeto = null;

                        if (documento == null)
                        {

                            using (var contextoProjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
                            {
                                contextoProjeto.Start();

                                var listaProjetos = contextoProjeto.GetByProperty("NUMERO", p_projeto).ToList();

                                if (listaProjetos.Count > 0 && listaProjetos.Count < 2)
                                {
                                    projeto = listaProjetos.FirstOrDefault();

                                    if (projeto.ListaOSs.FirstOrDefault(x => x.NUMERO == numeroDocSNCLavalin.OS) == null)
                                    {
                                        projeto.AddOS(new OS()
                                        {
                                            GUID = Guid.NewGuid().ToString(),
                                            NUMERO = numeroDocSNCLavalin.OS,
                                            Projeto = projeto
                                        });

                                    }

                                    if (projeto.ListaAreas.FirstOrDefault(x => x.NUMERO == numeroDocSNCLavalin.AREA) == null)
                                    {
                                        projeto.AddArea(new Area()
                                        {
                                            GUID = Guid.NewGuid().ToString(),
                                            NUMERO = numeroDocSNCLavalin.AREA,
                                            Projeto = projeto
                                        });


                                    }

                                    contextoProjeto.Update(projeto);
                                    contextoProjeto.Commit();


                                }
                                else
                                {
                                    projeto = new Projeto()
                                    {
                                        GUID = Guid.NewGuid().ToString(),
                                        NUMERO = numeroDocSNCLavalin.PROJETO
                                    };

                                    projeto.AddOS(new OS()
                                    {
                                        GUID = Guid.NewGuid().ToString(),
                                        NUMERO = numeroDocSNCLavalin.OS,
                                        Projeto = projeto
                                    });

                                    projeto.AddArea(new Area()
                                    {
                                        GUID = Guid.NewGuid().ToString(),
                                        NUMERO = numeroDocSNCLavalin.AREA,
                                        Projeto = projeto
                                    });


                                    contextoProjeto.Insert(projeto);
                                    contextoProjeto.Commit();

                                }


                            }

                            docGuid = Guid.NewGuid().ToString();


                            Planilha planilha = null;

                            using (var contextoPlanilha = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>())
                            {
                                contextoPlanilha.Start();
                                planilha = contextoPlanilha.ReturnByGUID(p_guid_planilha);

                            }

                            documento = new ListaVerificacao()
                            {
                                GUID = docGuid,
                                NUMERO = "1",
                                DOC_VERIFICADO = numeroDocSNCLavalin.ToString(),
                                Planilha = planilha,
                                Projeto = projeto,
                                OS = projeto.ListaOSs.Last(),
                                Area = projeto.ListaAreas.Last()
                            };

                            contextoDocumento.Insert(documento);
                            contextoDocumento.Commit();

                            numeroDocSNCLavalin.GUID = docGuid;
                            using (var contextoNumeroDocSNCLavalin = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
                            {
                                contextoNumeroDocSNCLavalin.Start();
                                contextoNumeroDocSNCLavalin.Insert(numeroDocSNCLavalin);
                                contextoNumeroDocSNCLavalin.Commit();
                            }

                            _passou = true;

                            docViewModel.GuidDocumento = docGuid;
                            Session["GuidDoc"] = docGuid;

                        }
                        else
                        {
                            docGuid = documento.GUID;

                            Session["GuidDoc"] = docGuid;

                            var listaRevisoes = documento.ListaRevisoes.Distinct().ToList();
                            documentoContemRevisoes = listaRevisoes.Count > 0;

                            existemRevisoesNaoConfirmadas = listaRevisoes.Exists(x => x.CONFIRMADO == 0);

                            _passou = true;
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





                if (_passou == true)
                {
                   
                    var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri)
                    {
                        Path = Url.Action("IndexLD", "ListaDocumento"),
                        Query = null,
                    };

                    //Uri uri = urlBuilder.Uri;
                    string url = urlBuilder.ToString();

                    string env = url + "?guidDocumento=" + docGuid;
                    return Content(env);
                }

                return Content("Preenchimento inadequado.");
            }



        }



    }
}