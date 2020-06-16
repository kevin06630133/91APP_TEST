using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace _91APP_TEST_V2.Models
{
    public class MyDataBase : IDisposable
    {
        public MySqlConnection conn = null;
        public MySqlTransaction trans = null;

        private bool disposed = false;
        public MyDataBase()
        {
            conn = new MySqlConnection();
            string connString = "server=127.0.0.1;port=3306;user id=root;password=Sk29792130;database=test_db;charset=utf8;";
            conn.ConnectionString = connString;
        }

        public void Connect(){
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        public void Disconnect(){
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }

        public MySqlCommand MySqlCommand(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn, trans);
            return cmd;
        }

        public void Rollback()
        {
            if (trans != null)
            {
                trans.Rollback();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            this.Disconnect();
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // 釋放資源邏輯
                }
                disposed = true;
            }
        }
    }
}
