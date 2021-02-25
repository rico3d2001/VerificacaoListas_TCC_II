using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntidadesRepositoriosLeitura
{
    public class Resultado
    {
        private string _comando;
        private bool _status;

        public string Comando { get => _comando; set => _comando = value; }
        public bool Status { get => _status; set => _status = value; }
    }
}