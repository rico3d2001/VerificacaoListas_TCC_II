using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Consultas;
using LVModel;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class BuscaDocumentoController : BasePresenterController
    {

        QryBuscaNumeroDoc _qryBuscaNumeroDoc;

        public BuscaDocumentoController()
        {
            _qryBuscaNumeroDoc = new QryBuscaNumeroDoc();
        }

      
        public ActionResult Index(string guidos, string guidarea, string iddisciplina, string guidtipo, string sequencial)
        {



            
             var projeto = (ProjetoVM)Session["Projeto"];


            var numeroDocSNCLavalin = new NumeroDocSNCLavalin();


            if (!string.IsNullOrEmpty(guidos) && !string.IsNullOrEmpty(guidarea) 
                && !string.IsNullOrEmpty(iddisciplina) && !string.IsNullOrEmpty(guidtipo) 
                && !string.IsNullOrEmpty(sequencial))
            {

                if(sequencial.Length >= 4 && sequencial.Length <= 6)
                {
                    int idDiscip = int.Parse(iddisciplina);
                    string numeroCompleto = FormaNumero(guidos, guidarea, guidtipo, sequencial.ToUpper(), projeto.Disciplinas, projeto, idDiscip);

                    numeroDocSNCLavalin = new NumeroDocSNCLavalin(numeroCompleto);

                    QryBuscaNumeroDoc qryBuscaDoc = new QryBuscaNumeroDoc();

                    NumeroSNCLV num = qryBuscaDoc.VerificaNumeroNoBanco(numeroDocSNCLavalin.NUMERO);

                    if (num != null)
                    {
                        ViewBag.resp = true;
                        ViewBag.NumeroDocumentoCorrente = numeroDocSNCLavalin.ToString();

                        ViewBag.MSGErroBusca = "";
                       
                        return PartialView("Index", numeroDocSNCLavalin);
                    }

                    ViewBag.resp = false;
                    ViewBag.MSGErroBusca = "Nenhum documento encontrado";
                    return PartialView("Index", numeroDocSNCLavalin);

                }

                ViewBag.MSGErroBusca = "Sequencial deve ter de 4 a 6 caracteres.";

                //TempData["MSGErroBusca"] = "Sequencial deve ter de 4 a 6 caracteres.";
                return PartialView("Index", numeroDocSNCLavalin);

                //return RedirectToAction("BuscaLV", "Inicial", new { id = projeto.GUID });

            }
          







            ViewBag.MSGErroBusca = "Há algum campo vazio ou não selecionado.";
            //TempData["MSGErroBusca"] = "Há algum campo vazio ou não selecionado.";
            return PartialView("Index", numeroDocSNCLavalin);

            //return RedirectToAction("BuscaLV", "Inicial", new { id = projeto.GUID });




        }

        [HttpPost]
        public ActionResult Index(NumeroDocSNCLavalin numeroDocSNCLavalin)
        {

            

            QryBuscaNumeroDoc qryBuscaDoc = new QryBuscaNumeroDoc();

            NumeroSNCLV num = qryBuscaDoc.VerificaNumeroNoBanco(numeroDocSNCLavalin.NUMERO);

            Session["PossuiRevisoes"] = false;
            Session["ExistemRevisoesNaoConfirmadas"] = false;

            //QryListaVerificacao qryListaVerificacao = new QryListaVerificacao(_baseUrl, num.GUID_LV);

            if (num != null)
            {

               

                Session["GidLV"] = num.GUID_LV;
                //Session["PossuiRevisoes"] = qryListaVerificacao.ListaPossuiRevisoes;
                //Session["ExistemRevisoesNaoConfirmadas"] = qryListaVerificacao.ObtemEstadoRevisoes().PossuiRevisoesNaoConfirmadas;





                var lvLocal = new LV_NoSQL().BuscarLV_ViewModel(num.GUID_LV);
                if (lvLocal == null)
                {
                    //var lv1 = ConsultaListaVerificacao.ObtemListaSimples(num);
                   var lv = MySQLConsultaListaVerificacao.ObtemListaSimples(num);

                    if (lv != null)
                    {
                        var estado = QryListaVerificacao.Instancia(lv.GUID).ObtemEstadoRevisoes();

                        Session["PossuiRevisoes"] = estado.ExistemRevisoesNesteDocumento;
                        Session["ExistemRevisoesNaoConfirmadas"] = estado.PossuiRevisoesNaoConfirmadas;

                        Session["AbriuNaoConfirmouAinda"] = true;
                        return RedirectToAction("ListaDoc", "Lista", new { id = lv.GUID });
                    }
                    //Session["AbriuNaoConfirmouAinda"] = true;
                    //return RedirectToAction("ListaDoc", "Lista", new { id = lv.GUID });
                }
                else
                {
                    var estado = QryListaVerificacao.Instancia(num.GUID_LV).ObtemEstadoRevisoes();

                    Session["PossuiRevisoes"] = estado.ExistemRevisoesNesteDocumento;
                    Session["ExistemRevisoesNaoConfirmadas"] = estado.PossuiRevisoesNaoConfirmadas;

                    Session["AbriuNaoConfirmouAinda"] = true;
                    return RedirectToAction("ListaDoc", "Lista", new { id = num.GUID_LV });
                }

            }

            return RedirectToAction("ListaDoc", "Lista", new { id = num.GUID_LV });

        

        }

        

        private static string FormaNumero(string guidos, string guidarea, string guidtipo, string sequencial, List<DisciplinaVM> listaDisciplinas, ProjetoVM projeto, int idDiscip)
        {
            return projeto.NUMERO
              + "-" + projeto.OSs.Find(x => x.GUID == guidos).NUMERO
              + "-" + projeto.Areas.Find(x => x.GUID == guidarea).NUMERO
              + "-" + listaDisciplinas.Find(x => x.ID_DISCIPLINA == idDiscip).SIGLA
              + projeto.Tipos.Find(x => x.GUID == guidtipo).CODIGO
              + "-" + sequencial;
        }
    }
}