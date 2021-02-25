
using Oracle.DataAccess.Client;
using System;
using System.Data;

namespace RepositorioLeitura
{
    public class Conexao: IDisposable
    {
        private OracleConnection _oracleConnection;
        //private OracleConnection _oracleConnection;

        //private Conexao()
        //{
        //    string conectionString = "Data Source=swp;User ID=swp;Password=m1n3r11swp;";
        //    _oracleConnection = new OracleConnection(conectionString);
        //    _oracleConnection.Open();
        //}

        public IDbConnection OracleConnection
        {
            get
            {
                if (_oracleConnection == null)
                {
                    string conectionString = "Data Source=swp;User ID=swp;Password=m1n3r11swp;";
                    _oracleConnection = new OracleConnection(conectionString);
                    _oracleConnection.Open();
                }
                else if(_oracleConnection.State == ConnectionState.Closed)
                {
                    _oracleConnection.Open();
                }

                return _oracleConnection;

            }

            //get { return _instancia ?? (_instancia = new Conexao()); }
        }


        public IDbConnection ConexaoOracle { get => _oracleConnection; }

        public void Dispose()
        {
            _oracleConnection.Close();
        }
    }
}
