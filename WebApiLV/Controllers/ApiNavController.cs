using EntidadesRepositoriosLeitura;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiLV.Controllers
{
    public class ApiNavController : ApiController
    {
        //Configuracao
        // GET: /api/ConfiguracoesNav
        [Route("api/ConfiguracoesNav")]
        public IEnumerable<ConfiguracaoNavDTO> GetConfiguracoesNav()
        {
            //return QryNavegacao.ListaCFGs();

            return MySQLConsultaConfiguracoesNAV.ObtemListaCFGs();

            //return new LV_NoSQL().PegaConfiguracaoNavDTO();

            //return new ConfiguracoesListaMDB().Buscar();
        }

        // GET: /api/ArquivosNav/2dc1722f-cd73-47f4-b608-df357c6c4009
        [Route("api/ArquivosNav/{guidConfig}")]
        public IEnumerable<ArquivoNavDTO> GetArquivosNav(string guidConfig)
        {
            return MySQLConsultaArquivosNAV.ObtemArquivosNAV(guidConfig);

            //return QryNavegacao.ListaArquivos(guidConfig);

            //return new LV_NoSQL().PegaArquivoNavDTO(guidConfig);
        }

        // GET: /api/PlanilhasNav/00d58237-29e1-4189-a8bf-4c9511fc096a
        [Route("api/PlanilhasNav/{guidTipoLV}")]
        public IEnumerable<PlanilhaNavDTO> GetPlanilhasNav(string guidTipoLV)
        {
            return MySQLConsultaPlanilhasNAV.ObtemPlanilhasNAV(guidTipoLV);

            //return QryNavegacao.ListaPlanilhas(guidTipoLV);

            //return new LV_NoSQL().PegaPlanilhaNavDTO(guidTipoLV);


        }
    }
}
