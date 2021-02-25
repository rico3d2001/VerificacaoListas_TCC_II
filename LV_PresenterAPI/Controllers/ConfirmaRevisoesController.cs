using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Consultas;
using System;
using System.Linq;
using System.Web.Mvc;
using LV_PresenterAPI.Comandos;
using RepositorioMongoDB;

namespace LV_PresenterAPI.Controllers
{
    public class ConfirmaRevisoesController : BasePresenterController
    {
        private readonly bool abriu_e_nao_confirmou_ainda;
        public ConfirmaRevisoesController()
        {
            abriu_e_nao_confirmou_ainda = true;
        }
        // GET: ConfirmaRevisoes
        public ActionResult Index(string id)
        {

            CmdConfirmacaoRevisao.Instancia().Reset();

            ViewBag.PodeConfirmar = false;

            var confDupla = (int)Session["VerificadorUnico"] == 0 ? true : false;

            //var qry = new QryListaVerificacao(_baseUrl, id);
            var estadoRevisoes = QryListaVerificacao.Instancia(id).ObtemEstadoRevisoes();
            if (estadoRevisoes.NaoTemRevisoesIndefinidas)
            {

                if (confDupla)
                {
                    if (QryListaVerificacao.Instancia(id).ObtemEstadoConfirmacoes().HouveSomentePrimeiraConfirmacaoColunaAtual)
                    {
                        ViewBag.IndiceConfirmar = estadoRevisoes.Indices.Last();
                        ViewBag.PodeConfirmar = true;
                        ViewBag.MensagemErro = "Esta é a segunda confirmação para esta LV.";
                        return View();
                    }
                    else
                    {
                        ViewBag.IndiceConfirmar = estadoRevisoes.Indices.Last();
                        ViewBag.PodeConfirmar = true;
                        ViewBag.MensagemErro = "Para esta LV será necessária uma segunda confirmação.";
                        return View();
                    }

                }
                else
                {
                    ViewBag.IndiceConfirmar = estadoRevisoes.Indices.Last();
                    ViewBag.PodeConfirmar = true;
                    ViewBag.MensagemErro = "Esta é uma LV de confirmação única.";
                    return View();
                }
            }
            else
            {
                ViewBag.IndiceConfirmar = estadoRevisoes.Indices.Last();
                ViewBag.PodeConfirmar = false;
                ViewBag.MensagemErro = "Defina todos os itens da lista antes de confirmar.";
                return View();
            }





        }


        //public ActionResult MsgItensIndefinidos()
        //{
        //    return View();
        //}


