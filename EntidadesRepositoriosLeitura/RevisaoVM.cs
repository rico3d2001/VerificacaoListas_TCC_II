using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class RevisaoVM
    {
        public RevisaoVM(string gUID, int iD_ESTADO,string gUID_DOC_VERIFICACAO)
        {
            GUID = gUID;
            ID_ESTADO = iD_ESTADO;
            GUID_DOC_VERIFICACAO = gUID_DOC_VERIFICACAO;
        }

        public RevisaoVM()
        {

        }
        public string GUID { get; set; }

        public int ID_ESTADO { get; set; }
        public string GUID_LV_ITEM { get; set; }
        public string GUID_LV_VERIFICADOR { get; set; }
        public DateTime DATA_VERICACAO { get; set; }

        public string GUID_DOC_VERIFICACAO { get; set; }
        public string INDICE { get; set; }
        public int ORDENADOR { get; set; }
        public int CONFIRMADO { get; set; }
        public int SALVO { get; set; }
        public int EMITIDO { get; set; }
        public string GUID_CONFIRMADO { get; set; }





    }
}
