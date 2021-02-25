using EntidadesRepositoriosLeitura;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LV_PresenterAPI.Comandos
{
    public class CmdMudarIndice:CmdGeral
    {

        //private string _baseURL;

        public CmdMudarIndice()
        {
            //_baseURL = baseURL;
        }

        public void Muda(ValoresMudaIndice valor)
        {
            string api = "api/ApiMudaIndice";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(_baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.PutAsJsonAsync(api, valor);


                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                }
            }
        }
    }
}