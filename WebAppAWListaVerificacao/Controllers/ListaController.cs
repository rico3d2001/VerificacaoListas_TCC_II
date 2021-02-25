using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class ListaController : DefaultController
    {

        

        protected ListaColunasApp _listaColunasApp;
        protected CabecalhoApp _cabecalhoApp;
        protected ImagemStatusViewModel _imagemStatusViewModel;

        protected ICrtlLista _crtl_Lista;

        protected Navegador _navegadorSession;

        public ListaController()
        {

            _cabecalhoApp = new CabecalhoApp();

            _imagemStatusViewModel = new ImagemStatusViewModel
            {
                ImagePathV = "~/imagens/V.png",
                ImagePathX = "~/imagens/X.png",
                ImagePathNA = "~/imagens/NA.png",
                ImagePathND = "~/imagens/ND.png",
                ImagePathI = "~/imagens/I.png"
            };

        }

        public ActionResult Index(int nivel, string guid)
        {

            Session["NivelCorrente"] = nivel;


            LayoutSession layoutSession = null;
            if (Session["BOTOES"] != null)
            {
                layoutSession = (LayoutSession)Session["BOTOES"];
            }


            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

           

            _navegadorSession = (Navegador)Session["Nav"];

            var numeroDocumento = string.Empty; 
            if (Session["NumeroDocumento"] != null)
            {
                numeroDocumento = (string)Session["NumeroDocumento"];
            }


            _navegadorSession.OperaNivel(nivel, guid);

            if (nivel > 2)
            {

                if (_navegadorSession.PlanilhaEscolhida == null)
                {

                    ViewBag.DocumentoAtivado = false;

                    Session["NivelCorrente"] = nivel;
                    ViewBag.SiglaUser = login;
                    return View();

                }
                else if (_navegadorSession.PlanilhaEscolhida != null)
                {

                    if (string.IsNullOrEmpty(numeroDocumento))
                    {

                        ViewBag.DocumentoAtivado = false;
                        ViewBag.SiglaUser = login;

                        return RedirectToAction("IndexLT", "ListaTemplate", new { guidPlanilha = _navegadorSession.PlanilhaEscolhida.GUID });
                    }
                    else
                    {
                        ViewBag.DocumentoAtivado = false;
                        ViewBag.SiglaUser = login;

                        _navegadorSession.OperaNivel(nivel, guid);

                        //var usuario = getUsuario(login);
                        var isVerificador = getUsuario(login).GetBoolIsVerificador();


                        if (isVerificador)
                        {
                            TempData["LayoutUsuario"] = "_LayoutProjeto";
                            return RedirectToAction("IndexLD", "ListaDocumento", new { guidDocumento = _navegadorSession.PlanilhaEscolhida.GUID });

                            
                        }
                        else
                        {
                            return RedirectToAction("IndexLD", "ListaDocumento", new { guidDocumento = _navegadorSession.PlanilhaEscolhida.GUID });
                        }

                        

                    }

                }
            }
            else
            {

                ViewBag.DocumentoAtivado = false;

                Session["NivelCorrente"] = nivel;
                ViewBag.SiglaUser = login;
                return View();
            }


            return View();
        }

        public ActionResult ValidaAddRevisao(string GuidDocumento, string Nome)
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
                resp = false;
            }
            else
            {
                resp = true;
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _GetStatusExistente(LinhaRevisao linhaRev)
        {
            return PartialView();
        }

        public PartialViewResult _TabelaGrupos(LinhaRevisao linhaRev)
        {
            return PartialView();
        }

        public ActionResult DefineStatus(string idTipo, string status)
        {
            var nav = (Navegador)Session["Nav"];

            var lista = new List<RegistroRevisao>();
            if (Session["ListaRegistrosView"] != null)
            {
                lista = (List<RegistroRevisao>)Session["ListaRegistrosView"];
            }

            lista.Find(x => x.GuidTipoRev.Equals(idTipo)).Status = status;

            Session["ListaRegistrosView"] = lista;

            TempData["PodeSalvar"] = false;

            return RedirectToAction("Index", "Lista", new { nivel = 3, guid = nav.PlanilhaEscolhida.GUID });
        }


    }
}