using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Comandos;
using LV_PresenterAPI.Consultas;
using LV_PresenterAPI.Models;
using LVModel;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class PegaDocController : BasePresenterController
    {






        private PlanilhaLVVM _planilha;
        public PegaDocController()
        {
            
        }


        // GET: PegaDoc
        public ActionResult Index()
        {
            Session["ListaVerificacaoVM"] = null;

            _planilha = (PlanilhaLVVM)Session["planilha"];

            DocViewModel docViewModel = new DocViewModel();

            docViewModel.SiglaDisciplina = _planilha.CabecalhoApp.SiglaDisciplina;

            docViewModel.Projeto = ((ProjetoVM)Session["Projeto"]).NUMERO;

            docViewModel.GuidPlanilha = _planilha.GUID;


            return View(docViewModel);
        }

        [HttpPost]
        public ActionResult Index(DocViewModel docViewModel)
        {
           

           

            if (ModelState.IsValid)
            {



                QryBuscaNumeroDoc qryBuscaNumeroDoc = new QryBuscaNumeroDoc();

                NumeroDocSNCLavalin numero = new NumeroDocSNCLavalin(
                    docViewModel.Projeto,
                    docViewModel.OS,
                    docViewModel.Area,
                    docViewModel.SiglaDisciplina,
                    docViewModel.TipoDocumento,
                    docViewModel.Sequencial
                    );

                var num = qryBuscaNumeroDoc.VerificaNumeroNoBanco(numero.ToString());

                //QryListaVerificacao qryListaVerificacao = QryListaVerificacao.Instancia(_baseUrl, docViewModel.GuidDocumento);

                if (num != null)
                {

                 
                    var lvLocal = new LV_NoSQL().BuscarLV_ViewModel(num.GUID_LV);
                    if (lvLocal == null)
                    {
                        var lv = MySQLConsultaListaVerificacao.ObtemListaSimples(num);
                        if (lv != null)
                        {
                            var estado = QryListaVerificacao.Instancia(lv.GUID).ObtemEstadoRevisoes();

                            Session["PossuiRevisoes"] = estado.ExistemRevisoesNesteDocumento;
                            Session["ExistemRevisoesNaoConfirmadas"] = estado.PossuiRevisoesNaoConfirmadas;

                            Session["AbriuNaoConfirmouAinda"] = true;
                            return RedirectToAction("ListaDoc", "Lista", new { id = lv.GUID });
                        }

                        return View();

                    }
                    else
                    {
                        var estado = QryListaVerificacao.Instancia(docViewModel.GuidDocumento).ObtemEstadoRevisoes();

                        Session["PossuiRevisoes"] = estado.ExistemRevisoesNesteDocumento;
                        Session["ExistemRevisoesNaoConfirmadas"] = estado.PossuiRevisoesNaoConfirmadas;

                        Session["AbriuNaoConfirmouAinda"] = true;
                        return RedirectToAction("ListaDoc", "Lista", new { id = num.GUID_LV });
                    }

                   
                }
                else
                {
                    string novoGuid = Guid.NewGuid().ToString();

                    ValoresComandoCriaLV valor = new ValoresComandoCriaLV()
                    {
                        NovoGuidLV = novoGuid,
                        GuidPlanilha = docViewModel.GuidPlanilha,
                        NumeroSNC = numero.ToString()
                    };

                    new CmdSalvaListaVerificacao().SalvaLV(valor);//(_baseUrl).SalvaLV(valor);


                    
                    

                    Session["PossuiRevisoes"] = false;
                 
                    Session["AbriuNaoConfirmouAinda"] = true;
                    TempData["DocumentoNovo"] = true;
                    Session["Projeto"] = new QryProjetos().GetProjetoApp(((ProjetoVM)Session["Projeto"]).GUID);
                    return RedirectToAction("ListaDoc", "Lista", new { id = novoGuid });
                }

                
            }


           
            return View();
        }

      
    }
}