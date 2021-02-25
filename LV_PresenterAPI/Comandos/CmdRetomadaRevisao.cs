using EntidadesRepositoriosLeitura;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LV_PresenterAPI.Comandos
{
    public class CmdRetomadaRevisao:CmdGeral
    {


        //private string _baseURL;
        private static CmdRetomadaRevisao _instancia;
        private bool _retomado;

        private CmdRetomadaRevisao()
        {
            //_baseURL = baseURL;
        }

        public static CmdRetomadaRevisao Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new CmdRetomadaRevisao();
            }

            return _instancia;
        }

        public void Retomar(ValoresConfirma valor)//, bool houveSomenteAprimeira)
        {

            if (_retomado == false)
            {

                string api = "api/ApiConfirmacao/Retomar";
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

                        _retomado = true;
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var str = readTask.Result;

                    }

                }
            }
        }

        public void Reset()
        {
            _retomado = false;
        }






        //private string _baseURL;

        //public CmdRetomadaRevisao(string baseURL)
        //{
        //    _baseURL = baseURL;
        //}

        //public void Retomar(ValoresConfirma valor)
        //{
        //    string api = "api/ApiConfirmacao/Retomar";
        //    var hndlr = new HttpClientHandler();
        //    hndlr.UseDefaultCredentials = true;

        //    using (var client = new HttpClient(hndlr))
        //    {
        //        client.BaseAddress = new Uri(_baseURL);

        //        client.DefaultRequestHeaders.Clear();

        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var responseTask = client.PutAsJsonAsync(api, valor);

        //        responseTask.Wait();

        //        var result = responseTask.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsStringAsync();
        //            readTask.Wait();

        //            var str = readTask.Result;

        //        }
        //    }
        //}














        //private string _baseURL;
        //private static CmdRetomadaRevisao _unicaInstancia;
        //private HttpClient _client;
        //private string _api;

        //public CmdRetomadaRevisao(string baseURL)
        //{
        //    _baseURL = baseURL;

        //    var hndlr = new HttpClientHandler();
        //    hndlr.UseDefaultCredentials = true;
        //    _client = new HttpClient(hndlr);
        //    _client.BaseAddress = new Uri(_baseURL);
        //}

        //public static CmdRetomadaRevisao Instance(string baseURL)
        //{

        //    if (_unicaInstancia == null)

        //    {

        //        _unicaInstancia = new CmdRetomadaRevisao(baseURL);

        //    }

        //    return _unicaInstancia;

        //}

        //public async Task Retomar(SendConfirmarVM valor)
        //{


        //    //using (var client = new HttpClient(hndlr))
        //    //{
        //    _api = "api/ApiConfirmacao/Retomar";

        //    _client.DefaultRequestHeaders.Clear();

        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var responseTask = await _client.PutAsJsonAsync(_api, valor);

        //    //responseTask.Wait();

        //    //var result = responseTask.Result;

        //    //if (responseTask.IsSuccessStatusCode)
        //    //{
        //    //    var readTask = result.Content.ReadAsStringAsync();
        //    //    readTask.Wait();

        //    //    var str = readTask.Result;

        //    //}

        //    //}
        //}

        //public void ClienteDispose()
        //{
        //    _client.Dispose();
        //}

        //public static void Reset()
        //{
        //    if(_unicaInstancia != null)
        //    {
        //        _unicaInstancia.ClienteDispose();
        //        _unicaInstancia = null;
        //    }

        //}


    }
}