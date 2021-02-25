namespace LVModel
{

    public class ItemRevisao
    {
        private int _ordenador;
        private string _descricao;
        private string _guid;
        //private string _guid_Grupo;
        private Grupo _grupo;




        public ItemRevisao(int ordenador, string descricao, string guid)
        {
           
            _guid = guid;
            _ordenador = ordenador;
            _descricao = descricao;
            
        }

        

        public ItemRevisao() { }

        public virtual int ORDENADOR { get => _ordenador; set => _ordenador = value; }
        public virtual string DESCRICAO { get => _descricao; set => _descricao = value; }
        public virtual string GUID { get => _guid; set => _guid = value; }
        //public virtual string GUID_GRUPO { get => _guid_Grupo; set => _guid_Grupo = value; }
        public virtual Grupo Grupo { get => _grupo; set => _grupo = value; }
    }
}