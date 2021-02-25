using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaPlanilhasNAV
    {
        public static List<PlanilhaNavDTO> ObtemPlanilhasNAV(string guidArquivo)
        {
            List<PlanilhaNavDTO> lista = null;

            string qryUser = "SELECT "
                            + "lv_planilha.GUID as GUID,"
                            + "lv_planilha.NOME as NOME,"
                            + "lv_planilha.GUID_TIPO as GUID_TIPO,"
                            + "lv_planilha.REVISAO as REV"
                            + " FROM "
                            + "listaverificacao.lv_planilha"
                            + " WHERE "
                            + "lv_planilha.GUID_TIPO = '" + guidArquivo + "'";

            using (var conexaoBD = new ConexaoMySQL())
            {
                lista = conexaoBD.MySqlConnection.Query<PlanilhaNavDTO>(qryUser).ToList();
            }

            return lista;



        }
    }
}
