using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class Tasks
    {
        public int TaskID { get; set; }
        public int EntityID { get; set; }
        public int EntityType { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserID { get; set; }
        public Users ResponsibleUser { get; set; }
        public DateTime ImplementationPlannedDate { get; set; }
        public DateTime? ImplementationEffectiveDate { get; set; }
        public string Observation { get; set; }
        public string Result { get; set; }
        public int TaskStateID { get; set; }
        public bool RequireEvidence { get; set; }
        public TaskStates TaskState { get; set; }
        public ICollection<TaskEvidences> TaskEvidences { get; set; }

    }


}
