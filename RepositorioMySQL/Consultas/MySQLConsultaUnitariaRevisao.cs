using Dapper;
using EntidadesRepositoriosLeitura;
using System.Linq;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaUnitariaRevisao
    {
        public static RevUnitQuery ObtemRevisao(string guid)
        {
            RevUnitQuery rev = null;

            string qryUser = "SELECT "
                    + "lv_revisao.guid AS guid,"
                    + "lv_revisao.id_estado AS ID_ESTADO"
                    + " FROM lv_revisao"
                    + " WHERE "
                    + "lv_revisao.guid = '" + guid + "'";

            using (var conexaoBD = new ConexaoMySQL())
            {
                rev = conexaoBD.MySqlConnection.Query<RevUnitQuery>(qryUser).FirstOrDefault();
            }

            return rev;



        }
    }
}
