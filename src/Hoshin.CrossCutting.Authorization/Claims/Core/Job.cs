using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Core
{
    public class Job : IClaim
    {
        public string Core { get; set; }

        public const string AddJob = "job.add";
        public const string EditJob = "job.edit";
        public const string ViewJob = "job.read";
        public const string DeactivateJob = "job.deactivate";
        public const string ActivateJob = "job.activate";
    }
}
