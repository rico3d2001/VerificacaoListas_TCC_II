using EntidadesRepositoriosLeitura;
using LVModel;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiLV.Controllers
{
    public class ApiProjetoController : ApiController
    {
        // GET: /api/Projetos
        [Route("api/Projetos")]
        public IEnumerable<ProjetoToListDTO> GetProjetos()
        {
            return new LV_NoSQL().PegaProjetoToListDTO();

            //return ConsultaListaProjetos.ObtemListaProjetos();
            //return new ProjetoListaMDB().Buscar();

            //return //QryListaProjetos.ListaProjetos();
        }

        // GET: /api/Projeto/eb6e5252-f751-4e1e-a59f-278d13c67d2d
                           //eb6e5252-f751-4e1e-a59f-278d13c67d2d
        // GET: MySQL em casa /api/Projeto/a70588d0-c157-424a-aa98-157683d85350
        [Route("api/Projeto/{guidProjeto}")]
        public ProjetoVM GetProjeto(string guidProjeto)
        {
            return MySQLConsultaProjeto.ObtemProjeto(guidProjeto);

         
            //return QryProjetoBusca.GetProjetoApp(guidProjeto);
        }



        [Route("api/Disciplinas")]
        public IEnumerable<Disciplina> GetDisciplinas()
        {
            List<Disciplina> lista = new List<Disciplina>();
            //using (var contextoLista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>())
            //{
            //    contextoLista.Start();
            //    lista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Disciplina>>()
            //    .Query().OrderBy(x => x.SIGLA).ToList();
            //}

            return lista;
        }



        //// GET: /api/Areas/eb6e5252-f751-4e1e-a59f-278d13c67d2d
        //[Route("api/Areas/{guidProjeto}")]
        //public IEnumerable<AreaApp> GetAreas(string guidProjeto)
        //{
        //    var config = new MapperConfiguration(
        //          cfg =>
        //          {
        //              cfg.CreateMap<Area, AreaApp>();

        //          });
        //    var mapper = config.CreateMapper();

        //    List<AreaApp> listaAreas = new List<AreaApp>();

        //    using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
        //    {
        //        contextoObjeto.Start();
        //        var projeto = contextoObjeto.ReturnByGUID(guidProjeto);
        //        var listaContexto = projeto.ListaAreas.Distinct().OrderBy(x => x.NUMERO).ToList();
        //        listaAreas = mapper.Map<List<AreaApp>>(listaContexto);

        //    }

        //    return listaAreas;
        //}

        //// GET: /api/OS/eb6e5252-f751-4e1e-a59f-278d13c67d2d
        //[Route("api/OS/{guidProjeto}")]
        //public IEnumerable<OSApp> GetOSs(string guidProjeto)
        //{
        //    var config = new MapperConfiguration(
        //          cfg =>
        //          {
        //              cfg.CreateMap<OS, OSApp>();

        //          });
        //    var mapper = config.CreateMapper();

        //    List<OSApp> listaOSs = new List<OSApp>();

        //    using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
        //    {
        //        contextoObjeto.Start();
        //        var projeto = contextoObjeto.ReturnByGUID(guidProjeto);
        //        var listaContexto = projeto.ListaOSs.Distinct().OrderBy(x => x.NUMERO).ToList();
        //        listaOSs = mapper.Map<List<OSApp>>(listaContexto);

        //    }

        //    return listaOSs;
        //}

        //// GET: /api/TiposDocumento/eb6e5252-f751-4e1e-a59f-278d13c67d2d
        //[Route("api/TiposDocumento/{guidProjeto}")]
        //public IEnumerable<TipoLVApp> GetTiposDocumentos(string guidProjeto)
        //{
        //    var config = new MapperConfiguration(
        //         cfg =>
        //         {
        //             cfg.CreateMap<TipoDocumento, TipoLVApp>();

        //         });
        //    var mapper = config.CreateMapper();

        //    var listaContexto = TipoLVApp.GetTiposDocumentos(guidProjeto);

        //    var listaTipos = mapper.Map<List<TipoLVApp>>(listaContexto);

        //    return listaTipos;
        //}

        // GET: /api/Disciplinas








    }
}
