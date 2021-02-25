using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using VerificacaoListas.DTO;

namespace LV_PresenterAPI.Consultas
{
    public class QryCabecalho : QryConsulta
    {
        ///api/Cabecalho/95864c2a-5a7d-49c4-8fa4-0b22abe72fb9
        ///
      
        public QryCabecalho()
        {
          
        }

        public CabecalhoDTO ObtemCabecalho(string guidPlanilha)
        {
            string api = "api/Cabecalho/" + guidPlanilha;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            CabecalhoDTO cabecalho = null;

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

                    cabecalho = JsonConvert.DeserializeObject<CabecalhoDTO>(str);

                }


            }

            return cabecalho;
        }

    }
}