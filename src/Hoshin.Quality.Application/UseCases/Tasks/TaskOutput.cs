using Hoshin.Core.Domain.Users;
using Hoshin.Quality.Domain.Evidence;
using Hoshin.Quality.Domain.TaskEvidence;
using Hoshin.Core.Application.UseCases.User.GetAllUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Tasks
{
    public class TaskOutput
    {
      
        public int TaskID { get; set; }
        public int EntityID { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserID { get; set; }
        public DateTime ImplementationPlannedDate { get; set; }
        public DateTime? ImplementationEffectiveDate { get; set; }
        public bool RequireEvidence { get; set; }

        public string Result { get; set; }
        public string Observation { get; set; }
        public int TaskStateID { get; set; }
        public int EntityType { get; set; }
        public List<Evidence> TaskEvidences { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public TaskState.TaskStateOutput TaskState { get; set; }
        public UserOutput ResponsibleUser { get; set; }

        public TaskOutput()
        {
        }
    }
}
