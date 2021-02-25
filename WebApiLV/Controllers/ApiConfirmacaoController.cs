using ConsumidorLV_Oracle.Comandos;
using EntidadesRepositoriosLeitura;
using RepositorioMongoDB;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiLV.Eventos;

namespace WebApiLV.Controllers
{
    public class ApiConfirmacaoController : ApiController
    {
        private bool _pode_confirmar;

        public ApiConfirmacaoController()
        {
            _pode_confirmar = true;
        }

        // GET: api/ApiConfirmacao
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiConfirmacao/5
        public string Get(int id)
        {
            return "value";
        }

        //GUID_LV = "70b973ec-c1cc-465c-9d8b-34beebb1f99e",
        //        IsConfiguarcaoDupla = false,
        //        GUID_USUARIO = "RRP",
        //        GUID_CONFIRMACAO = Guid.NewGuid().ToString(),
        //        ORDENADOR = 2



        // POST: api/ApiConfirmacao/Confirmar
        //[Route("api/ApiConfirmacao/Confirmar/{GUID_LV}/{IsConfiguarcaoDupla}/{GUID_USUARIO}/{GUID_CONFIRMACAO}/{ORDENADOR}")]


        //public void PostConfirmarRevisao(string GUID_LV, bool IsConfiguarcaoDupla, string GUID_USUARIO, string GUID_CONFIRMACAO, int ORDENADOR)//SendConfirmarVM valores)

        //[Route("api/ApiConfirmacao/{value}")]
        //[Route("api/ApiConfirmacao/Confirmar/{GUID_LV}/{IsConfiguarcaoDupla}/{GUID_USUARIO}/{GUID_CONFIRMACAO}/{ORDENADOR}")]

        [Route("api/ApiConfirmacao")]
        public IHttpActionResult PostConfirmarRevisao([FromBody]ValoresConfirma value)//string GUID_LV,string IsConfiguarcaoDupla, string GUID_USUARIO, string GUID_CONFIRMACAO, string ORDENADOR)
        {

            try
            {

                var cols = new LV_NoSQL().BuscarLV_ViewModel(value.GUID_LV).Colunas.OrderBy(x => x.ORDENADOR).Last();

                if (cols != null && cols.LV_Grupos.Last().Linhas.Last().EMITIDO != 1)
                {
                    //ComandoDispara<ValoresConfirma>.Dispara(new Envio<ValoresConfirma>(value, new int[] { 2 }));

                    CmdsOraConfirmacaoRevisao.Confirma(new Envio<ValoresConfirma>(value, new int[] { 2 }).MSG);
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));

            }
            catch (System.Exception)
            {
                return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Revisão não foi confirmada."));

            }


        }


        //// POST: api/ApiAddRevisao
        //public IHttpActionResult PostAddRevisao([FromBody]ValoresColunaRevisao valores)
        //{
        //    var conseguiu = CmdAcrescimoRevisao.Acrescenta(valores);

        //    if (conseguiu)
        //    {
        //        return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        //    }


        //    return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Revisão não foi inserida."));
        //}







        // PUT: /api/ApiRevisao/MudarEstado
        [Route("api/ApiConfirmacao/Retomar")]
        public void Put([FromBody]ValoresConfirma value)
        {
            var conseguiu = true;//CmdRetomarRevisao.Atualiza(value);

            //if (conseguiu)
            //{
            //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            //}


            //return ResponseMessage(Request.CreateResponse<string>(HttpStatusCode.NotFound, "Revisão não foi alterada."));
        }




        // DELETE: api/ApiConfirmacao/5
        public void Delete(int id)
        {
        }
    }
}
