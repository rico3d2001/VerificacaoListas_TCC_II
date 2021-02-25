using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity;

namespace WebAppAWListaVerificacao.Controllers
{
    public class ValidaBuscaDocumentoController : DefaultController
    {
        public ActionResult ValidaBuscaDocumento_Projeto(string Projeto)
        {

            string guid_logPC = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            //var usuario = getUsuario(guid_logPC);
            var isVerificador = getUsuario(guid_logPC).GetBoolIsVerificador();

            var li = PrencheSessionNumero();

            li[0] = Projeto;

            bool resp = testaPrenchimento(li, isVerificador);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidaBuscaDocumento_OS(string OS)
        {
 
            string guid_logPC = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var isVerificador = getUsuario(guid_logPC).GetBoolIsVerificador();

            var li = PrencheSessionNumero();

            li[2] = OS;

            bool resp = testaPrenchimento(li, isVerificador);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidaBuscaDocumento_Area(string Area)
        {
           
            string guid_logPC = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var isVerificador = getUsuario(guid_logPC).GetBoolIsVerificador();

            var li = PrencheSessionNumero();

            li[4] = Area;

            bool resp = testaPrenchimento(li, isVerificador);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidaBuscaDocumento_SiglaDisciplina(string SiglaDisciplina)
        {
            
            string guid_logPC = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var isVerificador = getUsuario(guid_logPC).GetBoolIsVerificador();

            var li = PrencheSessionNumero();

            li[6] = SiglaDisciplina;

            bool resp = testaPrenchimento(li, isVerificador);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidaBuscaDocumento_TipoDocumento(string TipoDocumento)
        {
          
            string guid_logPC = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var isVerificador = getUsuario(guid_logPC).GetBoolIsVerificador();

            var li = PrencheSessionNumero();

            li[7] = TipoDocumento;

            bool resp = testaPrenchimento(li, isVerificador);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidaBuscaDocumento(string Sequencial)
        {
            
            string guid_logPC = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            var isVerificador = getUsuario(guid_logPC).GetBoolIsVerificador();

            var li = PrencheSessionNumero();

            li[9] = Sequencial;

            bool resp = testaPrenchimento(li, isVerificador);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        private static bool testaPrenchimento(List<string> li, bool isVerificador)
        {
            string num = string.Empty;
            bool resp = false;

            if (li.Contains("00"))
            {
                resp = true;
            }
            else
            {
                foreach (var item in li)
                {
                    num = num + item;
                }

                var documento = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>()
                    .GetByProperty("DOC_VERIFICADO", num).FirstOrDefault();

                if (documento != null)
                {
                    resp = true;
                }
                else if (isVerificador)
                {
                    resp = true;
                }
            }

            return resp;
        }

        private List<string> PrencheSessionNumero()
        {
            List<string> li;
            if (Session["NumeroDesenhoCorrente"] == null)
            {
                li = new List<string> { "00", "-", "00", "-", "00", "-", "00", "00", "-", "00" };
                Session["NumeroDesenhoCorrente"] = li;
            }
            else
            {
                li = (List<string>)Session["NumeroDesenhoCorrente"];
            }

            return li;
        }


    }
}