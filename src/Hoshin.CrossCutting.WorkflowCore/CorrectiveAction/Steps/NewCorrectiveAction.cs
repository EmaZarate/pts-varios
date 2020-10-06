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
    public class NewCorrectiveAction : StepBody
    {
        const string STATE_OPEN_CODE = "ABI";
        const string STATE_PARAMETRIZATION_CORRECTIVEACTION_CODE_PLANIFICATION = "FVP";

        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        private readonly ICorrectiveActionStatesHistoryRepository _correctiveActionStateHistoryRepository;
        private readonly IParametrizationCorrectiveActionRepository _parametrizationCorrectiveActionRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly EmailSettings _emailSettings;

        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public int? FindingID { get; set; }
        public string EmitterUserID { get; set; }
        public int PlantLocationID { get; set; }
        public int SectorLocationID { get; set; }
        public int PlantTreatmentID { get; set; }
        public int SectorTreatmentID { get; set; }
        public string ResponsibleUserID { get; set; }
        public string ReviewerUserID { get; set; }
        public List<string> EmailAddresses { get; set; }

        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        

        public NewCorrectiveAction(
            ICorrectiveActionRepository correctiveActionRepository,
            ICorrectiveActionStatesHistoryRepository correctiveActionStateHistoryRepository,
            ICorrectiveActionStateRepository correctiveActionStateRepository,
            IUserWorkflowRepository userWorkflowRepository,
            IParametrizationCorrectiveActionRepository parametrizationCorrectiveActionRepository,
            ISectorPlantRepository sectorPlantRepository,
            IOptions<EmailSettings> emailSettings)
        {
            EmailAddresses = new List<string>();
            _correctiveActionRepository = correctiveActionRepository;
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _userWorkflowRepository = userWorkflowRepository;
            _correctiveActionStateHistoryRepository = correctiveActionStateHistoryRepository;
            _parametrizationCorrectiveActionRepository = parametrizationCorrectiveActionRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _emailSettings = emailSettings.Value;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var correctiveAction = new CorrectiveActionWorkflowData();
            correctiveAction.WorkflowId = context.Workflow.Id;
            correctiveAction.CreationDate = CreationDate;
            correctiveAction.Description = Description;
            correctiveAction.FindingID = FindingID;
            correctiveAction.EmitterUserID = EmitterUserID;
            correctiveAction.PlantLocationID = PlantLocationID;
            correctiveAction.SectorLocationID = SectorLocationID;
            correctiveAction.PlantTreatmentID = PlantTreatmentID;
            correctiveAction.SectorTreatmentID = SectorTreatmentID;
            correctiveAction.ResponsibleUserID = ResponsibleUserID;
            correctiveAction.ReviewerUserID = ReviewerUserID;
            correctiveAction.DeadlineDatePlanification = CreationDate.AddDays(_parametrizationCorrectiveActionRepository.GetByCode(STATE_PARAMETRIZATION_CORRECTIVEACTION_CODE_PLANIFICATION));

            correctiveAction.CorrectiveActionStateID = _correctiveActionStateRepository.GetByCode(STATE_OPEN_CODE);
            CorrectiveActionWorkflowData correctiveActionWorkFlowData  = _correctiveActionRepository.Add(correctiveAction);
            _correctiveActionStateHistoryRepository.Add(correctiveActionWorkFlowData.CorrectiveActionID, correctiveActionWorkFlowData.CorrectiveActionStateID, EmitterUserID);

            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleSGC());
            EmailAddresses.Add(_userWorkflowRepository.GetUserEmailByID(ResponsibleUserID));
            EmailAddresses.Add(_userWorkflowRepository.GetUserEmailByID(ReviewerUserID));
            EmailAddresses.AddRange(_sectorPlantRepository.GetSectorPlantReferredEmail(Convert.ToInt32(correctiveAction.PlantTreatmentID), Convert.ToInt32(correctiveAction.SectorTreatmentID)));

            correctiveAction = _correctiveActionRepository.GetOneByWorkflowId(correctiveAction.WorkflowId);
            var emailType = "new";

            this.EmailSubject = EmailStrings.GetSubjectCorrectiveAction(emailType);
            this.EmailMessage = EmailStrings.GetMessageCorrectiveAction(correctiveAction, _emailSettings.Url, emailType);

            return ExecutionResult.Next();

        }
    }
}
