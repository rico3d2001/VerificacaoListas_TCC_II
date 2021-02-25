using EntidadesRepositoriosLeitura;
using RepositorioMongoDB;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiLV.Eventos;
using WebApiLV.NosSQL;

namespace WebApiLV.Controllers
{
    public class ApiAddRevisaoController : ApiController
    {
        // GET: api/ApiAddRevisao
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiAddRevisao/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiAddRevisao
        public IHttpActionResult PostAddRevisao([FromBody]ValoresColunasRev valores)
        {

            //new LV_NoSQL().CriarLV_ViewModel(valores.NovoGuidLV);


            //var envio = new Envio<ValoresColunasRev>(valores,new int[] {1, 2});
            //ComandoDispara<ValoresColunasRev>.Dispara(envio);




            //var conseguiu = RCmdAcrescimoRevisaoRV.Acrescenta(valores);


            var lv = new LV_NoSQL().AcrescentarRevisoes_ViewModel(valores);

            //var conseguiu = true;

            if (lv != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }


            return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Revisão não foi inserida."));
        }

        // PUT: api/ApiAddRevisao/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiAddRevisao/5
        public void Delete(int id)
        {
        }
    }
}
