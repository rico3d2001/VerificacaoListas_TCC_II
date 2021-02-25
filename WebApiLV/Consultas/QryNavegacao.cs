using EntidadesRepositoriosLeitura;
using System.Collections.Generic;

namespace WebApiLV.Consultas
{
    public class QryNavegacao
    {
        public static List<ConfiguracaoNavDTO> ListaCFGs()
        {


            List<ConfiguracaoNavDTO> listaCFGs = new List<ConfiguracaoNavDTO>();



            //using (var contexto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ConfiguracaoNavDTO>>())
            //{
            //    contexto.Start();

            //    listaCFGs = contexto.Query().OrderBy(x => x.NOME).ToList();


            //}

            return listaCFGs;
        }

        public static List<PlanilhaNavDTO> ListaPlanilhas(string guidArquivo)
        {


            List<PlanilhaNavDTO> listaResult = new List<PlanilhaNavDTO>();



            //using (var contexto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<PlanilhaNavDTO>>())
            //{
            //    contexto.Start();

            //    listaResult = contexto.GetByProperty("GUID_TIPO", guidArquivo).ToList();


            //}

            return listaResult;
        }

        public static List<ArquivoNavDTO> ListaArquivos(string guidConfig)
        {


            List<ArquivoNavDTO> listaResult = new List<ArquivoNavDTO>();



            //using (var contexto = DIContainer.Instance.AppContainer.Resolve<AppServiceBase<ArquivoNavDTO>>())
            //{
            //    contexto.Start();

            //    listaResult = contexto.GetByProperty("GUID_CONFIG", guidConfig).ToList();


            //}

            return listaResult;
        }

    }
}