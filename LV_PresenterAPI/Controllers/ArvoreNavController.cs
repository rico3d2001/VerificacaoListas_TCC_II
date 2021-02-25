using LV_PresenterAPI.Models.Navegacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class ArvoreNavController : BasePresenterController
    {
        // GET: ArvoreNav
        public ActionResult _NavegarArvore(int nivel, string guid)
        {

            var navegador = (Navegador)Session["Nav"];

            if (nivel < 3)
            {
                navegador.PlanilhaEscolhida = null;
            }

        
            //var lista = navegador.GetDadosArvore("http://sap/ApiLV/");
            var lista = navegador.GetDadosArvore("https://localhost:44355");

            return PartialView(lista);
        }
    }
}