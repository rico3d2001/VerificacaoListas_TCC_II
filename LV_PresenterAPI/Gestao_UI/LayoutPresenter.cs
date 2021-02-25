using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LV_PresenterAPI.Gestao_UI
{
    public class LayoutPresenter
    {
        private string _layout = "";

        public LayoutPresenter(bool isVerificador, bool existemRevisoesNesteDocumento, bool existemRevisoesNaoConfimadas,
            int isVerificadorUnico, bool abriuNaoConfirmouAinda, bool retomada, bool isGestor)
        {

            _defineSegundoDocumentoEncontrado(
                isVerificador,
                existemRevisoesNesteDocumento,
                existemRevisoesNaoConfimadas,
                isVerificadorUnico == 1 ? true : false,
                abriuNaoConfirmouAinda,
                retomada,
                isGestor);

        }

        public LayoutPresenter(string layout)
        {
            _layout = layout;
        }

        public string Layout { get => _layout; }


        private void _defineSegundoDocumentoEncontrado(bool isVerificador, bool existemRevisoesNesteDocumento, bool existemRevisoesNaoConfimadas,
            bool isVerificadorUnico, bool acabou_de_abrir, bool retomada, bool isGestor)
        {



            if (isVerificador)
            {
                if (existemRevisoesNesteDocumento)
                {

                    if (existemRevisoesNaoConfimadas)
                    {
                        if (isVerificadorUnico)
                        {
                            _layout = "~/Views/Shared/_Layout_MI_CR.cshtml";
                        }
                        else if (acabou_de_abrir)
                        {
                            _layout = "~/Views/Shared/_Layout_MI_CR.cshtml";
                        }
                        else if (retomada)
                        {
                            _layout = "~/Views/Shared/_Layout_MI_CR.cshtml";
                        }
                        else
                        {
                            _layout = "~/Views/Shared/_Layout_NO_ACTION.cshtml";
                        }

                    }
                    else
                    {

                        _layout = "~/Views/Shared/_Layout_AR_RR.cshtml";

                    }
                }
                else
                {

                    _layout = "~/Views/Shared/_Layout_AR.cshtml";
                }
            }
            else
            {

                if (isGestor)
                {
                    _layout = "~/Views/Shared/_Layout_ER.cshtml";
                }
                else
                {
                    _layout = "~/Views/Shared/_Layout_NO_ACTION.cshtml";
                }

            }


        }




    }
}