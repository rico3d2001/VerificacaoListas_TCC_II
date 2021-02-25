using AppExcel;
using AppListaVerificacao.Interface;
using LV_DI;
using LVModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace WebAppAWListaVerificacao.Controllers
{
    public class InsereTemplateController : Controller
    {
        // GET: InsereTemplate
        public ActionResult Index()
        {

            ////insere usuario
            //Usuario usuario = new Usuario()
            //{
            //    GUID = "RRP",
            //    NOME = "Ricardo R. Pinto",
            //    SIGLA = "RONAR",
            //    SENHA = "cliente123",
            //    ISVERIFICADOR = 1,
            //    ISCONFIGURADOR = 1,
            //    ISGESTOR = 1
            //};

            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Usuario>>().Insert(usuario);

            


            ////preparar insercao disciplinas
            //var disciplinaDB1 = new Disciplina()
            //{ ID_DISCIPLINA = 1,NOME = "Mecânica", SIGLA = "45"};
            //var disciplinaDB2 = new Disciplina()
            //{ ID_DISCIPLINA = 2, NOME = "Tubulação", SIGLA = "46" };
        
            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>().Insert(disciplinaDB1);
            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>().Insert(disciplinaDB1);
            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>().Insert(disciplinaDB1);

            //EstadoRevisao v = new EstadoRevisao()
            //{ ID_ESTADO = 1, NOME = "V", DESCRICAO = "VERIFICADO" };
            //EstadoRevisao nd = new EstadoRevisao()
            //{ ID_ESTADO = 2, NOME = "ND", DESCRICAO = "DADO NÃO DISPONÍVEL" };
            //EstadoRevisao na = new EstadoRevisao()
            //{ ID_ESTADO = 3, NOME = "NA", DESCRICAO = "NÃO APLICÁVEL" };
            //EstadoRevisao x = new EstadoRevisao()
            //{ ID_ESTADO = 4, NOME = "X", DESCRICAO = "ERRO" };
            //EstadoRevisao i = new EstadoRevisao()
            //{ ID_ESTADO = 5, NOME = "I", DESCRICAO = "INDEFINIDO" };
            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<EstadoRevisao>>().Insert(v);
            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<EstadoRevisao>>().Insert(nd);
            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<EstadoRevisao>>().Insert(na);
            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<EstadoRevisao>>().Insert(x);
            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<EstadoRevisao>>().Insert(i);


            //DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>().Insert(disciplinaDB1);


            LeitorDiretorios.Ler(@"D:\Trabalho\Db4OBancos\Planilhas Verificacao");

            return View();
        }
    }
}