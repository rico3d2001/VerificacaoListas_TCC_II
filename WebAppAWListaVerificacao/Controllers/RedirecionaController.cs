using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppAWListaVerificacao.Controllers
{
    public class RedirecionaController : Controller
    {
        // GET: Redireciona
        public ActionResult Index()
        {
            return Redirect("http://sap/AdminListaVerificacao");
        }
    }
}