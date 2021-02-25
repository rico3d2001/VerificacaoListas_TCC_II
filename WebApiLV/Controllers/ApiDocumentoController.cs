
using EntidadesRepositoriosLeitura;
using LVModel;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;
using System.Collections.Generic;
using System.Web.Http;
using WebApiLV.Consultas;

namespace WebApiLV.Controllers
{
    public class ApiDocumentoController : ApiController
    {

        // GET: /api/NumeroSNCLavalin/9999-999-9999-46XX-00003";
        [Route("api/NumeroSNCLavalin/{numeroSNC}")]
        public NumeroDocSNCLavalin GetNumeroSNCLavalin(string numeroSNC)
        {
            return QryNumeroDocumento.GetNumeroDocumento(numeroSNC);
        }


        
        // GET: /api/LV_NumeroSNC/9999-999-9999-46XX-00001
        [Route("api/LV_NumeroSNC/{numeroDocSNC}")]
        public NumeroSNCLV GetLV_NumeroSNC(string numeroDocSNC)
        {
            return MySQLConsultaNumeroSNCLavalin.ObtemNumeroSNCLvalin(numeroDocSNC);
            //return QryLV.ObtemLVporNumeroSNCLavalin(numeroDocSNC);
        }


        // string api = /api/StatusRevisoesLV/5efa3953-7238-4b2d-92a4-981bf7973fcc
        //              /api/StatusRevisoesLV/7e5e3cf4-dd1f-4fda-865e-9a7d3d884651
        [Route("api/StatusRevisoesLV/{guidDocumento}")]
        public StatusRevisoesLV GetStatusRevisoesLV(string guidDocumento)
        {
            return MySQLConsultaListaVerificacao.StatusRevisoesLV(guidDocumento);
            //QryLV.ObtemLVporNumeroSNCLavalin(numeroDocSNC);
        }


        // string api = /api/StatusLV/5efa3953-7238-4b2d-92a4-981bf7973fcc
        //              /api/StatusLV/7e5e3cf4-dd1f-4fda-865e-9a7d3d884651
        [Route("api/StatusLV/{guidDocumento}")]
        public StatusLV GetStatusLV(string guidDocumento)
        {
            return MySQLConsultaListaVerificacao.StatusLV(guidDocumento);
            //QryLV.ObtemLVporNumeroSNCLavalin(numeroDocSNC);
        }


        // string api = /api/StatusConfirmacoesLV/5efa3953-7238-4b2d-92a4-981bf7973fcc
        //              /api/StatusConfirmacoesLV/7e5e3cf4-dd1f-4fda-865e-9a7d3d884651
        [Route("api/StatusConfirmacoesLV/{guidDocumento}")]
        public StatusConfirmacoesLV GetStatusConfirmacoesLV(string guidDocumento)
        {
            return MySQLConsultaListaVerificacao.StatusConfirmacoesLV(guidDocumento);
            //QryLV.ObtemLVporNumeroSNCLavalin(numeroDocSNC);
        }



        // string api = /api/LVCompleta/5efa3953-7238-4b2d-92a4-981bf7973fcc
        //   /api/LVCompleta/f843d182-5a6c-47ee-8c39-9d0546647118
        [Route("api/LVCompleta/{guidDocumento}")]
        public ListaVerficacaoVM GetListaCompleta(string guidDocumento)
        {
            var noSQL = new LV_NoSQL();
                
             var lv = noSQL.BuscarLV_ViewModel(guidDocumento);

            if(lv == null)
            {
               lv = MySQLConsultaListaVerificacao.ObtemListaCompleta(guidDocumento);

                noSQL.CriarLV_ViewModel(lv);
            }

            //return ConsultaListaVerificacao.ObtemListaCompleta(guidDocumento);



            //QryLV.ObtemLVporNumeroSNCLavalin(numeroDocSNC);

            return lv;


        }

        //              /api/LVInicial/420c7f7a-d0ab-4417-9955-bf92f4252eb7
        [Route("api/LVInicial/{guidDocumento}")]
        public ListaVerficacaoVM GetListaSemRevisoes(string guidDocumento)
        {

            return new LV_NoSQL().BuscarLV_ViewModel(guidDocumento);
            //return ConsultaListaVerificacao.ObtemListaSemRevisoes(guidDocumento);




            //QryLV.ObtemLVporNumeroSNCLavalin(numeroDocSNC);
        }


        //              /api/LVSemConfirmacoes/7e5e3cf4-dd1f-4fda-865e-9a7d3d884651
        [Route("api/LVSemConfirmacoes/{guidDocumento}")]
        public ListaVerficacaoVM GetListaSemConfirmacoes(string guidDocumento)
        {
            return MySQLConsultaListaVerificacao.ObtemListaSemConfirmacoes(guidDocumento);
            //QryLV.ObtemLVporNumeroSNCLavalin(numeroDocSNC);
        }

        //"api/LVPrimeiraConfirmacao/fc16ae5a-5fa9-41c5-9655-659f076bed7f"
        [Route("api/LVPrimeiraConfirmacao/{guidDocumento}")]
        public ListaVerficacaoVM GetLVPrimeiraConfirmacao(string guidDocumento)
        {
            return MySQLConsultaListaVerificacao.ObtemListaPrimeiraConfirmacao(guidDocumento);
            //QryLV.ObtemLVporNumeroSNCLavalin(numeroDocSNC);
        }



        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiDocumento/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiDocumento
        public void Post(string value)
        {
        }

        // PUT: api/ApiDocumento/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiDocumento/5
        public void Delete(int id)
        {
        }
    }
}
