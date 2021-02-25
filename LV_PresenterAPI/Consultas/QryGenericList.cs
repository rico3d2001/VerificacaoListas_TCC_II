using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LV_PresenterAPI.Consultas
{
    public class QryGenericList<T>
    {

        public List<T> GetLista(string api, string _baseUrl)
        {
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;
            List<T> lista = new List<T>();

            using (var client = new HttpClient(hndlr))
            {
                client.BaseAddress = new Uri(_baseUrl);

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

                    lista = JsonConvert.DeserializeObject<T[]>(str).ToList();

                }


            }

            return lista;
        }



    }
}