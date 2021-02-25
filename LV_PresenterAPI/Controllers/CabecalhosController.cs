using EntidadesRepositoriosLeitura;
using LV_PresenterAPI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LV_PresenterAPI.Controllers
{
    public class CabecalhosController : Controller
    {
        // GET: Cabecalhos
        public ActionResult Fixo(PlanilhaLVVM cab)
        {
            ViewBag.ImagePath = "~/imagens/SNC-Isologotipo.png";
            ViewBag.PlanilhaViewModel = cab;
            return View();
        }

        public ActionResult FixoDocumentoEncontrado(ListaVerficacaoVM cab)
        {
            ViewBag.ImagePath = "~/imagens/SNC-Isologotipo.png";
            //ViewBag.LV_ViewModel = cab;
            return View(cab);
        }

        public ActionResult Identificacao(PlanilhaLVVM cab)
        {
            ViewBag.ImagePath = "~/imagens/SNC-Isologotipo.png";
            ViewBag.PlanilhaViewModel = cab;
            return View();
        }

        public ActionResult Documento(ListaVerficacaoVM cab)
        {
            //ViewBag.CabecalhoViewModel = cab;
            //ViewBag.LV_ViewModel = cab;
            return View(cab);
        }


        public ActionResult Revisoes(List<ColunaRevisaoViewModel> listaColunas)
        {
            var divisor = listaColunas.Count == 0 ? 1 : listaColunas.Count;
            ViewBag.LarguraCalculada = 100 / divisor;

            List<string> listaIndicesRevisao = new List<string>();

            listaColunas.ForEach(x => listaIndicesRevisao.Add(x.IndiceRevisao));


            ViewBag.ListaIndicesRevisao = listaIndicesRevisao;

            return View();

        }

        public ActionResult RevisoesDOCEncontrado(ListaVerficacaoVM listaVer)
        {
        

            var divisor = listaVer.Colunas.Count == 0 ? 1 : listaVer.Colunas.Count;
            ViewBag.LarguraCalculada = 100 / divisor;

            List<string> listaIndicesRevisao = new List<string>();

            listaVer.Colunas.ForEach(x => listaIndicesRevisao.Add(x.INDICE_REV));


            ViewBag.ListaIndicesRevisao = listaIndicesRevisao;

            return View();

        }


    }
}