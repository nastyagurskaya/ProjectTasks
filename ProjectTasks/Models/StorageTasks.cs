﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectTasks.Models
{
    /// <summary>
    /// Class for work with ETask table content
    /// </summary>
    public class StorageTasks
    {
        private static SqlConnection conn;
        private static StorageTasks _storage;

        public List<Task> Tasks
        {
            get;
            set;
        }
        /// <summary>
        /// Selecting/Updating data in the list of Tasks
        /// </summary>
        public void Update()
        {
            OpenConn();
            SqlCommand cmd = null;
            SqlDataReader tableData = null;

            Tasks.RemoveAll((item) => true);

            try
            {
                var cmdSql = "SELECT * FROM Task;";
                cmd = new SqlCommand(cmdSql, conn);
                tableData = cmd.ExecuteReader();

                if (!tableData.HasRows)
                {
                    return;
                }


                while (tableData.Read())
                {
                    var tsk = new Task {
                        Id = tableData.GetInt32(0),
                        Name = tableData.GetString(1),
                        Workload = tableData.GetString(2),
                        StartDate = tableData.GetDateTime(3),
                        EndDate = tableData.GetDateTime(4),
                        Status = tableData.GetString(5),
                        ExecutorID = tableData.GetInt32(6) };
                    string cmd1 = "Select * from Executor where id=" + tsk.ExecutorID + ";";
                    SqlCommand cmd2 = new SqlCommand(cmd1, conn);
                    SqlDataReader tableData2 = cmd2.ExecuteReader();
                    tableData2.Read();
                    var exc = new Executor
                    {
                        Id = tableData2.GetInt32(0),
                        Name = tableData2.GetString(1),
                        Surname = tableData2.GetString(2),
                        Patronymic_ = tableData2.GetString(3),
                    };
                    tsk.Executor = exc;
                    this.Tasks.Add(tsk);
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
        static StorageTasks()
        {
            _storage = new StorageTasks();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        private StorageTasks()
        {
            Tasks = new List<Task>();
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
        public static StorageTasks Repository
        {
            get
            {
                return _storage;
            }
        }
        /// <summary>
        /// Update record in the Database
        /// </summary>
        public void UpdateTask(Task task)
        {
            var comm = new SqlCommand { Connection = conn };

            try
            {
                conn.Open();
                comm.CommandText = "UPDATE Task SET Name=@Name, Workload=@Workload, StartDate=@StartDate, EndDate=@EndDate, Status = @Status, ExecutorID = @ExecutorID WHERE ID=@ID";
                comm.Parameters.AddWithValue("ID", task.Id);
                comm.Parameters.AddWithValue("Name", (object)task.Name ?? DBNull.Value);
                comm.Parameters.AddWithValue("Workload", (object)task.Workload ?? DBNull.Value);
                comm.Parameters.AddWithValue("StartDate", (object)task.StartDate ?? DBNull.Value);
                comm.Parameters.AddWithValue("EndDate", (object)task.EndDate ?? DBNull.Value);
                comm.Parameters.AddWithValue("Status", (object)task.Status ?? DBNull.Value);
                comm.Parameters.AddWithValue("ExecutorID", (object)task.ExecutorID ?? DBNull.Value);
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }
            finally
            {
                CloseConn();
            }
        }
        /// <summary>
        /// Delete record from the Database
        /// </summary>
        public void DeleteTask(Task tsk)
        {
            var comm = new SqlCommand { Connection = conn };

            try
            {
                conn.Open();
                comm.CommandText = "DELETE Task WHERE ID=@ID";
                comm.Parameters.AddWithValue("ID", tsk.Id);
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }
            finally
            {
                CloseConn();
            }
            Update();
        }
        /// <summary>
        /// Insert record into the Database
        /// </summary>
        public void AddTask(Task task)
        {

            conn.Open();
            var comm = new SqlCommand { Connection = conn };

            try
            {
                comm.CommandText = "INSERT INTO Task(Name, Workload, StartDate, EndDate, Status, ExecutorID) VALUES(@Name, @Workload, @StartDate, @EndDate, @Status, @ExecutorID)";
                comm.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Name",
                    Value = (object)task.Name ?? DBNull.Value
                });
                comm.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Workload",
                    Value = (object)task.Workload ?? DBNull.Value
                });
                comm.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@StartDate",
                    Value = (object)task.StartDate ?? DBNull.Value
                });
                comm.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@EndDate",
                    Value = (object)task.EndDate ?? DBNull.Value
                });
                comm.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Status",
                    Value = (object)task.Status ?? DBNull.Value
                });
                comm.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@ExecutorID",
                    Value = (object)task.ExecutorID ?? DBNull.Value
                });
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
            }
            finally
            {
                CloseConn();
            }
            Update();
        }
    }
}