using System.Collections.Generic;


namespace LVModel
{
    public class Grupo
    {
        private IList<ItemRevisao> _listaItens;
        private int _ordenador;
        private string _nome;
        //private string _guid_Planilha;
        private string _guid;
        private Planilha _planilha;
      

        public Grupo()
        {
            _listaItens = new List<ItemRevisao>();
        }

       

        public virtual int ORDENADOR { get => _ordenador; set => _ordenador = value; }
        public virtual string NOME { get => _nome; set => _nome = value; }
        //public virtual string GUID_PLANILHA { get => _guid_Planilha; set => _guid_Planilha = value; }
        public virtual string GUID { get => _guid; set => _guid = value; }
        public virtual IList<ItemRevisao> ListaItens { get => _listaItens; set => _listaItens = value; }
        public virtual Planilha Planilha { get => _planilha; set => _planilha = value; }
    }
}
