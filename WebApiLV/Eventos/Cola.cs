using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLV.Eventos
{
    public class Cola
    {
        public int ColaId { get; set; }
        public string Nome { get; set; }

        public static IEnumerable<Cola> Colas = new List<Cola>
        {
            new Cola {ColaId = 1,Nome = "Cola1"},
            new Cola {ColaId = 2,Nome = "Cola2"}
        };
    }
}