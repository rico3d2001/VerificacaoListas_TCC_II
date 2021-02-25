using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Comandos;
using LV_PresenterAPI.Consultas;
using LV_PresenterAPI.Gestao_UI;
using LV_PresenterAPI.Models;
using LV_PresenterAPI.Models.Navegacao;
using LV_PresenterAPI.Service;
using RepositorioMySQL.Consultas;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class ListaController : BasePresenterController
    {
        protected Navegador _navegadorSession;
        protected PlanilhaLVVM _planilha;
        protected ImagemStatusViewModel _imagemStatusViewModel;
        public ListaController()
        {

            //_cabecalhoApp = new CabecalhoApp();

            _imagemStatusViewModel = new ImagemStatusViewModel
            {
                ImagePathV = "~/imagens/V.png",
                ImagePathX = "~/imagens/X.png",
                ImagePathNA = "~/imagens/NA.png",
                ImagePathND = "~/imagens/ND.png",
                ImagePathI = "~/imagens/I.png"
            };

        }



        // GET: Lista
        public ActionResult Index(int? nivel, string guid)
        {
            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            _navegadorSession = (Navegador)Session["Nav"];

            if (nivel == null)
            {
                nivel = (int)Session["NivelCorrente"];
            }

            _navegadorSession.OperaNivel(nivel, guid);

            var numeroDocumento = string.Empty;
            if (Session["NumeroDocumento"] != null)
            {
                numeroDocumento = (string)Session["NumeroDocumento"];
            }

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

                        return RedirectToAction("LTemplate", "Lista", new { guidPlanilha = _navegadorSession.PlanilhaEscolhida.GUID });
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


        public ActionResult LTemplate(string guidPlanilha)
        {

            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            //_planilha = ConsultaPlanilha.ObtemPlanilha(guidPlanilha);

            Session["NivelCorrente"] = 3;




            Session["Login"] = login;


           


            ViewBag.DocumentoAtivado = false;

            ViewBag.ImagemStatusViewModel = _imagemStatusViewModel;

            //_planilha = ConsultaPlanilha.ObtemPlanilha(guidPlanilha); //
            _planilha = MySQLConsultaPlanilha.ObtemPlanilha(guidPlanilha);

            ViewBag.PlanilhaViewModel = _planilha;//ConsultaPlanilha.ObtemPlanilha(guidPlanilha);

            ViewBag.PlanilhaAtivada = true;

            ViewBag.List_ColunaRevisaoViewModel = new ListaColunasTemplate(_planilha).ObtemLista_ColunaRevisaoDocumento();

            ViewBag.GuidPlanilha = _planilha;

            Session["planilha"] = _planilha;


            ViewBag.SiglaUser = login;
            TempData["LayoutUsuario"] = "_LayoutPlanilhaEncontrada";



            return View();


        }


        public ActionResult ListaDoc(string id)
        {

            //QryListaVerificacao.Reset();


            //    QryListaVerificacao qryListaVerificacao = new QryListaVerificacao(_baseUrl, id);
            Session["PossuiRevisoes"] = QryListaVerificacao.Instancia(id).ListaPossuiRevisoes;
            Session["ExistemRevisoesNaoConfirmadas"] = QryListaVerificacao.Instancia(id).ObtemEstadoRevisoes().PossuiRevisoesNaoConfirmadas;


            bool retomada = false;
            if (TempData["Retomada"] != null)
            {
                retomada = (bool)TempData["Retomada"];
            }
            

           

            //var lv = qryListaVerificacao.RecuperaLV(id);
            var lv = QryListaVerificacao.Instancia(id).ListaVerificacaoApp;//.RecuperaLV_ViewModel();


            Session["lv"] = lv;

                     Session["GidLV"] = lv.GUID;
            Session["NumeroDoc"] = lv.NUMERODOC;
            Session["VerificadorUnico"] = lv.VERFICADOR_UNICO;

            //var estadoConfirmacoes = qryListaVerificacao.ObtemEstadoConfirmacoes(lv.GUID);


            //var estadoRevisoes = qryListaVerificacao.ObtemEstadoRevisoes(lv.GUID);
            var estadoRevisoes = QryListaVerificacao.Instancia(id).ObtemEstadoRevisoes();

            var abriuNaoConfirmouAinda = (bool)Session["AbriuNaoConfirmouAinda"];

            ViewBag.PossuiRevisoesNaoConfirmadas = estadoRevisoes.PossuiRevisoesNaoConfirmadas;

                var layoutPresenter =
                   new LayoutPresenter(
                       (bool)Session["IsVerficador"],
                       estadoRevisoes.ExistemRevisoesNesteDocumento, //(bool)Session["PossuiRevisoes"],
                        estadoRevisoes.PossuiRevisoesNaoConfirmadas,   //(bool)Session["ExistemRevisoesNaoConfirmadas"],
                        lv.VERFICADOR_UNICO,  //(int)Session["VerificadorUnico"],
                       abriuNaoConfirmouAinda,
                       retomada,
                       (bool)Session["IsGestor"]);

            Session["LayoutPresenter"] = layoutPresenter;



            //ViewBag.IsVerificador = Usuario_Verificador();

            //CmdConfirmacaoRevisao.Reset();
            CmdConfirmacaoRevisao.Instancia().Reset();
            CmdRetomadaRevisao.Instancia().Reset();

            

            return View(lv);
        }




    }
}