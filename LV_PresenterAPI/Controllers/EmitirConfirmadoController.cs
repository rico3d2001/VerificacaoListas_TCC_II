using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Comandos;
using LV_PresenterAPI.Consultas;
using RepositorioMongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class EmitirConfirmadoController : Controller
    {
        // GET: EmitirConfirmado
        public ActionResult Index(string id)
        {
            //var login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();
            //var usuario = new QryUsuario().ObtemUsuario(login);

            //CmdConfirmacaoRevisao.Instancia().EmiteViewModel(id,usuario.GUID);
            //Session["AbriuNaoConfirmouAinda"] = true;
            //string guid = id;
            //return RedirectToAction("ListaDoc", "Lista", new { id = guid });

            List<ListaVerficacaoVM> lista = new LV_NoSQL().ListaLVS();



            return View(lista);
        }
    }
}