using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LV_PresenterAPI.Models
{
    public class GrupoRevisoes
    {
        private List<LinhaRevisao> listaLinhas;

        private string nome;
        private string indice;

        public GrupoRevisoes(string indice, string nome, List<LinhaRevisao> listaLinhas)
        {
            this.indice = indice;
            this.nome = nome;
            this.listaLinhas = listaLinhas;
        }

        public List<LinhaRevisao> ListaLinhas { get { return this.listaLinhas; } }

        public string Nome { get { return this.nome; } }
        public string Indice { get { return this.indice; } }
    }
}