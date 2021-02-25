using LVModel;

namespace WebApiLV.Consultas
{
    public class QryNumeroDocumento
    {

        public static NumeroDocSNCLavalin GetNumeroDocumento(string numeroSNCLavalin)
        {

            NumeroDocSNCLavalin numeroDocSNCLavalin = null;

            //using (var contextoObjeto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<NumeroDocSNCLavalin>>())
            //{
            //    contextoObjeto.Start();
            //    var qry = contextoObjeto.GetByProperty("NUMERO", numeroSNCLavalin).FirstOrDefault();
            //    //if(qry == null)
            //    //{
            //    //    numeroDocSNCLavalin = new NumeroDocSNCLavalin() { GUID = "X" };
            //    //}
            //    //else
            //    //{
            //        numeroDocSNCLavalin = qry;
            //    //}
            //}

            return numeroDocSNCLavalin;
        }
    }
}