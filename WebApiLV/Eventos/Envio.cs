using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLV.Eventos
{
    public class Envio<T>
    {

        private string _msg;
        private int[] _idColas;
       
        public string MSG { get => _msg; }

        public Envio(T obj, int[] idColas)
        {
            _idColas = idColas;
            _msg = JsonConversao.ConverteObjectParaJSon(obj);
        }
        public int[] IdColas { get => _idColas; }
        
        
    }
}