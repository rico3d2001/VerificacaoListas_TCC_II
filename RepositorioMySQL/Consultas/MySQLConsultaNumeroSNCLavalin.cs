
using EntidadesRepositoriosLeitura;
using System.Linq;
using Dapper;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaNumeroSNCLavalin
    {
        public static NumeroSNCLV ObtemNumeroSNCLvalin(string numeroSNC)
        {
            NumeroSNCLV numero = null;

            string qryNumero = "SELECT "
                        + "lv_numero_snc.guid AS GUID_LV,"
                        + "lv_numero_snc.numero AS NUMERO,"
                        + "lv_numero_snc.GUID_ULTIMA_CONFIRMACAO AS GUID_ULTIMA_CONFIRMACAO"
                        + " FROM "
                        + "lv_numero_snc"
                        + " WHERE "
                        + "lv_numero_snc.numero = '" + numeroSNC + "'";

            using (var conexaoBD = new ConexaoMySQL())
            {
                var respostaProjeto = conexaoBD.MySqlConnection.Query<NumeroSNCLV>(qryNumero);


                numero = respostaProjeto.FirstOrDefault();


            }

            return numero;



            }
    }
}
