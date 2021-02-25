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
    public class QryPlanilha : QryConsulta
    {
      
        public QryPlanilha(string baseURL)
        {
           
        }

        public PlanilhaTemplateDTO ObtemPlanilhaApresentacao(string guidPlanilha)
        {
            ///api/Template/guidPlanilha
            string api = "api/Template/" + guidPlanilha;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            PlanilhaTemplateDTO planilha = null;

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

                    planilha = JsonConvert.DeserializeObject<PlanilhaTemplateDTO>(str);

                }


            }

            return planilha;
        }
    }
}