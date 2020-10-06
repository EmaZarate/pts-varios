using Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDUser;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveActionStates;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction
{
    public class CorrectiveActionDTO
    {
        public int CorrectiveActionID { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int? RelatedFindingId { get; set; }
        public int? PlantLocationID { get; set; }
        public int? SectorLocationID { get; set; }
        public int? PlantTreatmentID { get; set; }
        public int? SectorTreatmentID { get; set; }
        public string ResponsibleUserID { get; set; }
        public string LastResponsibleUserID { get; set; }

        public string ReviewerUserID { get; set; }
        public int CorrectiveActionStateID { get; set; }
        public CorrectiveActionStateDTO CorrectiveActionState { get; set; }
        public UserDTO ResponisbleUser { get; set; }
        public IFormFile[] CorrectiveActionEvidences { get; set; }
        public string[] FileNamesToDelete { get; set; }
        public string EvaluationCommentary { get; set; }
        public bool isEffective { get; set; }
        public string WorkflowId { get; set; }
        public string FindingID { get; set; }
        public string EmitterUserID { get; set; }
        public string WorkGroup { get; set; }
        public string RootReason { get; set; }
        public string ImmediateAction { get; set; }
        public string Impact { get; set; }
        public DateTime MaxDateImplementation { get; set; }
        public DateTime EffectiveDateImplementation { get; set; }
        public DateTime MaxDateEfficiencyEvaluation { get; set; }
        public DateTime dateTimeEfficiencyEvaluation { get; set; }
        public DateTime DeadlineDateEvaluation { get; set; }
        public DateTime DeadlineDatePlanification { get; set; }
        [JsonProperty("date")]
        public DateTime OverdueTime { get; set; }
        public string Observation { get; set; }
        
    }
}
