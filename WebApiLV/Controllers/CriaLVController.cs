using ConsumidorLV_Oracle.Comandos;
using EntidadesRepositoriosLeitura;
using LVModel;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiLV.Controllers
{
    public class CriaLVController : ApiController
    {
        // GET: api/CriaLV
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CriaLV/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CriaLV
        public IHttpActionResult Post([FromBody]ValoresComandoCriaLV valores)
        {
            ListaVerificacao lv = CmdsListaVerficacao.CriaLV(valores);

            var listaVerficacaoVM = MySQLConsultaListaVerificacao.ObtemListaSemRevisoes(valores.NovoGuidLV);

            var confirma = new LV_NoSQL().CriarLV_ViewModel(listaVerficacaoVM);

            if (confirma)
            {
               
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }


            return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Lista não foi inserida."));
        }


        






        // PUT: api/CriaLV/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CriaLV/5
        public void Delete(int id)
        {
        }
    }
}
