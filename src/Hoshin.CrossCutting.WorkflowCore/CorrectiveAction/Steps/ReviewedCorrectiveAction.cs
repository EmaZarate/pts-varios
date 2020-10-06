using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Steps
{
    public class ReviewedCorrectiveAction : StepBody
    {
        const string STATE_CLOSED_CODE = "CER";
        private readonly IWorkflowCore _workflowCore;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly ICorrectiveActionEvidenceRepository _correctiveActionEvidenceRepository;
        private readonly ICorrectiveActionStatesHistoryRepository _correctiveActionStateHistoryRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly EmailSettings _emailSettings;

        public string EmitterUserID { get; set; }

        public bool isEffective { get; set; }
        public string EvaluationCommentary { get; set; }
        public List<string> EvidencesUrl { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        public ReviewedCorrectiveAction(
            ICorrectiveActionRepository correctiveActionRepository,
            ICorrectiveActionStateRepository correctiveActionStateRepository,
            ICorrectiveActionEvidenceRepository correctiveActionEvidenceRepository,
            ICorrectiveActionStatesHistoryRepository correctiveActionStateHistoryRepository,
            ISectorPlantRepository sectorPlantRepository,
            IWorkflowCore workflowCore,
            IUserWorkflowRepository userWorkflowRepository,
            IOptions<EmailSettings> emailSettings)
        {
            EmailAddresses = new List<string>();
            _correctiveActionRepository = correctiveActionRepository;
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _correctiveActionEvidenceRepository = correctiveActionEvidenceRepository;
            _correctiveActionStateHistoryRepository = correctiveActionStateHistoryRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _userWorkflowRepository = userWorkflowRepository;
            _workflowCore = workflowCore;
            _emailSettings = emailSettings.Value;
        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var correctiveAction = _correctiveActionRepository.GetOneByWorkflowId(context.Workflow.Id);
            correctiveAction.CorrectiveActionStateID = _correctiveActionStateRepository.GetByCode(STATE_CLOSED_CODE);
            correctiveAction.EvaluationCommentary = EvaluationCommentary;
            correctiveAction.dateTimeEfficiencyEvaluation = DateTime.Now;
            correctiveAction.isEffective = isEffective;

            _correctiveActionEvidenceRepository.Update(correctiveAction.CorrectiveActionID, EvidencesUrl, new List<string>());

            _correctiveActionRepository.Update(correctiveAction);
            _correctiveActionStateHistoryRepository.Add(correctiveAction.CorrectiveActionID, correctiveAction.CorrectiveActionStateID, EmitterUserID);

            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleSGC());
            EmailAddresses.Add(_userWorkflowRepository.GetUserEmailByID(correctiveAction.ResponsibleUserID));
            EmailAddresses.AddRange(_sectorPlantRepository.GetSectorPlantReferredEmail(Convert.ToInt32(correctiveAction.PlantTreatmentID), Convert.ToInt32(correctiveAction.SectorTreatmentID)));

            correctiveAction = _correctiveActionRepository.GetOneByWorkflowId(correctiveAction.WorkflowId);
            var emailType = "reviewed";

            this.EmailSubject = EmailStrings.GetSubjectCorrectiveAction(emailType);
            this.EmailMessage = EmailStrings.GetMessageCorrectiveAction(correctiveAction, _emailSettings.Url, emailType);

            if (!isEffective)
            {
                //EmailMessage = "Se ha rechazado la evaluación de la accion correctiva. " + EvaluationCommentary;

                correctiveAction.Flow = "CorrectiveAction";
                correctiveAction.FlowVersion = 1;
                _workflowCore.StartFlow(correctiveAction);
            }

            return ExecutionResult.Next();
        }
    }
}
