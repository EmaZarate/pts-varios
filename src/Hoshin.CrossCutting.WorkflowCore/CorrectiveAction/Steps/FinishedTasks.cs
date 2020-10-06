using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Steps
{
    public class FinishedTasks : StepBody
    {
        const string STATE_PLANNED_CODE = "TRT";
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        private readonly ICorrectiveActionStatesHistoryRepository _correctiveActionStateHistoryRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly EmailSettings _emailSettings;

        public string EmitterUserID { get; set; }

        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        
        public FinishedTasks(
            ICorrectiveActionRepository correctiveActionRepository,
            ICorrectiveActionStateRepository correctiveActionStateRepository,
            IUserWorkflowRepository userWorkflowRepository,
            ISectorPlantRepository sectorPlantRepository,
            ICorrectiveActionStatesHistoryRepository correctiveActionStateHistoryRepository,
            IOptions<EmailSettings> emailSettings)
        {
            EmailAddresses = new List<string>();
            _correctiveActionRepository = correctiveActionRepository;
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _userWorkflowRepository = userWorkflowRepository;
            _correctiveActionStateHistoryRepository = correctiveActionStateHistoryRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _emailSettings = emailSettings.Value;
        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var correctiveAction = _correctiveActionRepository.GetOneByWorkflowId(context.Workflow.Id);
            correctiveAction.CorrectiveActionStateID = _correctiveActionStateRepository.GetByCode(STATE_PLANNED_CODE);
            correctiveAction.EffectiveDateImplementation = DateTime.Now;
            _correctiveActionRepository.Update(correctiveAction);
            _correctiveActionStateHistoryRepository.Add(correctiveAction.CorrectiveActionID, correctiveAction.CorrectiveActionStateID, EmitterUserID);

            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleSGC());
            EmailAddresses.Add(_userWorkflowRepository.GetUserEmailByID(correctiveAction.ResponsibleUserID));
            EmailAddresses.Add(_userWorkflowRepository.GetUserEmailByID(correctiveAction.ReviewerUserID));
            EmailAddresses.AddRange(_sectorPlantRepository.GetSectorPlantReferredEmail(Convert.ToInt32(correctiveAction.PlantTreatmentID), Convert.ToInt32(correctiveAction.SectorTreatmentID)));

            correctiveAction = _correctiveActionRepository.GetOneByWorkflowId(correctiveAction.WorkflowId);
            var emailType = "finishedtasks";

            this.EmailSubject = EmailStrings.GetSubjectCorrectiveAction(emailType);
            this.EmailMessage = EmailStrings.GetMessageCorrectiveAction(correctiveAction, _emailSettings.Url, emailType);

            return ExecutionResult.Next();
        }
    }
}
