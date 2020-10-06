using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.TaskState
{
   public class TaskStateOutput
    {

        public int TaskStateID { get; set; }
       
        public string Name { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }

        public TaskStateOutput()
        {

        }
        public TaskStateOutput(int id, string name, string colour,bool active,string code)

        {
            this.TaskStateID = id;
           
            this.Name = name;
            this.Color = colour;
            this.Active = active;
            this.Code = code;
            
        }
    }
}
