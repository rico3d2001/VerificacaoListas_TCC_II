using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaRegistroVerificacoes
    {
        private List<RegistroVerificacao> listaRevisoes;

        public ListaRegistroVerificacoes()
        {
            var item = new RegistroVerificacao(Guid.NewGuid().ToString(), "0", 0,"")
            {
                Verificador = "RONAR"
            };

            item.SetDateTime(DateTime.Now);

            this.listaRevisoes = new List<RegistroVerificacao>()
            {
                item
            };
        }

        public List<RegistroVerificacao> ListaRevisoes { get => this.listaRevisoes; set => this.listaRevisoes = value; }
    }
}