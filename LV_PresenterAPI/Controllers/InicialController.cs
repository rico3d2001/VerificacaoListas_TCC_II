using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Consultas;
using LV_PresenterAPI.Gestao_UI;
using LV_PresenterAPI.Models.Navegacao;
using LVModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class InicialController : BasePresenterController
    {

        private QryProjetos _qryProjetos;


        public InicialController()
        {
            _qryProjetos = new QryProjetos();
        }

        public ActionResult Index()
        {

            Session["Projeto"] = null;
            Session["ListaVerificacaoVM"] = null;

            Session["NivelCorrente"] = 0;
            Session["Nav"] = new Navegador();

            var login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var usuario  = new QryUsuario().ObtemUsuario(login);
            if (usuario == null)
            {
                usuario = new Usuario()
                {
                    SIGLA = login,
                    ISVERIFICADOR = 0,
                    NOME = login,
                    SENHA = ""
                };
            }

            Session["Usuario"] = usuario;

            Session["IsVerficador"] = usuario.ISVERIFICADOR == 1 ? true : false;
            Session["IsGestor"] = usuario.ISGESTOR == 1 ? true : false;

            var isVerficador = (bool)Session["IsVerficador"];

            if(Session["Listaprojetos"] == null)
            {
                Session["Listaprojetos"] = _qryProjetos.GetProjetoToLists();
            }

            var listaProjetos = (List<ProjetoToListDTO>)Session["Listaprojetos"];


            ViewBag.ListaProjetos = new SelectList(listaProjetos, "GUID", "NUMERO");

            return View(listaProjetos);
        }

        public ActionResult BuscaLV(string id)
        {

            Session["GuidLV"] = null;

            //var msg = (string)TempData["MSGErroBusca"];
            //if(!string.IsNullOrEmpty(msg))
            //{
            //    //ViewBag.MSGErroBusca = msg;
            //    ModelState.AddModelError("ErroBusca", msg);
            //}

           //if(ModelState.IsValid)
           // {
                ViewBag.SiglaUser = ((Usuario)Session["Usuario"]).SIGLA;

                ProjetoVM projetoVM = null;
                if (Session["Projeto"] != null)
                {
                    projetoVM = (ProjetoVM)Session["Projeto"];
                }
                else
                {
                    projetoVM = _qryProjetos.GetProjetoApp(id);
                    Session["Projeto"] = projetoVM;
                }

                Session["LayoutPresenter"] = new LayoutPresenter("~/Views/Shared/_LayoutProjeto.cshtml");

                return View();
            //}

            //return View();


        }

        

        
    }
}