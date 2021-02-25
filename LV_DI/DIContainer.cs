using AppListaVerificacao.Interface;
using LVModel;
using LV14FluentNHB;
using Unity;
using Unity.Lifetime;
using LV14FluentNHB.Service;
using LV14FluentNHB.Interface;
using VerificacaoListas.DTO;

namespace LV_DI
{
    public class DIContainer
    {
        private IUnityContainer _appContainer;


        private static DIContainer _instance = null;

        private DIContainer()
        {
           

            //Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperPerfil>());

         
            _appContainer = new UnityContainer();

           

            //DISCIPLINA
            _appContainer.RegisterType<AppServiceBase<Disciplina>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Disciplina>, ServiceBase<Disciplina>>();
            _appContainer.RegisterType<IRepository<Disciplina>, Repository<Disciplina>>();

            //LV_REVISAO_ESTADO
            //_appContainer.RegisterType<AppServiceBase<EstadoRevisao>>(new ContainerControlledLifetimeManager());
            //_appContainer.RegisterType<IServiceBase<EstadoRevisao>, ServiceBase<EstadoRevisao>>();
            //_appContainer.RegisterType<IRepository<EstadoRevisao>, Repository<EstadoRevisao>>();

            //LV_GRUPO_CODIGO_DOCUMENTO
            _appContainer.RegisterType<AppServiceBase<GrupoCodigoDocumento>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<GrupoCodigoDocumento>, ServiceBase<GrupoCodigoDocumento>>();
            _appContainer.RegisterType<IRepository<GrupoCodigoDocumento>, Repository<GrupoCodigoDocumento>>();


            //LV_CODIGO_DOCUMENTO
            _appContainer.RegisterType<AppServiceBase<CodigoDocumento>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<CodigoDocumento>, ServiceBase<CodigoDocumento>>();
            _appContainer.RegisterType<IRepository<CodigoDocumento>, Repository<CodigoDocumento>>();

            //NumeroDocSNCLavalin
            _appContainer.RegisterType<AppServiceBase<NumeroDocSNCLavalin>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<NumeroDocSNCLavalin>, ServiceBase<NumeroDocSNCLavalin>>();
            _appContainer.RegisterType<IRepository<NumeroDocSNCLavalin>, Repository<NumeroDocSNCLavalin>>();

            //ITEM_VERIFICACAO
            _appContainer.RegisterType<AppServiceBase<ItemRevisao>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<ItemRevisao>, ServiceBase<ItemRevisao>>();
            _appContainer.RegisterType<IRepository<ItemRevisao>, Repository<ItemRevisao>>();

            //CONFIGURACAO
            _appContainer.RegisterType<AppServiceBase<Configuracao>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Configuracao>, ServiceBase<Configuracao>>();
            _appContainer.RegisterType<IRepository<Configuracao>, Repository<Configuracao>>();

            //GRUPO
            _appContainer.RegisterType<AppServiceBase<Grupo>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Grupo>, ServiceBase<Grupo>>();
            _appContainer.RegisterType<IRepository<Grupo>, Repository<Grupo>>();

            //TIPO_LV
            _appContainer.RegisterType<AppServiceBase<ArquivoListas>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<ArquivoListas>, ServiceBase<ArquivoListas>>();
            _appContainer.RegisterType<IRepository<ArquivoListas>, Repository<ArquivoListas>>();

            

            //LV_USUARIO
            _appContainer.RegisterType<AppServiceBase<Usuario>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Usuario>, ServiceBase<Usuario>>();
            _appContainer.RegisterType<IRepository<Usuario>, Repository<Usuario>>();

            //LV_OS
            _appContainer.RegisterType<AppServiceBase<OS>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<OS>, ServiceBase<OS>>();
            _appContainer.RegisterType<IRepository<OS>, Repository<OS>>();

            //LV_AREA
            _appContainer.RegisterType<AppServiceBase<Area>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Area>, ServiceBase<Area>>();
            _appContainer.RegisterType<IRepository<Area>, Repository<Area>>();

            //LV_REVISAO
            _appContainer.RegisterType<AppServiceBase<Revisao>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Revisao>, ServiceBase<Revisao>>();
            _appContainer.RegisterType<IRepository<Revisao>, Repository<Revisao>>();

            //Confirmacao
            _appContainer.RegisterType<AppServiceBase<Confirmacao>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Confirmacao>, ServiceBase<Confirmacao>>();
            _appContainer.RegisterType<IRepository<Confirmacao>, Repository<Confirmacao>>();



            //LV_PROJETO 
            _appContainer.RegisterType<AppServiceBase<Projeto>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Projeto>, ServiceBase<Projeto>>();
            _appContainer.RegisterType<IRepository<Projeto>, Repository<Projeto>>();

            //PLANILHA 
            _appContainer.RegisterType<AppServiceBase<Planilha>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Planilha>, ServiceBase<Planilha>>();
            _appContainer.RegisterType<IRepository<Planilha>, Repository<Planilha>>();


            ////PLANILHA ESPECIAL
            //_appContainer.RegisterType<PlanilhaAppServiceBase>(new ContainerControlledLifetimeManager());
            //_appContainer.RegisterType<IServiceBase<Planilha>, PlanilhaServiceBase>();
            //_appContainer.RegisterType<IRepository<Planilha>, Repository<Planilha>>();

            //Documento ESPECIAL
            _appContainer.RegisterType<AppServiceBase<ListaVerificacao>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<ListaVerificacao>, ServiceBase<ListaVerificacao>>();
            _appContainer.RegisterType<IRepository<ListaVerificacao>, Repository<ListaVerificacao>>();

           


            ////LV_CONFIRMACAO_DUPLA ESPECIAL 
            //_appContainer.RegisterType<ConfirmacaoDuplaAppServiceBase>(new ContainerControlledLifetimeManager());
            //_appContainer.RegisterType<IServiceBase<ColunaRevisao>, ConfirmacaoDuplaServiceBase>();
            //_appContainer.RegisterType<IRepository<ColunaRevisao>, Repository<ColunaRevisao>>();

            ////LV_CONFIRMACAO
            _appContainer.RegisterType<AppServiceBase<Confirmacao>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<Confirmacao>, ServiceBase<Confirmacao>>();
            _appContainer.RegisterType<IRepository<Confirmacao>, Repository<Confirmacao>>();

            ////ConfiguracoesDTO
            //_appContainer.RegisterType<AppServiceBase<ConfiguracaoNavDTO>>(new ContainerControlledLifetimeManager());
            //_appContainer.RegisterType<IServiceBase<ConfiguracaoNavDTO>, ServiceBase<ConfiguracaoNavDTO>>();
            //_appContainer.RegisterType<IRepository<ConfiguracaoNavDTO>, Repository<ConfiguracaoNavDTO>>();

            ////ArquivoDTO
            //_appContainer.RegisterType<AppServiceBase<ArquivoNavDTO>>(new ContainerControlledLifetimeManager());
            //_appContainer.RegisterType<IServiceBase<ArquivoNavDTO>, ServiceBase<ArquivoNavDTO>>();
            //_appContainer.RegisterType<IRepository<ArquivoNavDTO>, Repository<ArquivoNavDTO>>();

            ////ConfiguracoesDTO
            //_appContainer.RegisterType<AppServiceBase<PlanilhaNavDTO>>(new ContainerControlledLifetimeManager());
            //_appContainer.RegisterType<IServiceBase<PlanilhaNavDTO>, ServiceBase<PlanilhaNavDTO>>();
            //_appContainer.RegisterType<IRepository<PlanilhaNavDTO>, Repository<PlanilhaNavDTO>>();

            //ProjetoToListDTO
            //_appContainer.RegisterType<AppServiceBase<ProjetoToListDTO>>(new ContainerControlledLifetimeManager());
            //_appContainer.RegisterType<IServiceBase<ProjetoToListDTO>, ServiceBase<ProjetoToListDTO>>();
            //_appContainer.RegisterType<IRepository<ProjetoToListDTO>, Repository<ProjetoToListDTO>>();

            //CabecalhoDTO
            _appContainer.RegisterType<AppServiceBase<CabecalhoDTO>>(new ContainerControlledLifetimeManager());
            _appContainer.RegisterType<IServiceBase<CabecalhoDTO>, ServiceBase<CabecalhoDTO>>();
            _appContainer.RegisterType<IRepository<CabecalhoDTO>, Repository<CabecalhoDTO>>();


        }

        public static DIContainer Instance
        {
            get
            {

                if (_instance == null)
                {
                    _instance = new DIContainer();
                }

                return _instance;
            }
        }

        public IUnityContainer AppContainer { get => _appContainer;  }
        
       
    }

}
