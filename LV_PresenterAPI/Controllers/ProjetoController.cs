using LV_PresenterAPI.Consultas;
using LV_PresenterAPI.Models.Navegacao;
using System.Web.Mvc;


namespace LV_PresenterAPI.Controllers
{
    public class ProjetoController : BasePresenterController
    {

        private QryProjetos _qryProjetos;

        public ProjetoController()
        {
            _qryProjetos = new QryProjetos();
        }

        public ActionResult Index()
        {
            //string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var navegadorSession = new Navegador();

            var listaProjetos = _qryProjetos.GetProjetoToLists();

            ViewBag.ListaProjetos = new SelectList(listaProjetos, "GUID", "NUMERO");



            return View();
        }

   

    }
}