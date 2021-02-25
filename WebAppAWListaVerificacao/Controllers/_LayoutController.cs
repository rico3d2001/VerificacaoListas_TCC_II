using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class _LayoutController : Controller
    {
        public ActionResult _Navegar(int nivel, string guid)
        {



            var navegador = (Navegador)Session["Nav"];

            if(nivel < 3)
            {
                navegador.PlanilhaEscolhida = null;
            }

            var lista = navegador.ListaConfiguracoes();

            

            return PartialView(lista);
        }
    }
}