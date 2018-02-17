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
        public int ID;
        public Task curTask;
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
        public void UpdateTask(int TaskID)
        {
            Task myTsk = StorageTasks.Repository.Tasks.Where(p => p.Id == TaskID).FirstOrDefault();
            if (myTsk != null)
            {
                StorageTasks.Repository.UpdateTask(myTsk);
            }
        }
        public IEnumerable<Executor> GetExecutors()
        {
            return StorageExecutors.Repository.Executors;
        }
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