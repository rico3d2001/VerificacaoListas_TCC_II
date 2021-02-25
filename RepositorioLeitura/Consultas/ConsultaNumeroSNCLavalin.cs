using Dapper;
using EntidadesRepositoriosLeitura;
using System.Linq;

namespace RepositorioLeitura.Consultas
{
    public class ConsultaNumeroSNCLavalin
    {
        public static NumeroSNCLV ObtemNumeroSNCLvalin(string numeroSNC)
        {
            NumeroSNCLV numero = null;

            string qryNumero = "SELECT "
                        + "swp.lv_numero_snc.guid AS GUID_LV,"
                        + "swp.lv_numero_snc.numero AS NUMERO,"
                        + "swp.lv_numero_snc.GUID_ULTIMA_CONFIRMACAO AS GUID_ULTIMA_CONFIRMACAO"
                        + " FROM "
                        + "swp.lv_numero_snc"
                        + " WHERE "
                        + "swp.lv_numero_snc.numero = '" + numeroSNC + "'";

            using (var conexaoBD = new Conexao())
            {
                var respostaProjeto = conexaoBD.OracleConnection.Query<NumeroSNCLV>(qryNumero);


                numero = respostaProjeto.FirstOrDefault();


            }

            return numero;



            }
    }
}
