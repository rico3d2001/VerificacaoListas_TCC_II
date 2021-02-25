using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class CabecalhosController : Controller
    {


        public ActionResult Fixo(CabecalhoViewModel cab)
        {
            ViewBag.CabecalhoViewModel = cab;
            return View();
        }


        public ActionResult Identificacao(CabecalhoViewModel cab)
        {
            ViewBag.CabecalhoViewModel = cab;
            return View();
        }

        public ActionResult Documento(CabecalhoViewModel cab)
        {
            ViewBag.CabecalhoViewModel = cab;
            return View();
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
    }
}