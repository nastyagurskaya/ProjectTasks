using ProjectTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectTasks.Pages
{
    public partial class MainPage : System.Web.UI.Page
    {
        //private Storage repository = new Storage();

        public IEnumerable<Task> GetTasks()
        {
            return StorageTasks.Repository.Tasks;
        }
        public void DeleteTask(int TaskID)
        {
            Task myTsk = StorageTasks.Repository.Tasks.Where(p => p.Id == TaskID).FirstOrDefault();
            if (myTsk != null)
            {
                StorageTasks.Repository.DeleteTask(myTsk);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int selectedTaskId;
                if (int.TryParse(Request.Form["delete"], out selectedTaskId))
                {
                    DeleteTask(selectedTaskId);
                }
            }
            if (IsPostBack)
            {
                int selectedTaskId;
                if (int.TryParse(Request.Form["edit"], out selectedTaskId))
                {
                    Response.Redirect("~/Pages/Update.aspx?id="+ selectedTaskId);
                    //UpdateTask(selectedGameId);
                }
            }
        }
    }
}