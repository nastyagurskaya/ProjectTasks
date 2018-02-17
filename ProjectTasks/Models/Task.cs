namespace ProjectTasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
   
    public partial class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Workload { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public int? ExecutorID { get; set; }

        public virtual Executor Executor { get; set; }
        public String ToString()
        {

            return String.Format(@"
                        <div class='task'>
                            <h3>{0}</h3>
                            <font color = blue> Workload:</font> {1}
                            <br/><font color = green> StartDate: </font> {2}
                            <br/><font color = orange> EndDate: </font> {3}
                            <h4>{4} </h4
                            <h4>{5} </h4>
                        </div>",
                         this.Name, this.Workload, this.StartDate, this.EndDate, this.Executor.Name, this.Status);
        }
    }
}
