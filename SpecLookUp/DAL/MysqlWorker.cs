using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SpecLookUp.DAL
{
    public class MysqlWorker
    {
        private readonly string _connString =
            "SERVER= remotemysql.com" +
            ";USERID= IalS35jGSf" +
            ";PASSWORD= lHxoGp4AQC" +
            ";DATABASE= IalS35jGSf" +
            ";Connection Timeout=10;";

        private readonly MySqlConnection _connection;
        private MySqlCommand _command;

        public MysqlWorker()
        {
            _connection = new MySqlConnection{ConnectionString = _connString};
        }


        public DataTable GetFromDataBase(string cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();

                _command = new MySqlCommand(cmd,_connection);
                dt.Load(_command.ExecuteReader());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally{ _connection.Close();}

            return dt;
        }

    }
}
