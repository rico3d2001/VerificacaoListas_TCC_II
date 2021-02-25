using FluentNHibernate.Mapping;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LV14FluentNHB
{
    public class GrupoCodigoDocumentoMap : ClassMap<GrupoCodigoDocumento>
    {
        public GrupoCodigoDocumentoMap()
        {
            Table("LV_GRUPO_CODIGO_DOCUMENTO");
            Id(x => x.ID);
            Map(x => x.NOME);

        }
    }
}
