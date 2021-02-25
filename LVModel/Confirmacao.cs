
using System;
using System.Collections.Generic;

namespace LVModel
{
    public class Confirmacao
    {
        protected string _guidConfirmacao;
        protected string _guidUsuario1;
        protected string _guidUsuario2;
        protected string _guidDocumento;
        protected string _indiceRevisao;
        protected DateTime _data;
        protected int _ordenador;
      



        public virtual string INDICE_REV { get => _indiceRevisao; set => _indiceRevisao = value; }
        public virtual string GUID { get => _guidConfirmacao; set => _guidConfirmacao = value; }
        public virtual DateTime DATA { get => _data; set => _data = value; }
        public virtual int ORDENADOR { get => _ordenador; set => _ordenador = value; }
        public virtual string GUID_USUARIO1 { get => _guidUsuario1; set => _guidUsuario1 = value; }
        public virtual string GUID_USUARIO2 { get => _guidUsuario2; set => _guidUsuario2 = value; }
        public virtual string GUID_DOCUMENTO { get => _guidDocumento; set => _guidDocumento = value; }


        public Confirmacao()
        {

        }


        







    }
}
