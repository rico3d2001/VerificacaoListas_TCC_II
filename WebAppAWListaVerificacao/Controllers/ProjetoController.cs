using AppListaVerificacao.Interface;
using AutoMapper;
using LV_DI;
using LVModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Controllers
{
    public class ProjetoController : DefaultController
    {
        public ActionResult Index()
        {
            string login = HttpContext.User.Identity.Name.Split('\\')[1].ToUpper();

            

            List<ProjetoViewModel> listalocal;

            var navegadorSession = new Navegador();

            Session["Nav"] = navegadorSession;

            ViewBag.SiglaUser = login;
            Session["NivelCorrente"] = 0;
            ViewBag.Title = "Projeto";

            Session["BOTOES"] = new LayoutSession();

            using (var contextoProjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
            {
                contextoProjeto.Start(); //_banco);

                var listaprojetos = contextoProjeto.Query().OrderBy(x => x.NUMERO).ToList();

                listalocal = Mapper.Map<IEnumerable<ProjetoViewModel>>(listaprojetos).ToList();
            }

            ViewBag.ListaProjetos = new SelectList(listalocal, "GUID", "NUMERO");

            TempData["LayoutUsuario"] = "_LayoutProjeto";
            return View();
        }

        public JsonResult GetOSs(string id)
        {
            List<OS> listaOSs = new List<OS>();
            using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
            {
                contextoObjeto.Start();
                var projeto = contextoObjeto.ReturnByGUID(id);
                listaOSs = projeto.ListaOSs.Distinct().OrderBy(x => x.NUMERO).ToList();
                
            }

            return Json(new SelectList(listaOSs, "GUID", "NUMERO"));
        }
        
        public JsonResult GetAreas(string id)
        {
            List<Area> listaAreas = new List<Area>();
            using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
            {
                contextoObjeto.Start();
                var projeto = contextoObjeto.ReturnByGUID(id);
                listaAreas = projeto.ListaAreas.Distinct().OrderBy(x => x.NUMERO).ToList();

            }

            

            return Json(new SelectList(listaAreas, "GUID", "NUMERO"));
        }
        
        public JsonResult GetDisciplinas(string id)
        {
            var listaDisciplinas = listarDisciplinas();

            return Json(new SelectList(listaDisciplinas, "ID_DISCIPLINA", "SIGLA")); 
        }

        //provisorio
        public JsonResult GetTiposDocumentos(string id)
        {

            return Json(new SelectList(getTiposDocumentos(id), "GUID", "CODIGO"));
        }

        //rotina provisória
        private List<TipoDocumento> getTiposDocumentos(string guid_Projeto)
        {

            List<TipoDocumento> listaTipoDocumentos = new List<TipoDocumento>();
            Projeto projeto = null;
            using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
            {
                contextoObjeto.Start();
                projeto = contextoObjeto.ReturnByGUID(guid_Projeto);

                //}

                string numeroProjeto = projeto.NUMERO;

                //List<NumeroDocSNCLavalin> listaNumeroDocSNCLavalin = ListaNumeroDocSNCLavalin(projeto.NUMERO);
                //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().GetByProperty("PROJETO", projeto.NUMERO).ToList();

                /////

                List<NumeroDocSNCLavalin> listaNumeroDocSNCLavalin = null;
                using (var contextoLista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
                {
                    contextoLista.Start();
                    listaNumeroDocSNCLavalin = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().GetByProperty("PROJETO", numeroProjeto).ToList();//projeto.NUMERO).ToList();
                }




                /////

                List<string> listaStr = new List<string>();

                //var listaCodigos = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<CodigoDocumento>>().Query().ToList();

                var codAgrup = listaNumeroDocSNCLavalin.Distinct();

                //List<Documento> listaDocumentos =
                //    DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Documento>>().GetByProperty("GUID_PROJETO", guidProjeto).ToList();

                foreach (var item in codAgrup)
                {
                    var numero = item.NUMERO;
                    var strarray = numero.ToString().Split('-');
                    var str = strarray[3];
                    str = str.Substring(2, 2);
                    listaStr.Add(item.TIPO);
                }

                var agrupado = listaStr.Distinct().OrderBy(x => x).ToList();



                

                for (int i = 0; i < agrupado.Count; i++)
                {
                    listaTipoDocumentos.Add(new TipoDocumento(agrupado[i], i.ToString()));
                }

            }

            return listaTipoDocumentos;
        }

        private static List<Disciplina> listarDisciplinas()
        {
            List<Disciplina> lista;
            using (var contextoLista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>())
            {
                contextoLista.Start();
                lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>()
                .Query().OrderBy(x => x.SIGLA).ToList();
            }

            return lista;
        }




    }
}