using EntidadesRepositoriosLeitura;
using RepositorioMySQL.Consultas;
using System.Collections.Generic;
using System.Web.Http;
using VerificacaoListas.DTO;
using WebApiLV.Consultas;

namespace WebApiLV.Controllers
{
    public class PlanilhaDTOController : ApiController
    {
        // GET: api/PlanilhaDTO
        public IEnumerable<GruposDTO> Get()
        {
            return new List<GruposDTO>();
        }

        // GET: /api/Cabecalho/95864c2a-5a7d-49c4-8fa4-0b22abe72fb9
        [Route("api/Cabecalho/{guidPlanilha}")]
        public CabecalhoDTO GetCabecalho(string guidPlanilha)
        {
            return QryCabecalhoApp.ObtemCabecalho(guidPlanilha);
        }

        // GET: /api/Template/95864c2a-5a7d-49c4-8fa4-0b22abe72fb9
        [Route("api/Template/{guidPlanilha}")]
        public PlanilhaLVVM GetPlanilhaVazia(string guidPlanilha)
        {
            return MySQLConsultaPlanilha.ObtemPlanilha(guidPlanilha);
        }

        // POST: api/PlanilhaDTO
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PlanilhaDTO/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PlanilhaDTO/5
        public void Delete(int id)
        {
        }
    }
}
