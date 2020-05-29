using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace _91APP_TEST_V1.Models
{
    public class MyDataBase
    {
        public MySqlConnection conn = null;
        
        public MyDataBase()
        {
            conn = new MySqlConnection();
            string connString = "server=127.0.0.1;port=3306;user id=root;password=Sk29792130;database=test_db;charset=utf8;";
            conn.ConnectionString = connString;
        }

        public MySqlCommand MySqlCommand(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            return cmd;
        }

        public void Connect(){
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        public void Disconnect(){
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }
    }
}
