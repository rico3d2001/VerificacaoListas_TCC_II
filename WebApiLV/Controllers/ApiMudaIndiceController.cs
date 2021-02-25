using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiLV.Controllers
{
    public class ApiMudaIndiceController : ApiController
    {
        // GET: api/ApiMudaIndice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiMudaIndice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiMudaIndice
        public void Post([FromBody]string value)
        {
        }

        //// PUT: api/ApiMudaIndice/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}


        //CmdMudaIndice
        // PUT: /api/ApiRevisao/MudarEstado
        [Route("api/ApiMudaIndice")]
        public void Put([FromBody]ValoresMudaIndice value)
        {
            var conseguiu = true; // CmdMudaIndice.Atualiza(value);

            //if (conseguiu)
            //{
            //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            //}


            //return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Revisão não foi alterada."));
        }

        // DELETE: api/ApiMudaIndice/5
        public void Delete(int id)
        {
        }
    }
}
