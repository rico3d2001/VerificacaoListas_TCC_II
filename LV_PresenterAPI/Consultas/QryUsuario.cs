using LVModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace LV_PresenterAPI.Consultas
{
    public class QryUsuario : QryConsulta
    {
       

        public QryUsuario():base()
        {
            
        }

        public Usuario ObtemUsuario(string login)
        {
            Usuario usuario = null;

            string api = "api/Usuario/" + login;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(_baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.GetAsync(api);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                    usuario = JsonConvert.DeserializeObject<Usuario>(str);

                }
            }

            return usuario;
        }
    }
}