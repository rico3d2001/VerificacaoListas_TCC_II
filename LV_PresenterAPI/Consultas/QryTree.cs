using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Net.Http;
using VerificacaoListas.DTO;


namespace LV_PresenterAPI.Consultas
{
    public class QryTree
    {
        public static List<ConfiguracaoNavDTO> GetConfiguracoes(string baseURL)
        {

            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;
            string api = "api/ConfiguracoesNav";

            QryGenericList<ConfiguracaoNavDTO> qryGenericList = new QryGenericList<ConfiguracaoNavDTO>();

            List<ConfiguracaoNavDTO> listaConfiguracaoNav = qryGenericList.GetLista(api, baseURL);

            return listaConfiguracaoNav;

        }

        public static List<ArquivoNavDTO> GetArquivosNav(string guidConfiguracao, string baseURL)
        {
            string api = "api/ArquivosNav/" + guidConfiguracao;

            QryGenericList<ArquivoNavDTO> qryGenericList = new QryGenericList<ArquivoNavDTO>();

            List<ArquivoNavDTO> listaArquivosNav = qryGenericList.GetLista(api, baseURL);

            return listaArquivosNav;

        }



        public static List<PlanilhaNavDTO> GetPlanilhasNav(string guidArquivo, string baseURL)
        {

            string api = "api/PlanilhasNav/" + guidArquivo;

            QryGenericList<PlanilhaNavDTO> qryGenericList = new QryGenericList<PlanilhaNavDTO>();

            List<PlanilhaNavDTO> listaPlanilhasNav = qryGenericList.GetLista(api, baseURL);

            return listaPlanilhasNav;

        }


    }
}