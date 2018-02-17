using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectTasks.Models
{
    /// <summary>
    /// Class for work with Executor table content
    /// </summary>
    public class StorageExecutors
    {
        private static SqlConnection conn;
        private static StorageExecutors _storage;

        public List<Executor> Executors
        {
            get;
            set;
        }
        /// <summary>
        /// Selecting/Updating data in the list of Executors
        /// </summary>
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
        /// <summary>
        /// Static constructor
        /// </summary>
        static StorageExecutors()
        {
            _storage = new StorageExecutors();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        private StorageExecutors()
        {
            Executors = new List<Executor>();

            Update();
        }
        /// <summary>
        /// Open connection with Database
        /// </summary>
        private void OpenConn()
        {
            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ProjectModel1"].ConnectionString;
            conn.Open();
        }
        /// <summary>
        /// Close connection with Database
        /// </summary>
        private void CloseConn()
        {
            if (conn != null)
                conn.Close();
        }
        /// <summary>
        /// Repository method
        /// </summary>
        /// <returns>
        /// exemplar of current class
        /// </returns>
        public static StorageExecutors Repository
        {
            get
            {
                return _storage;
            }
        }
    }
}