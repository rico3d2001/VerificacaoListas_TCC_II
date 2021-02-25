using EntidadesRepositoriosLeitura;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiLV.Controllers
{
    public class ApiRevisaoController : ApiController
    {
        // GET: api/ApiRevisao
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/ApiRevisao/fe2b2399-3bd1-493a-82f1-33ff45e64dce
        [Route("api/ApiRevisao/{guidRevisao}")]
        public RevUnitQuery Get(string guidRevisao)
        {
            return MySQLConsultaUnitariaRevisao.ObtemRevisao(guidRevisao);
        }

        //// POST: api/ApiRevisao
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: /api/ApiRevisao/MudarEstado
        [Route("api/ApiRevisao/MudarEstado")]
        public IHttpActionResult Put([FromBody]RevisaoVM value)
        {
            //var conseguiu = true; // CmdsAlterarRevisao.Atualiza(value);

            var lv =   new LV_NoSQL().MudaEstadoRevisao_ViewModel(value);

            if (lv != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }


            return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Revisão não foi alterada."));
        }

        




        // DELETE: api/ApiRevisao/5
        public void Delete(int id)
        {
        }
    }
}
