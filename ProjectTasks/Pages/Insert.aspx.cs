using ProjectTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectTasks.Pages
{
    public partial class Insert : System.Web.UI.Page
    {
        public String error = "";
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
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Insert button click event
        /// </summary>
        protected void insertBtn_Click(object sender, EventArgs e)
        {
            if (Request.Form["ExecutorID"] == "")
            {
                error = "ExecutorID requared";
                return;
            }
            if (Page.IsValid)
            {
                Task rsvp = new Task();
                rsvp.Name = Request.Form["name"];
                rsvp.Workload = Request.Form["workload"];
                rsvp.StartDate = Convert.ToDateTime(Request.Form["StartDate"]);
                rsvp.EndDate = Convert.ToDateTime(Request.Form["EndDate"]);
                rsvp.Status = Request.Form["Status"];
                rsvp.ExecutorID = Convert.ToInt32(Request.Form["ExecutorID"]);
                StorageTasks.Repository.AddTask(rsvp);
                error = "";
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}