using EntidadesRepositoriosLeitura;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LV_PresenterAPI.Consultas
{
    public class QryBuscaNumeroDoc: QryConsulta
    {
       

        public QryBuscaNumeroDoc()
        {
           
        }

        public NumeroSNCLV VerificaNumeroNoBanco(string numeroDocSNC)
        {

            string api = "api/LV_NumeroSNC/" + numeroDocSNC.ToString();
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            NumeroSNCLV numero = null;

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

                    numero = JsonConvert.DeserializeObject<NumeroSNCLV>(str);

                }


            }

            return numero;

        }


    }
}