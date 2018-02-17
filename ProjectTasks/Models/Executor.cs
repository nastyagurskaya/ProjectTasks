namespace ProjectTasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
   
    public partial class Executor
    {
        public Executor()
        {
            //Task = new HashSet<Task>();
        }

        public int Id { get; set; }

        public string Surname { get; set; }

        
        public string Name { get; set; }

        public string Patronymic_ { get; set; }

        //public virtual ICollection<Task> Task { get; set; }
    }
}