        public ActionResult Confirma()//async Task<ActionResult> Confirma()
        {
            //QryListaVerificacao.Instancia(_baseUrl, (string)Session["GidLV"])
            //var pesquisa = new QryListaVerificacao(_baseUrl, (string)Session["GidLV"]);

            //var listaPossuiRevisoes = QryListaVerificacao.Instancia((string)Session["GidLV"]).ListaVerificacaoApp;
            //var estadoConfirmacoes = pesquisa.ObtemEstadoConfirmacoes((string)Session["GidLV"]);
            //var estadoRevisoes = QryListaVerificacao.Instancia((string)Session["GidLV"]).ObtemEstadoRevisoes();




            //if (QryListaVerificacao.Instancia((string)Session["GidLV"]).ObtemEstadoRevisoes().NaoTemRevisoesIndefinidas)
            //{



            var login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();
            var usuario = new QryUsuario().ObtemUsuario(login);



            //ValoresConfirma valoresConfirma = new ValoresConfirma(
            //    ((string)Session["GidLV"]),
            //    ((int)Session["VerificadorUnico"]) == 0 ? true : false,
            //    usuario.GUID,
            //    Guid.NewGuid().ToString(),
            //    QryListaVerificacao.Instancia((string)Session["GidLV"]).ObtemEstadoRevisoes().Indices.Last(),
            //    usuario.NOME);


            if ((bool)Session["AbriuNaoConfirmouAinda"])
            {


                new LV_NoSQL().ConfirmacaoRevisaoVM((string)Session["GidLV"], usuario);

                    //var cols = new LV_NoSQL().ConfirmacaoRevisaoVM(new ValoresConfirma(
                    //             (string)Session["GidLV"],
                    //             usuario.GUID,
                    //             ((int)Session["VerificadorUnico"]) == 0 ? true : false,
                    //             usuario.GUID,
                    //             Guid.NewGuid().ToString(),
                    //             QryListaVerificacao.Instancia((string)Session["GidLV"]).ObtemEstadoRevisoes().Indices.Last(),
                    //             usuario.NOME)).Colunas.OrderBy(x => x.ORDENADOR).Last();
                Session["AbriuNaoConfirmouAinda"] = false;
                
               
            }
            //if(abriu_e_nao_confirmou_ainda)
            //{
            //    CorfirmarRevisoes.Instancia.Corfirmar(
            //        HttpContext.User.Identity.Name.Split('\\')[1].ToUpper(),
            //        (int)Session["VerificadorUnico"] == 1 ? true : false,
            //         (string)Session["GidLV"]);

            //    //abriu_e_nao_confirmou_ainda = CorfirmarRevisoes(login, 
            //    //    (int)Session["VerificadorUnico"] == 1 ? true : false, 
            //    //    abriu_e_nao_confirmou_ainda, (string)Session["GidLV"]);
            //}


            var urlBuilder = new UriBuilder(Request.Url.AbsoluteUri)
            {
                Path = Url.Action("ListaDoc", "Lista"),
                Query = null,
            };

            //Uri uri = urlBuilder.Uri;
            string url = urlBuilder.ToString();


            return Json(new { env = url + "?id=" + (string)Session["GidLV"] }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //return Content("");
            //}




        }

        private static bool CorfirmarRevs(string login, bool verificadorUnico, bool abriu_e_nao_confirmou_ainda, string guidLV)
        {
            int tentativa = 0;
            if (verificadorUnico && abriu_e_nao_confirmou_ainda)
            {


                var usuario = new QryUsuario().ObtemUsuario(login);
                
                //CmdConfirmacaoRevisao.Instancia().ConfirmaViewModel(new ValoresConfirma(
                //guidLV,
                //verificadorUnico,
                //usuario.GUID,
                //Guid.NewGuid().ToString(),
                //QryListaVerificacao.Instancia(guidLV).ObtemEstadoRevisoes().Indices.Last(),
                //usuario.NOME));


                abriu_e_nao_confirmou_ainda = false;

            }
            else if (!verificadorUnico && abriu_e_nao_confirmou_ainda)
            {

                var lista = QryListaVerificacao.Instancia(guidLV).ListaVerificacaoApp;

                var ultimaConfirmacao = lista.Confirmacoes.OrderBy(x => x.CONFIRMACAO_ORDENADOR).Last();



                if (string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER1))
                {
                    var usuario = new QryUsuario().ObtemUsuario(login);
                    new LV_NoSQL().ConfirmacaoRevisaoVM(guidLV, usuario);
                    //  var cols = new LV_NoSQL().ConfirmacaoRevisaoVM(new ValoresConfirma(
                    //guidLV,
                    //verificadorUnico,
                    //usuario.GUID,
                    //Guid.NewGuid().ToString(),
                    //QryListaVerificacao.Instancia(guidLV).ObtemEstadoRevisoes().Indices.Last(),
                    //usuario.NOME)).Colunas.OrderBy(x => x.ORDENADOR).Last();

                    abriu_e_nao_confirmou_ainda = false;

                }
                else if (!string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER1)
                    && string.IsNullOrEmpty(ultimaConfirmacao.CONFIRMACAO_ID_USER2)
                    && abriu_e_nao_confirmou_ainda)
                {

                    var usuario = new QryUsuario().ObtemUsuario(login);
                    new LV_NoSQL().ConfirmacaoRevisaoVM(guidLV, usuario);
                    //var cols = new LV_NoSQL().ConfirmacaoRevisaoVM(new ValoresConfirma(
                    //      guidLV,
                    //      verificadorUnico,
                    //      usuario.GUID,
                    //      Guid.NewGuid().ToString(),
                    //      QryListaVerificacao.Instancia(guidLV).ObtemEstadoRevisoes().Indices.Last(),
                    //      usuario.NOME)).Colunas.OrderBy(x => x.ORDENADOR).Last();


                    //CmdConfirmacaoRevisao.Instancia().ConfirmaViewModel(new ValoresConfirma(
                    //guidLV,
                    //verificadorUnico,
                    //usuario.GUID,
                    //Guid.NewGuid().ToString(),
                    //QryListaVerificacao.Instancia(guidLV).ObtemEstadoRevisoes().Indices.Last(),
                    //usuario.NOME));
                }









            }

            return abriu_e_nao_confirmou_ainda;
        }











        //CmdConfirmacaoRevisao

        //public ActionResult Confirma()//ConfirmaColunaViewModel confirmaViewModel)
        //{

        //}

    }
}