using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Comandos;
using LV_PresenterAPI.Consultas;
using LV_PresenterAPI.Models;
using RepositorioMongoDB;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class IndiceController : BasePresenterController
    {
        // GET: Indice
        public ActionResult IndexIndiceRev(string id)
        {

            //ValoresMudaIndice

           // ((ListaVerficacaoVM)Session["ListaVerificacaoVM"])

            MudaIndiceViewModel mudaIndiceViewModel = new MudaIndiceViewModel();

            mudaIndiceViewModel.GuidDocumento = id;

            return View(mudaIndiceViewModel);
        }

        [HttpPost]
        public ActionResult IndexIndiceRev(MudaIndiceViewModel mudado)
        {
         

            bool AindaNaoInseriuDesteIndice = QryListaVerificacao.Instancia((string)Session["GidLV"]).ObtemEstadoRevisoes().Indices
                .Where(x => x == mudado.Nome).Count() == 0 ? true : false;

              

            if (AindaNaoInseriuDesteIndice)
            {

                ValoresMudaIndice valor = new ValoresMudaIndice(AindaNaoInseriuDesteIndice, (string)Session["GidLV"], mudado.Nome);

                //CmdMudarIndice cmdMudar = new CmdMudarIndice();
                //cmdMudar.Muda(valor);
                new LV_NoSQL().MudaIndice((string)Session["GidLV"], mudado.Nome);

                var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri)
                {
                    Path = Url.Action("ListaDoc", "Lista"),
                    Query = null,
                };

                Uri uri = urlBuilder.Uri;
                string url = urlBuilder.ToString();

                return Json(new { env = url + "?id=" + (string)Session["GidLV"] }, JsonRequestBehavior.AllowGet);




            }


            return Content("");



        }


    }
}