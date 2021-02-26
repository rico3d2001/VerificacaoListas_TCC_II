using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioMySQL
{
    public class ConexaoMySQL : IDisposable
    {

        private MySqlConnection _mySqlConnection;

        public IDbConnection MySqlConnection
        {
            get
            {
                if (_mySqlConnection == null)
                {
                    string conectionString = "Server=localhost;Database=listaverificacao;Uid=rico3d;Pwd=umsa45;";
                    _mySqlConnection = new MySqlConnection(conectionString);
                    _mySqlConnection.Open();
                }
                else if (_mySqlConnection.State == ConnectionState.Closed)
                {
                    _mySqlConnection.Open();
                }

                return _mySqlConnection;

            }

        }


        public IDbConnection ConexaoOracle { get => _mySqlConnection; }

        public void Dispose()
        {
            _mySqlConnection.Close();
        }

    }
}
