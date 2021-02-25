
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LVModel
{
    public class LogAuditoria
    {
        private string guid_usuario;

        public LogAuditoria(string guid_usuario)
        {
            this.guid_usuario = guid_usuario;
        }

        //public void CriaDocumento(string documento)
        //{
        //    string desc = string.Format("Documento {0} Criado por {1}", documento, guid_usuario);

        //    string guid = Guid.NewGuid().ToString();

        //    LV_LOG_AUDITORIA lv = new LV_LOG_AUDITORIA()
        //    {
        //        GUID = guid,
        //        DATA = DateTime.Now,
        //        DESCRICAO = desc,
        //        GUID_USUARIO = guid_usuario
        //    };

        //    new Repository<LV_LOG_AUDITORIA>().Insert(lv);
        //}

        //public void MudaRegistro(string status_novo, string status_antigo, string guid_revisao, string documento, string item)
        //{
        //    var rev = new RepositoryGUID<LV_REVISAO>().ReturnByGUID(guid_revisao);

        //    var indice = rev.INDICE;

        //    string desc = string.Format("{0} mudou o registro {1} da revisão {2} do documento {3} de {4} para {5}", guid_usuario, item, indice, documento, status_antigo, status_novo);

        //    string guid = Guid.NewGuid().ToString();

        //    if(!status_novo.Equals(status_antigo))
        //    {
        //        LV_LOG_AUDITORIA lv = new LV_LOG_AUDITORIA()
        //        {
        //            GUID = guid,
        //            DATA = DateTime.Now,
        //            DESCRICAO = desc,
        //            GUID_USUARIO = guid_usuario
        //        };

        //        new Repository<LV_LOG_AUDITORIA>().Insert(lv);
        //    }
        //}

        //public void ConfirmaRevisao(string revisao)
        //{
        //    string desc = string.Format("Revisão {0} Confirmada por {1}", revisao, guid_usuario);

        //    string guid = Guid.NewGuid().ToString();

        //    LV_LOG_AUDITORIA lv = new LV_LOG_AUDITORIA()
        //    {
        //        GUID = guid,
        //        DATA = DateTime.Now,
        //        DESCRICAO = desc,
        //        GUID_USUARIO = guid_usuario
        //    };

        //    new Repository<LV_LOG_AUDITORIA>().Insert(lv);
        //}
    }
}
