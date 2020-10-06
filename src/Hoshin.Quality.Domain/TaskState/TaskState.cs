using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.TaskState
{
    public class TaskState
    {
        public int TaskStateID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }
    

    public TaskState(string code, string name, string color, bool active, int id)
    {
            this.TaskStateID = id;
            this.Name = name;
            this.Color = color;
            this.Active = active;
            this.Code = code; 
    }

        public TaskState(string code, string name, string color, bool active)
        {
            this.Name = name;
            this.Color = color;
            this.Active = active;
            this.Code = code;
        }
  }
}
