using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data
{
    public class CorrectiveActionWorkflowData : IDataWorkflow
    {
        public string WorkflowId { get; set; }
        public string WorkflowData { get; set; }
        public string Flow { get; set; }
        public int FlowVersion { get; set; }
        public string EventData { get; set; }
        public string EventName { get; set; }
        public List<string> EmailAddresses { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }

        #region correctiveActionProperties
        public int CorrectiveActionID { get; set; }
        public int? FindingID { get; set; }
        public DateTime CreationDate { get; set; }
        public string EmitterUserID { get; set; }
        public int? SectorLocationID { get; set; }
        public int? PlantLocationID { get; set; }
        public int? SectorTreatmentID { get; set; }
        public int? PlantTreatmentID { get; set; }
        public string SectorTreatmentName { get; set; }
        public string ResponsibleUserID { get; set; }
        public string ResponsibleUserFullName { get; set; }
        public string WorkGroup { get; set; }
        public string Description { get; set; }
        public int CorrectiveActionStateID { get; set; }
        public string CorrectiveActionStateName { get; set; }
        public string RootReason { get; set; }
        public string ImmediateAction { get; set; }
        public string Impact { get; set; }
        public string ReviewerUserID { get; set; }
        public string ReviewerUserFullName { get; set; }
        public DateTime MaxDateImplementation { get; set; }
        public DateTime EffectiveDateImplementation { get; set; }
        public DateTime MaxDateEfficiencyEvaluation { get; set; }
        public DateTime dateTimeEfficiencyEvaluation { get; set; }
        public DateTime DeadlineDateEvaluation { get; set; }
        public DateTime DeadlineDatePlanification { get; set; }
        public bool isEffective { get; set; }
        public string EvaluationCommentary { get; set; }
        public List<string> EvidencesUrl { get; set; }
        #endregion
        public CorrectiveActionWorkflowData()
        {

        }
    }

    public class TaskWorkflowData
    {
        public int EntityID { get; set; }
        public int TaskID { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserID { get; set; }
        public string ResponsibleUserFullName { get; set; }
        public DateTime ImplementationPlannedDate { get; set; }
    }
}
