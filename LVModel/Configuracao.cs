using System.Collections.Generic;

namespace LVModel
{
    public class Configuracao
    {
        
        private string _guid;
        private string _nome;
        //private int _id_Disciplina;
        private Disciplina _disciplina;
        private IList<ArquivoListas> _listaArquivos;
     

        public Configuracao(string guid)
        {
            _guid = guid;
            _listaArquivos = new List<ArquivoListas>();

        }

        public Configuracao()
        {
            _listaArquivos = new List<ArquivoListas>();
        }

        public virtual string GUID { get => _guid; set => _guid = value; }
        public virtual string NOME { get => _nome; set => _nome = value; }

        //public virtual int ID_DISCIPLINA { get => _id_Disciplina; set => _id_Disciplina = value; }
        public virtual Disciplina Disciplina { get => _disciplina; set => _disciplina = value; }
        public virtual IList<ArquivoListas> ListaArquivos { get => _listaArquivos; set => _listaArquivos = value; }
    }
}
