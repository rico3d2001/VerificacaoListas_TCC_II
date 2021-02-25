using System.Collections.Generic;
using System.Linq;

namespace WebAppAWListaVerificacao.Models
{
    public class ListaRegistrosPorColunas
    {
        string indiceRevisao;
        int ordenador;
        #pragma warning disable CS0169 
        string dataRevisao;
        #pragma warning restore CS0169 

        List<RegistroVerificacao> listaRegistros;

        public ListaRegistrosPorColunas(string indiceRevisao, int ordenador)
        {
            this.ordenador = ordenador;
            this.indiceRevisao = indiceRevisao;
            this.listaRegistros = new List<RegistroVerificacao>();
        }

        public string IndiceRevisao { get => this.indiceRevisao; }
        public int Ordenador { get => this.ordenador; }

        public string DataRevisaoExistente
        {
            get
            {
                if (listaRegistros.Count > 0)
                {
                    return this.listaRegistros.First().GetDataApresenta();
                }

                return "00/00/00";
            }
        }

        public string SiglaVerificadorRevisaoExistente
        {
            get
            {
                if (listaRegistros.Count > 0)
                {
                    return this.listaRegistros.First().GuidVerificador;
                }

                return "XXX";
            }
        }

        public string IdVerificador
        {
            get
            {
                if (listaRegistros.Count > 0)
                {
                    return this.listaRegistros.First().GuidVerificador;
                }

                return "XXX";
            }
        }

        public string NomeVerificadorRevisaoExistente
        {
            get
            {
                if (listaRegistros.Count > 0)
                {
                    return this.listaRegistros.First().NomeVerificador;//NomeVerificadorPelaGuid();
                }

                return "XXX";
            }
        }

        public void AddRegistro(RegistroVerificacao registro)
        {
            listaRegistros.Add(registro);
        }

        public List<RegistroVerificacao> ListaRegistros { get => listaRegistros; }
    }
}