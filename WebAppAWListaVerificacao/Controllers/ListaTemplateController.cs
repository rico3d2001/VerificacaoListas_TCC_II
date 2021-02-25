
using AppListaVerificacao.Interface;
using LV_DI;
using LV14FluentNHB.Service;
using LVModel;
using System.Linq;
using System.Web.Mvc;
using Unity;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class ListaTemplateController : ListaController
    {
        protected Planilha _planilha;


        public ActionResult IndexLT(string guidPlanilha)
        {

            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            //Usuario usuario = getUsuario(login);


            using (var contextoPlanilha = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>())
            {
                contextoPlanilha.Start();



                _planilha = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Planilha>>().ReturnByGUID(guidPlanilha);



                Session["NivelCorrente"] = 3;




                Session["Login"] = login;


                ViewBag.PlanilhaAtivada = true;


                ViewBag.DocumentoAtivado = false;

                ViewBag.ImagemStatusViewModel = _imagemStatusViewModel;



                ViewBag.CabecalhoViewModel = new CabecalhoApp().GetCabecalhoViewModel(_planilha);

                ViewBag.List_ColunaRevisaoViewModel = new ListaColunasTemplate(_planilha).ObtemLista_ColunaRevisaoDocumento();


                ViewBag.GuidPlanilha = _planilha.GUID;

                ViewBag.SiglaUser = login;
                TempData["LayoutUsuario"] = "_LayoutPlanilhaEncontrada";

            }

            return View();


        }
    }
}