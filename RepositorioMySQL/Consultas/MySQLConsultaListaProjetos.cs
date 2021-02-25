using EntidadesRepositoriosLeitura;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaListaProjetos
    {



        public static List<ProjetoToListDTO> ObtemListaProjetos()
        {



            List<ProjetoToListDTO> lista = new List<ProjetoToListDTO>();


            string qry = "SELECT"
                        + " lv_projeto.guid as GUID,"
                                + " lv_projeto.numero AS NUMERO"
                                + " FROM"
                        + " lv_projeto";

            using (var conexaoBD = new ConexaoMySQL())
            {
                lista = conexaoBD.MySqlConnection.Query<ProjetoToListDTO>(qry).ToList();

            }

            return lista;



        }
    }
}
