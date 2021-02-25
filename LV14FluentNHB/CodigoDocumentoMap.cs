using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class CodigoDocumentoMap : ClassMap<CodigoDocumento>
    {
        public CodigoDocumentoMap()
        {
            Table("LV_CODIGO_DOCUMENTO");
            Id(x => x.GUID);
            Map(x => x.CODIGO);
            Map(x => x.ID_DISCIPLINA);
            Map(x => x.ID_GRUPO_COD_DOC);
        }
    }
}
