using EntidadesRepositoriosLeitura;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LV_PresenterAPI.Consultas
{
    public class QryProjetos : QryConsulta
    {
       
       
        private List<ProjetoToListDTO> _listaProjetos;
        List<DisciplinaVM> _disciplinas;

        private ProjetoVM _projetoSelecionado;

        public QryProjetos()
        {

            setProjetos();
            setDisciplinas();

        }

        

        private void setProjetos()
        {
            string api = "api/Projetos";
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            List<ProjetoToListDTO> listaProjetoToList = new List<ProjetoToListDTO>();

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

                    _listaProjetos = JsonConvert.DeserializeObject<ProjetoToListDTO[]>(str).ToList();

                }


            }

            
        }

        public ProjetoVM GetProjetoApp(string id)
        {

            string api = "api/Projeto/" + id;
            var hndlr = new HttpClientHandler();
            hndlr.UseDefaultCredentials = true;

            ProjetoVM projetoApp = null;

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

                    projetoApp = JsonConvert.DeserializeObject<ProjetoVM>(str);

                }


            }

            return projetoApp;
        }

        public List<OSVM> GetOSs(string id)
        {
            if(_projetoSelecionado != null)
            {
                _projetoSelecionado = GetProjetoApp(id);
                return _projetoSelecionado.OSs;
            }
            else
            {
                return _projetoSelecionado.OSs;
            }

        }

        public List<AreaVM> GetAreas(string id)
        {
        

            if (_projetoSelecionado != null)
            {
                _projetoSelecionado = GetProjetoApp(id);
                return _projetoSelecionado.Areas;
            }
            else
            {
                return _projetoSelecionado.Areas;
            }

        }

        public List<TipoLVVM> GetTipos(string id)
        {


            if (_projetoSelecionado != null)
            {
                _projetoSelecionado = GetProjetoApp(id);
                return _projetoSelecionado.Tipos;
            }
            else
            {
                return _projetoSelecionado.Tipos;
            }

        }

        public List<DisciplinaVM> GetDisciplinas()
        {
            return _disciplinas;
        }

        public List<ProjetoToListDTO> GetProjetoToLists()
        {
            return _listaProjetos.OrderBy(x => x.NUMERO).ToList();
        }

        private void setDisciplinas()
        {

            string api = "api/Disciplinas";
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

                    _disciplinas = JsonConvert.DeserializeObject<DisciplinaVM[]>(str).ToList();

                }


            }

            
        }


    }
}