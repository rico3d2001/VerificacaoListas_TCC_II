using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;
using Dapper;
namespace RepositorioLeitura.Consultas
{
    public class ConsultaArquivosNAV
    {

        public static List<ArquivoNavDTO> ObtemArquivosNAV(string guidConfiguracao)
        {
            List<ArquivoNavDTO> lista = null;

           

            string qryUser = "SELECT "
                            + "swp.lv_tipo.GUID,"
                            + "swp.lv_tipo.NOME,"
                            + "swp.lv_tipo.SIGLA,"
                            + "swp.lv_tipo.GUID_CONFIG"
                            + " FROM "
                            + "swp.lv_tipo"
                            + " WHERE "
                            + "swp.lv_tipo.GUID_CONFIG = '" + guidConfiguracao + "'";

            using (var conexaoBD = new Conexao())
            {
                lista = conexaoBD.OracleConnection.Query<ArquivoNavDTO>(qryUser).ToList();
            }

            return lista;



        }
    }
}
