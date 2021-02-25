using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class LinhaRevisao
    {
        private string item;
        private string descricao;
        private string guidTipo;
        private string indiceRevisao;
        private string guidRevisao;
        private bool confirmado = false;
        private bool salvo = false;
        private bool emitido = false;

        public LinhaRevisao(string item, string descricao, string guidTipo, string indiceRevisao)
        {
            this.guidRevisao = "";
            this.item = item;
            this.descricao = descricao;
            this.guidTipo = guidTipo;
            this.indiceRevisao = indiceRevisao;
        }

        public string Item { get { return this.item; } }
        public string Descricao { get { return this.descricao; } }
        public string Status { get; set; }
        public string Guid { get; set; }
        public string GuidTipo { get => this.guidTipo; }
        public string IndiceRevisao { get => this.indiceRevisao; }
        public string GuidRevisao { get => this.guidRevisao; set => this.guidRevisao = value; }
        public bool Confirmado { get => confirmado; set => confirmado = value; }
        public bool Salvo { get => salvo; set => salvo = value; }
        public bool Emitido { get => emitido; set => emitido = value; }
    }
}