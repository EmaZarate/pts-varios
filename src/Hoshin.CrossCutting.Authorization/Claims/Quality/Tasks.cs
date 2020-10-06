using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class Tasks:IClaim
    {
        public string Quality { get; set; }

        public const string Reedtask = "task.read";
        public const string EditTask = "task.edit";
        public const string ExtendDueDate = "task.extend.duedate";
        public const string RequestDueDateExtention = "task.request.duedateextention";
    }
}
