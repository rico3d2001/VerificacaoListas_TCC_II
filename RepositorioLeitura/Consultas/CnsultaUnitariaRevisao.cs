using Dapper;
using EntidadesRepositoriosLeitura;
using System.Linq;

namespace RepositorioLeitura.Consultas
{
    public class CnsultaUnitariaRevisao
    {
        public static RevUnitQuery ObtemRevisao(string guid)
        {
            RevUnitQuery rev = null;

            string qryUser = "SELECT "
                    + "swp.lv_revisao.guid AS guid,"
                    + "swp.lv_revisao.id_estado AS ID_ESTADO"
                    + " FROM swp.lv_revisao"
                    + " WHERE "
                    + "swp.lv_revisao.guid = '" + guid + "'";

            using (var conexaoBD = new Conexao())
            {
                rev = conexaoBD.OracleConnection.Query<RevUnitQuery>(qryUser).FirstOrDefault();
            }

            return rev;



        }
    }
}
