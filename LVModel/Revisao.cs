using LVModel.ObjetosValor;
using System;


namespace LVModel
{
    public class Revisao
    {

        //private ItemRevisao _itemRevisao;
        //private EstadoRevisao _estadoRevisao;
        //private ColunaRevisao _confirmacao;
        //private Documento _documento;
        //private Usuario _usuario;

        public Revisao() { }


        public virtual string GUID { get; set; }
        public virtual string GUID_LV_ITEM { get; set; }
        public virtual string GUID_LV_VERIFICADOR { get; set; }
        public virtual DateTime DATA_VERICACAO { get; set; }
        public virtual int ID_ESTADO { get; set; }

        public virtual string GUID_DOC_VERIFICACAO { get; set; }
        public virtual string INDICE { get; set; }
        public virtual int ORDENADOR { get; set; }
        public virtual int CONFIRMADO { get; set; }
        public virtual int SALVO { get; set; }
        public virtual int EMITIDO { get; set; }
        public virtual string GUID_CONFIRMADO { get; set; }

        


        //public virtual ItemRevisao ItemRevisao { get => _itemRevisao; set => _itemRevisao = value; }
        //public virtual EstadoRevisao EstadoRevisao { get => _estadoRevisao; set => _estadoRevisao = value; }
        //public virtual ColunaRevisao Confirmacao { get => _confirmacao; set => _confirmacao = value; }
        //public virtual Documento Documento { get => _documento; set => _documento = value; }
        //public Usuario Usuario { get => _usuario; set => _usuario = value; }
    }
}
