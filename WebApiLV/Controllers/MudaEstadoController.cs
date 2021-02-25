using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiLV.Controllers
{
    public class MudaEstadoController : ApiController
    {
        // GET: api/MudaEstado
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MudaEstado/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MudaEstado

        public void Post([FromBody]string value)
        {
        }


        // PUT: /api/ApiRevisao/MudarEstado
        
        public void Put([FromBody]ValoresMudaIndice value)
        {
            var conseguiu = true; // CmdMudaIndice.Atualiza(value);

            //if (conseguiu)
            //{
            //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            //}


            //return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Revisão não foi alterada."));
        }



        // PUT: api/MudaEstado/5
        [Route("api/MudaEstado")]
        public void Put([FromBody]RevisaoUnitaria value)
        {
            var conseguiu = true; // CmdRevisaoUnitaria.MudaEstado(value);
        }

        // DELETE: api/MudaEstado/5
        public void Delete(int id)
        {
        }
    }
}
