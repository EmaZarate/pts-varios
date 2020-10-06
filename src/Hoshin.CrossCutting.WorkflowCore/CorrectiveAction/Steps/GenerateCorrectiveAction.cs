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
    public class GenerateCorrectiveAction : StepBody
    {
        const string STATE_PLANNED_CODE = "PLN";
        const string STATE_PARAMETRIZATION_CORRECTIVEACTION_CODE_EVALUATION = "FVE";

        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        private readonly ICorrectiveActionStatesHistoryRepository _correctiveActionStateHistoryRepository;
        private readonly IParametrizationCorrectiveActionRepository _parametrizationCorrectiveActionRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly EmailSettings _emailSettings;
        public DateTime MaxDateEfficiencyEvaluation { get; set; }
        public DateTime MaxDateImplementation { get; set; }
        public string Impact { get; set; }

        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        public string EmitterUserID { get; set; }

        public GenerateCorrectiveAction(
            ICorrectiveActionRepository correctiveActionRepository,
            ICorrectiveActionStateRepository correctiveActionStateRepository,
            IUserWorkflowRepository userWorkflowRepository,
            ICorrectiveActionStatesHistoryRepository correctiveActionStateHistoryRepository,
            IParametrizationCorrectiveActionRepository parametrizationCorrectiveActionRepository,
            ITaskRepository taskRepository,
            ISectorPlantRepository sectorPlantRepository,
            IOptions<EmailSettings> emailSettings)
        {
            EmailAddresses = new List<string>();
            _correctiveActionRepository = correctiveActionRepository;
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _userWorkflowRepository = userWorkflowRepository;
            _correctiveActionStateHistoryRepository = correctiveActionStateHistoryRepository;
            _parametrizationCorrectiveActionRepository = parametrizationCorrectiveActionRepository;
            _taskRepository = taskRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _emailSettings = emailSettings.Value;
        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var correctiveAction = _correctiveActionRepository.GetOneByWorkflowId(context.Workflow.Id);
            correctiveAction.CorrectiveActionStateID = _correctiveActionStateRepository.GetByCode(STATE_PLANNED_CODE);
            correctiveAction.MaxDateEfficiencyEvaluation = MaxDateEfficiencyEvaluation;
            correctiveAction.MaxDateImplementation = MaxDateImplementation;
            correctiveAction.DeadlineDateEvaluation = MaxDateEfficiencyEvaluation.AddDays(_parametrizationCorrectiveActionRepository.GetByCode(STATE_PARAMETRIZATION_CORRECTIVEACTION_CODE_EVALUATION));
            correctiveAction.Impact = Impact;

            _correctiveActionRepository.Update(correctiveAction);
            _correctiveActionStateHistoryRepository.Add(correctiveAction.CorrectiveActionID, correctiveAction.CorrectiveActionStateID, EmitterUserID);

            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleSGC());
            EmailAddresses.Add(_userWorkflowRepository.GetUserEmailByID(correctiveAction.ResponsibleUserID));
            EmailAddresses.AddRange(_taskRepository.GetAllResponsibleUserEmailForCorrectiveAction(correctiveAction.CorrectiveActionID));
            EmailAddresses.AddRange(_sectorPlantRepository.GetSectorPlantReferredEmail(Convert.ToInt32(correctiveAction.PlantTreatmentID), Convert.ToInt32(correctiveAction.SectorTreatmentID)));

            correctiveAction = _correctiveActionRepository.GetOneByWorkflowId(correctiveAction.WorkflowId);
            var emailType = "generate";

            this.EmailSubject = EmailStrings.GetSubjectCorrectiveAction(emailType);
            this.EmailMessage = EmailStrings.GetMessageCorrectiveAction(correctiveAction, _emailSettings.Url, emailType);

            return ExecutionResult.Next();
        }
    }
}
