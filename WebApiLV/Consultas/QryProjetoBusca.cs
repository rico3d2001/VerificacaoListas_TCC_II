namespace WebApiLV.Consultas
{
    public class QryProjetoBusca
    {
        //public static ProjetoVM GetProjetoApp(string guidProjeto)
        //{

        //    var config = new MapperConfiguration(
        //          cfg =>
        //          {
        //              cfg.CreateMap<Projeto, ProjetoVM>();
        //              cfg.CreateMap<Area, AreaVM>();
        //              cfg.CreateMap<OS, OSVM>();
        //              cfg.CreateMap<TipoDocumento, TipoLVVM>();
        //          });
        //    var mapper = config.CreateMapper();

        //    ProjetoVM projetoApp = null;

        //    using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
        //    {
        //        contextoObjeto.Start();
        //        var projeto = contextoObjeto.ReturnByGUID(guidProjeto);

        //            projetoApp = mapper.Map<ProjetoVM>(projeto);

        //        var areas = projeto.ListaAreas.Distinct().ToList();
        //       var oss =  projeto.ListaOSs.Distinct().ToList();

        //        projetoApp.Areas = mapper.Map<List<AreaVM>>(areas);

        //        projetoApp.OSs = mapper.Map<List<OSVM>>(oss);

        //        var listaContexto = TipoLVVM.GetTiposDocumentos(projeto);

        //        projetoApp.Tipos = mapper.Map<List<TipoLVVM>>(listaContexto);

        //    }

        //    return projetoApp;
        //}

    }
}