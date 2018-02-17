using ProjectTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectTasks.Pages
{
    public partial class Update : System.Web.UI.Page
    {
        public String error = "";
        /// <value>
        /// Id of current task
        /// </value>
        public int ID;
        /// <value>
        /// Current task to update
        /// </value>
        public Task curTask;
        /// <summary>
        /// Page_Load method
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
           // DataBind();
            ID = Convert.ToInt32(Request.QueryString["id"]);
            curTask = new Task();
            curTask = StorageTasks.Repository.Tasks.Where(p => p.Id == ID).FirstOrDefault();
            name.Text = curTask.Name;
            workload.Text = curTask.Workload;
            StartDate.Text = ((DateTime)curTask.StartDate).ToString("dd.MM.yy");
            EndDate.Text = ((DateTime)curTask.EndDate).ToString("dd.MM.yy");
            Status.SelectedValue = curTask.Status;

        }
        /// <summary>
        /// Update task in database
        /// </summary>
        public void UpdateTask(int TaskID)
        {
            Task myTsk = StorageTasks.Repository.Tasks.Where(p => p.Id == TaskID).FirstOrDefault();
            if (myTsk != null)
            {
                StorageTasks.Repository.UpdateTask(myTsk);
            }
        }
        /// <summary>
        /// Get all executors
        /// </summary>
        /// <returns>
        /// List of executors
        /// </returns>
        public IEnumerable<Executor> GetExecutors()
        {
            return StorageExecutors.Repository.Executors;
        }
        /// <summary>
        /// IUpdate button click event
        /// </summary>
        protected void updateBtn_Click(object sender, EventArgs e)
        {
            if (Request.Form["ExecutorID"] == "")
            {
                error = "ExecutorID requared";
                return;
            }
            if (Page.IsValid)
            {
                curTask.Name = Request.Form["name"];
                curTask.Workload = Request.Form["workload"];
                curTask.StartDate = Convert.ToDateTime(Request.Form["StartDate"]);
                curTask.EndDate = Convert.ToDateTime(Request.Form["EndDate"]);
                curTask.Status = Request.Form["Status"];
                curTask.ExecutorID = Convert.ToInt32(Request.Form["ExecutorID"]);
                StorageTasks.Repository.UpdateTask(curTask);
                error = "";
                Response.Redirect("~/Pages/MainPage.aspx");
            }
        }
    }
}