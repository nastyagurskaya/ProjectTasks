using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectTasks.Models
{
    public class StorageExecutors
    {
        private static SqlConnection conn;
        private static StorageExecutors _storage;

        public List<Executor> Executors
        {
            get;
            set;
        }
        public void Update()
        {
            OpenConn();
            SqlCommand cmd = null;
            SqlDataReader tableData = null;

            Executors.RemoveAll((item) => true);

            try
            {
                var cmdSql = "SELECT * FROM  Executor ;";
                cmd = new SqlCommand(cmdSql, conn);
                tableData = cmd.ExecuteReader();

                if (!tableData.HasRows)
                {
                    return;
                }


                while (tableData.Read())
                {
                    var exc = new Executor
                    {
                        Id = tableData.GetInt32(0),
                        Name = tableData.GetString(2),
                        Surname = tableData.GetString(1),
                        Patronymic_ = tableData.GetString(3),
                      
                    };
                    this.Executors.Add(exc);
                }
            }
            finally
            {
                if (tableData != null)
                {
                    tableData.Close();
                }
                CloseConn();
            }
        }

        static StorageExecutors()
        {
            _storage = new StorageExecutors();
        }


        private StorageExecutors()
        {
            Executors = new List<Executor>();

            Update();
        }
        private void OpenConn()
        {
            // var connSb = new SqlConnectionStringBuilder();
            ////connSb. = "Microsoft.Jet.OLEDB.4.0";
            //connSb.DataSource = @"|DataDirectory|\ProjectDatabase.mdf";
            //var connStr = connSb.ToString();
            //connSb.ConnectionString = "data source=(Localdb)\\MSSQLLocalDB;Initial Catalog=|DataDirectory|\\ProjectDatabase.mdf;integrated security=True;";
            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProjectModel1"].ConnectionString;
            conn.Open();
        }
        private void CloseConn()
        {
            if (conn != null)
                conn.Close();
        }
        public static StorageExecutors Repository
        {
            get
            {
                return _storage;
            }
        }
    }
}