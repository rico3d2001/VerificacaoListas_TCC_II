using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class StatusRevisoesLV
    {

        public StatusRevisoesLV()
        {
            Indices = new List<string>();
        }

        //public bool ExistemRevisoesNaoConfimadas { get; set; }
        public bool ExistemRevisoesNesteDocumento { get; set; }

        public bool NaoTemRevisoesIndefinidas { get; set; }

        public bool PossuiRevisoesNaoConfirmadas { get; set; }

        public List<string> Indices { get; set; }
        public bool LVEmitida { get; set; }
    }
}
