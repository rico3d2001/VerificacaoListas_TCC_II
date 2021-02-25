namespace EntidadesRepositoriosLeitura
{
    public class TipoLVVM
    {
        public virtual string CODIGO { get; set; }
        public virtual string GUID { get; set; }

        //public static List<TipoDocumento> GetTiposDocumentos(Projeto projeto)
        //{
        //    List<TipoDocumento> listaTipoDocumentos = new List<TipoDocumento>();

        //    //string numeroProjeto = projeto.NUMERO;

        //    //List<NumeroDocSNCLavalin> listaNumeroDocSNCLavalin = null;
        //    //using (var contextoLista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
        //    //{
        //    //    contextoLista.Start();
        //    //    listaNumeroDocSNCLavalin = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().GetByProperty("PROJETO", numeroProjeto).ToList();
        //    //}

        //    //List<string> listaStr = new List<string>();

        //    //var codAgrup = listaNumeroDocSNCLavalin.Distinct();

        //    //foreach (var item in codAgrup)
        //    //{
        //    //    var numero = item.NUMERO;
        //    //    var strarray = numero.ToString().Split('-');
        //    //    var str = strarray[3];
        //    //    str = str.Substring(2, 2);
        //    //    listaStr.Add(item.TIPO);
        //    //}

        //    //var agrupado = listaStr.Distinct().OrderBy(x => x).ToList();

        //    //for (int i = 0; i < agrupado.Count; i++)
        //    //{
        //    //    listaTipoDocumentos.Add(new TipoDocumento(agrupado[i], i.ToString()));
        //    //}



        //    return listaTipoDocumentos;
        //}


        //public static List<TipoDocumento> GetTiposDocumentos(string guid_Projeto)
        //{

        //    List<TipoDocumento> listaTipoDocumentos = new List<TipoDocumento>();
        //    Projeto projeto = null;
        //    using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<Projeto>>())
        //    {
        //        contextoObjeto.Start();
        //        projeto = contextoObjeto.ReturnByGUID(guid_Projeto);

        //        string numeroProjeto = projeto.NUMERO;

        //        List<NumeroDocSNCLavalin> listaNumeroDocSNCLavalin = null;
        //        using (var contextoLista = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
        //        {
        //            contextoLista.Start();
        //            listaNumeroDocSNCLavalin = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>().GetByProperty("PROJETO", numeroProjeto).ToList();
        //        }

        //        List<string> listaStr = new List<string>();

        //        var codAgrup = listaNumeroDocSNCLavalin.Distinct();

        //        foreach (var item in codAgrup)
        //        {
        //            var numero = item.NUMERO;
        //            var strarray = numero.ToString().Split('-');
        //            var str = strarray[3];
        //            str = str.Substring(2, 2);
        //            listaStr.Add(item.TIPO);
        //        }

        //        var agrupado = listaStr.Distinct().OrderBy(x => x).ToList();

        //        for (int i = 0; i < agrupado.Count; i++)
        //        {
        //            listaTipoDocumentos.Add(new TipoDocumento(agrupado[i], i.ToString()));
        //        }

        //    }

        //    return listaTipoDocumentos;
        //}
    }
}