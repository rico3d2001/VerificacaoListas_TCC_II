
using LVModel;
using System.Linq;
using Dapper;

namespace RepositorioMySQL.Consultas
{
    public class MySQLConsultaUsuario
    {
        public static Usuario ObtemUsuarioPorLogin(string login)
        {
            Usuario usuario = null;

            string qryUser = "SELECT "
                            + "lv_usuario.guid,"
                            + "lv_usuario.nome,"
                            + "lv_usuario.isconfigurador,"
                            + "lv_usuario.isverificador,"
                            + "lv_usuario.isgestor,"
                            + "lv_usuario.sigla,"
                            + "lv_usuario.senha"
                            + " FROM "
                            + "lv_usuario"
                            + " WHERE "
                            + "lv_usuario.sigla = '" + login + "'";

            using (var conexaoBD = new ConexaoMySQL())
            {
                usuario = conexaoBD.MySqlConnection.Query<Usuario>(qryUser).FirstOrDefault();
            }

            return usuario;



        }
    }
}
