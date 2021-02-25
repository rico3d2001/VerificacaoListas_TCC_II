using ConsumidorLV_Oracle.Comandos;
using EntidadesRepositoriosLeitura;
using LVModel;
using RepositorioMongoDB;
using RepositorioMySQL.Consultas;

namespace LV_PresenterAPI.Comandos
{
    public class CmdSalvaListaVerificacao
    {

        //private string _baseURL;

        public CmdSalvaListaVerificacao()//string baseURL)
        {
            //_baseURL = baseURL;
        }

        public void SalvaLV(ValoresComandoCriaLV valor)
        {

            ListaVerificacao lv = CmdsListaVerficacao.CriaLV(valor);

            var listaVerficacaoVM = MySQLConsultaListaVerificacao.ObtemListaSemRevisoes(valor.NovoGuidLV);

            var confirma = new LV_NoSQL().CriarLV_ViewModel(listaVerficacaoVM);




            //string api = "api/CriaLV";
            //var hndlr = new HttpClientHandler();
            //hndlr.UseDefaultCredentials = true;

            //using (var client = new HttpClient(hndlr))
            //{
            //    client.BaseAddress = new Uri(_baseURL);

            //    client.DefaultRequestHeaders.Clear();

            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    var responseTask = client.PostAsJsonAsync(api, valor);


            //    responseTask.Wait();

            //    var result = responseTask.Result;

            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsStringAsync();
            //        readTask.Wait();

            //        var str = readTask.Result;

            //    }






            //}

        }
    }
}