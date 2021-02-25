using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppConfigLV.Controllers
{
    public class ConfiguracoesController : Controller
    {
        

        // GET: Configuracoes
        public ActionResult Index()
        {
            var listaCFGs = new ListaConfiguracoes().ListaCFGs;

            return View(listaCFGs);
        }

        // GET: Configuracoes/Details/5
        public ActionResult DetalhesConfiguracao(string guid)
        {
            Configuracao configuracao = new Configuracao(guid);
            
            return View(configuracao);
        }

        public ActionResult LVs(string guid)
        {
            
            var listaLVs = new ListaConfiguracoes().ListaCFGs.Find(x => x.GUID == guid).GetListaLVs();
            Session["guidCFG"] = guid;
            return View(listaLVs);
        }

        // GET: Configuracoes/Details/5
        public ActionResult DetalhesLV(string guid)
        {
            TipoLV configuracao = new TipoLV(guid);
            Session["guidLV"] = guid;
            return View(configuracao);
        }

        public ActionResult ListaPlanilhas(string guid)
        {
            string guidCFG = Session["guidCFG"].ToString();
            var listaPlanilas = new ListaConfiguracoes().ListaCFGs.Find(x => x.GUID == guidCFG).GetListaLVs().Find(x => x.GUID == guid).GetListaPlanilhas();

            return View(listaPlanilas);
        }

        public ActionResult DetalhesPlanilha(string guid)
        {
            Template planilha = new Template(guid);
            Session["guidPlanilha"] = guid;
            return View(planilha);
        }

        public ActionResult ListaGrupos(string guid)
        {
            string guidCFG = Session["guidCFG"].ToString();
            string guidLV = Session["guidLV"].ToString();
            var listaGrupos = new ListaConfiguracoes().ListaCFGs.Find(x => x.GUID == guidCFG).
                GetListaLVs().Find(x => x.GUID == guidLV).
                GetListaPlanilhas().Find(x => x.Guid == guid).ListaGrupos;

            return View(listaGrupos);
        }

        public ActionResult DetalhesGrupo(int ordenador)
        {
            string guidCFG = Session["guidCFG"].ToString();
            string guidLV = Session["guidLV"].ToString();
            string guidPlanilha = Session["guidPlanilha"].ToString();
            Session["ordenadorGrupo"] = ordenador;
            Grupo grupo = new ListaConfiguracoes().ListaCFGs.Find(x => x.GUID == guidCFG).
                GetListaLVs().Find(x => x.GUID == guidLV).
                GetListaPlanilhas().Find(x => x.Guid == guidPlanilha).ListaGrupos.Find(x => x.ORDENADOR == ordenador);

            return View(grupo);
        }

        public ActionResult ListaItens(int ordenador)
        {
            string guidCFG = Session["guidCFG"].ToString();
            string guidLV = Session["guidLV"].ToString();
            string guidPlanilha = Session["guidPlanilha"].ToString();
            var listaItens = new ListaConfiguracoes().ListaCFGs.Find(x => x.GUID == guidCFG).
                GetListaLVs().Find(x => x.GUID == guidLV).
                GetListaPlanilhas().Find(x => x.Guid == guidPlanilha).ListaGrupos.Find(x => x.ORDENADOR == ordenador).GetListaItens();

            return View(listaItens);
        }



        // GET: Configuracoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configuracoes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuracoes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Configuracoes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        

        // GET: Configuracoes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Configuracoes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
