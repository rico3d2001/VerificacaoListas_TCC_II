using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LV_PresenterAPI.Comandos
{
    public abstract class CmdGeral
    {
        protected string _baseURL;

        public CmdGeral()
        {
            //_baseURL = "http://sap/ApiLV/";
            _baseURL = "https://localhost:44355/";

        }
    }
}