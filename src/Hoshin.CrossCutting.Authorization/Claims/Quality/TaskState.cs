using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class TaskStates:IClaim
    {
        public string Quality { get; set; }

        public const string AddTaskState = "taskstate.add";
        public const string EditTaskState = "taskstate.edit";
        public const string ReadTaskState = "taskstate.read";
        public const string DeactivateTaskState = "taskstate.deactivate";
        public const string ActivateTaskState = "taskstate.activate";
    }
}
