using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Comandos;
using LV_PresenterAPI.Consultas;
using RepositorioMongoDB;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class RetomarEdicaoController : BasePresenterController
    {
        // GET: RetomarEdicao
        public async Task<ActionResult> Index(string id)
        {


            TempData["Retomada"] = true;


            //CmdRetomadaRevisao.Instancia().Retomar(new ValoresConfirma(id, false, "", "", QryListaVerificacao.Instancia(id).ObtemEstadoRevisoes().Indices.Last(),"NomeUser"));
            new LV_NoSQL().RetomarVM(id);

            CmdConfirmacaoRevisao.Instancia().Reset();
            Session["AbriuNaoConfirmouAinda"] = true;
            
          

            Session["PossuiRevisoes"] = QryListaVerificacao.Instancia( id).ObtemEstadoRevisoes().ExistemRevisoesNesteDocumento;
            Session["ExistemRevisoesNaoConfirmadas"] = true;
            string guid = id;
            return RedirectToAction("ListaDoc", "Lista", new { id = guid });


           
        }
    }
}