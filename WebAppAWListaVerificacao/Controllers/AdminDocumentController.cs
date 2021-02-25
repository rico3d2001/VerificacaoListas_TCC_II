using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class AdminDocumentController : Controller
    {
        // GET: AdminDocument
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaLvs(string projeto, string os, string area, string disciplina, string tipo, string Seguencial)
        {
            var lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().Query().OrderBy(x => x.NUMERO).ToList();
            
            if (projeto != null && projeto != "")
            {
                lista = lista.Where(x => x.PROJETO == projeto).ToList();
            }
            if (os != null && os != "")
            {
                lista = lista.Where(x => x.OS == os).ToList();
            }
            if (area != null && area != "")
            {
                lista = lista.Where(x => x.AREA == area).ToList();
            }
            if (disciplina != null && disciplina != "")
            {
                lista = lista.Where(x => x.DISCIPLINA == disciplina).ToList();
            }
            if (tipo != null && tipo != "")
            {
                lista = lista.Where(x => x.TIPO == tipo).ToList();
            }
            if (Seguencial != null && Seguencial != "")
            {
                lista = lista.Where(x => x.SEQUENCIAL == Seguencial).ToList();
            }


            List<NumeroSncViewModel> listaModel = new List<NumeroSncViewModel>();
            //aqui
            foreach (var item in lista)
            {
                if (item.GUID_ULTIMA_CONFIRMACAO != null)
                {
                    var confirmação = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Confirmacao>>().ReturnByGUID(item.GUID_ULTIMA_CONFIRMACAO);

                    listaModel.Add(new NumeroSncViewModel()
                    {
                        NUMERO = item.NUMERO,
                        Confirmacao1 = confirmação.GUID_USUARIO1,
                        Confirmacao2 = confirmação.GUID_USUARIO2,
                        IndiceUltimaRevisao = confirmação.INDICE_REV,
                        Data = confirmação.DATA.ToShortDateString()
                    });
                }

            }

            return View(listaModel);
        }

        // GET: AdminDocument/Edit/5
        public ActionResult Edit(string doc)
        {
            var lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>().GetByProperty("DOC_VERIFICADO", doc).ToList();

            var documento = lista.First();

            return View(documento);
        }

        // POST: AdminDocument/Edit/5
        [HttpPost]
        public ActionResult Edit(ListaVerificacao documento)
        {
            try
            {

                DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>()
                    .Update(documento);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminDocument/Delete/5
        public ActionResult Delete(string doc)
        {
            var lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>().GetByProperty("DOC_VERIFICADO", doc).ToList();

            var documento = lista.First();

            return View(documento);

            DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ListaVerificacao>>().Delete(lista.First());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(ListaVerificacao documento)
        {
            try
            {
                //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Documento>>().Delete(lista.First());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
