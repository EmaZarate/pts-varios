using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDUser;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDTaskState;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDTask
{
    public class TaskDTO
    {
        public int TaskID { get; set; }
        public int EntityID { get; set; }
        public int EntityType { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserID { get; set; }
        public UserDTO ResponsibleUser { get; set; }
        public DateTime ImplementationPlannedDate { get; set; }
        public DateTime? ImplementationEffectiveDate { get; set; }
        public bool RequireEvidence { get; set; }
        public IFormFile[] TaskEvidences { get; set; }
        public string[] DeleteEvidencesUrls { get; set; }
        public string Observation { get; set; }
        public int TaskStateID { get; set; }
        public DateTime overdureTime { get; set; }
        public TaskStateDTO TaskState { get; set; }

        public TaskDTO()
        {

        }
    }
}
