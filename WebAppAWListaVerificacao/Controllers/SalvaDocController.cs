using System.Web.Mvc;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class SalvaDocController : Controller
    {
        CabecalhoApp _cabecalhoApp;

        protected ImagemStatusViewModel _imagemStatusViewModel;

        public SalvaDocController()
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


        public ActionResult Index()
        {

            var navegador = (Navegador)Session["Nav"];
        
                return RedirectToAction("Index", "Lista", new { nivel = 3, guid = navegador.PlanilhaEscolhida.GUID });
            
        }

        [HttpPost]
        public ActionResult Index(DocViewModel docViewModel)
        {
            var navegadorSession = (Navegador)Session["Nav"];


         

            return RedirectToAction("Index", "Lista", new { nivel = 3, guid = navegadorSession.PlanilhaEscolhida.GUID });
        }
    }
}