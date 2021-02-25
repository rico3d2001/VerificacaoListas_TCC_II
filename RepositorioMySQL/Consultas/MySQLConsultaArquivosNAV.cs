using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaArquivosNAV
    {

        public static List<ArquivoNavDTO> ObtemArquivosNAV(string guidConfiguracao)
        {
            List<ArquivoNavDTO> lista = null;

            string qryUser = "SELECT "
                            + "lv_tipo.GUID,"
                            + "lv_tipo.NOME,"
                            + "lv_tipo.SIGLA,"
                            + "lv_tipo.GUID_CONFIG"
                            + " FROM "
                            + "listaverificacao.lv_tipo"
                            + " WHERE "
                            + "lv_tipo.GUID_CONFIG = '" + guidConfiguracao + "'";

            using (var conexaoBD = new ConexaoMySQL())
            {
                lista = conexaoBD.MySqlConnection.Query<ArquivoNavDTO>(qryUser).ToList();
            }

            return lista;



        }
    }
}
