using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class ColunaRevisaoViewModel
    {
        private List<GrupoRevisoes> listaGrupos;
        private string indiceRevisao;
        private string dataRevisaoExistente;
        private string nomeVerificador;
        private string loginVericador;
        private int ordenador;
        private string idVerificaor;

        public ColunaRevisaoViewModel(string indiceRevisao, string dataRevisaoExistente, string nomeVerificador, string loginVerificaro, int ordenador, string idSiglaVerificador)
        {
            this.ordenador = ordenador;
            this.loginVericador = loginVerificaro;
            this.nomeVerificador = nomeVerificador;
            this.dataRevisaoExistente = dataRevisaoExistente;
            this.indiceRevisao = indiceRevisao;
            this.listaGrupos = new List<GrupoRevisoes>();
            this.idVerificaor = idSiglaVerificador;
        }
        
        public List<GrupoRevisoes> ListaGrupos { get { return this.listaGrupos; } }

        public void AddGrupo(GrupoRevisoes grupo)
        {
            listaGrupos.Add(grupo);
        }

        public string IndiceRevisao { get => this.indiceRevisao; }
        public string DataRevisaoExistente { get => dataRevisaoExistente; set => dataRevisaoExistente = value; }
        public string NomeVerificador { get => nomeVerificador; set => nomeVerificador = value; }
        public string SiglaVerificador { get => loginVericador; set => loginVericador = value; }
        public int Ordenador { get => ordenador; }
        public string IdVerificaor { get => idVerificaor; }
    }
}