using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesRepositoriosLeitura
{
    public class SendConfirmarVM
    {
        public SendConfirmarVM(string gUID_LV, string isConfiguarcaoDupla, string gUID_USUARIO, string gUID_CONFIRMACAO, 
            string oRDENADOR)//, bool houveSomentePrimeira)
        {
            GUID_LV = gUID_LV;
            IsConfiguarcaoDupla = isConfiguarcaoDupla;
            GUID_USUARIO = gUID_USUARIO;
            GUID_CONFIRMACAO = gUID_CONFIRMACAO;
            ORDENADOR = oRDENADOR;
            //HouveSomentePrimeira = houveSomentePrimeira;
        }

        public SendConfirmarVM(string gUID_LV)
        {
            GUID_LV = gUID_LV;
        }


        public string GUID_LV { get; set; }
        public string IsConfiguarcaoDupla { get; set; }
        public string GUID_USUARIO { get; set; }
        public string GUID_CONFIRMACAO { get; set; }
        public string ORDENADOR { get; set; }

        //public bool HouveSomentePrimeira { get; set; }

    }
}
