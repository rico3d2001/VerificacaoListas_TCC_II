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
    public class AdicionaRevisaoController : BasePresenterController
    {
        // GET: AdicionaRevisao





        private int _tentativa;

        public AdicionaRevisaoController()
        {
            _tentativa = 0;

        }

        public ActionResult Index(string id)
        {

            var addRevisaoViewModel = new AddRevisaoViewModel();

            addRevisaoViewModel.GuidDocumento = id;

            return PartialView("Index", addRevisaoViewModel);
        }

        public ActionResult Adiciona(AddRevisaoViewModel addRevisaoViewModel)
        {
            //var qry = QryListaVerificacao.Instancia(_baseUrl, addRevisaoViewModel.GuidDocumento);



            //if (!estadoRevisoes.ExistemRevisoesNesteDocumento || !estadoRevisoes.PossuiRevisoesNaoConfirmadas)
            //{
            //if (estadoRevisoes.Indices.Count() == 0 || estadoRevisoes.Indices.FirstOrDefault(x => x == addRevisaoViewModel.Nome) == null)
            //{
            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            //var usuario = new QryUsuario(_baseUrl).ObtemUsuario(login);

            //if ((usuario != null && usuario.ISVERIFICADOR == 1))
            //{




            if (QryListaVerificacao.Instancia(addRevisaoViewModel.GuidDocumento).ObtemEstadoRevisoes().Indices.FirstOrDefault(x => x == addRevisaoViewModel.Nome) == null)
            {

                //var estadoRevisoes = QryListaVerificacao.Instancia(_baseUrl, addRevisaoViewModel.GuidDocumento).ObtemEstadoRevisoes();

                if (string.IsNullOrEmpty(addRevisaoViewModel.Nome) || addRevisaoViewModel.Nome.Length > 2)
                {

                    ViewBag.MessageError = "Preenchimento inadequado.";
                    return Content("");
                }

                //var estadoRevisoes = QryListaVerificacao.Instancia(_baseUrl, addRevisaoViewModel.GuidDocumento).ObtemEstadoRevisoes();
                if (QryListaVerificacao.Instancia(addRevisaoViewModel.GuidDocumento).ObtemEstadoRevisoes().ExistemRevisoesNesteDocumento 
                    && QryListaVerificacao.Instancia(addRevisaoViewModel.GuidDocumento).ObtemEstadoRevisoes().PossuiRevisoesNaoConfirmadas)
                {

                    ViewBag.MessageError = "Confirme a ultima revisão antes de acrescentar.";
                    return Content("");
                }
                else
                {
                    ValoresColunasRev valoresCriaColunaRevisao = new ValoresColunasRev(
                  addRevisaoViewModel.GuidDocumento, addRevisaoViewModel.Nome, new QryUsuario().ObtemUsuario(login).GUID);


                    //CmdCriaColunaRevisao cmdCriaColunaRevisao = new CmdCriaColunaRevisao(_baseUrl);
                    //_tentativa = cmdCriaColunaRevisao.Cria(valoresCriaColunaRevisao, _tentativa);

                    var lv = new LV_NoSQL().AcrescentarRevisoes_ViewModel(valoresCriaColunaRevisao);


                    CmdConfirmacaoRevisao.Instancia().Reset();
                    Session["AbriuNaoConfirmouAinda"] = true;
                }




            }
            else
            {
                ViewBag.MessageError = "Indice de revisão já cadastrado.";
                return Content("");
            }





            return SegueAdiante(addRevisaoViewModel);

        }



        private ActionResult SegueAdiante(AddRevisaoViewModel addRevisaoViewModel)
        {
            var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri)
            {
                Path = Url.Action("ListaDoc", "Lista"),
                Query = null,
            };

            Uri uri = urlBuilder.Uri;
            string url = urlBuilder.ToString();



            return Json(new { env = url + "?id=" + addRevisaoViewModel.GuidDocumento }, JsonRequestBehavior.AllowGet);
        }


    }
}