using EntidadesRepositoriosLeitura;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LV_PresenterAPI.Comandos
{
    public class CmdCriaColunaRevisao
    {
        private string _baseURL;

        public CmdCriaColunaRevisao(string baseURL)
        {
            _baseURL = baseURL;
        }

        public int Cria(ValoresColunasRev valor, int tentativa)
        {
            
            string api = "api/ApiAddRevisao";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(_baseURL);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.PostAsJsonAsync(api, valor);


                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var str = readTask.Result;

                    tentativa = 2;

                }
            }

            return tentativa;
        }
    }
}