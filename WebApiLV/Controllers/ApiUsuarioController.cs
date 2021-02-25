using LVModel;
using RepositorioMySQL.Consultas;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiLV.Controllers
{
    public class ApiUsuarioController : ApiController
    {
        // GET: api/ApiUsuario
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //   string api = /api/Usuario/RONAR
        // GET: api/ApiUsuario/5
        [Route("api/Usuario/{login}")]
        public Usuario GetUsuario(string login)
        {
            return MySQLConsultaUsuario.ObtemUsuarioPorLogin(login);
            // return ConsultaNumeroSNCLavalin.ObtemNumeroSNCLvalin(numeroDocSNC);
        }

        // POST: api/ApiUsuario
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiUsuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiUsuario/5
        public void Delete(int id)
        {
        }
    }
}
