using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.Finding.Steps
{
    public class ApproveFinding : StepBody
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IFindingStateRepository _findingStateRepository;
        private readonly IFindingStatesHistoryRepository _findingStatesHistoryRepository;
        private readonly IFindingEvidenceRepository _findingEvidenceRepository;
        private readonly IWorkflowCore _workflowCore;
        private readonly EmailSettings _emailSettings;
        private IHubContext<FindingsHub> _hub;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        public ApproveFinding(
            IFindingRepository findingRepository,
            IFindingStateRepository findingStateRepository,
            IFindingStatesHistoryRepository findingStatesHistoryRepository,
            IFindingEvidenceRepository findingEvidenceRepository,
            IWorkflowCore workflowCore,
            IOptions<EmailSettings> emailSettings,
            IHubContext<FindingsHub> hub,
            IUserWorkflowRepository userWorkflowRepository
            )
        {
            _findingRepository = findingRepository;
            _findingStateRepository = findingStateRepository;
            _findingStatesHistoryRepository = findingStatesHistoryRepository;
            _findingEvidenceRepository = findingEvidenceRepository;
            _workflowCore = workflowCore;
            EmailAddresses = new List<string>();
            _emailSettings = emailSettings.Value;
            _hub = hub;
            _userWorkflowRepository = userWorkflowRepository;
        }
        public string State { get; set; }
        public string Description { get; set; }
        public int PlantLocationID { get; set; }
        public int SectorLocationID { get; set; }
        public int PlantTreatmentID { get; set; }
        public int SectorTreatmentID { get; set; }
        public string ResponsibleUserID { get; set; }
        public int FindingTypeID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        public List<string> NewEvidencesUrls { get; set; }
        public List<string> DeleteEvidencesUrls { get; set; }
        public string EmitterUserID { get; set; }
        public string ReviewerUserID { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            FindingWorkflowData finding = _findingRepository.GetOneByWorkflowId(context.Workflow.Id);
            finding.Description = this.Description;
            finding.PlantLocationID = this.PlantLocationID;
            finding.SectorLocationID = this.SectorLocationID;
            finding.PlantTreatmentID = this.PlantTreatmentID;
            finding.SectorTreatmentID = this.SectorTreatmentID;
            finding.ResponsibleUserID = this.ResponsibleUserID;
            finding.FindingTypeID = this.FindingTypeID;
            finding.ExpirationDate = this.ExpirationDate;



            if (State == "ApproveWithPDCA")
            {
                int newStateId = _findingStateRepository.GetOneByCode("APD");
                finding.FindingStateID = newStateId;
                _findingRepository.Update(finding);
                var correctiveAction = new CorrectiveActionWorkflowData();
                correctiveAction.Description = finding.Description;
                correctiveAction.PlantLocationID = finding.PlantLocationID;
                correctiveAction.SectorLocationID = finding.SectorLocationID;
                correctiveAction.PlantTreatmentID = finding.PlantTreatmentID;
                correctiveAction.SectorTreatmentID = finding.SectorTreatmentID;
                correctiveAction.ResponsibleUserID = finding.ResponsibleUserID;
                correctiveAction.FindingID = finding.FindingID;
                correctiveAction.EmitterUserID = EmitterUserID;
                correctiveAction.ReviewerUserID = ReviewerUserID;
                correctiveAction.CreationDate = DateTime.Today;
                correctiveAction.Flow = "CorrectiveAction";
                correctiveAction.FlowVersion = 1;
                _workflowCore.StartFlow(correctiveAction);
            }
            else if (State == "Approve")
            {
                int newStateId = _findingStateRepository.GetOneByCode("APR");
                finding.FindingStateID = newStateId;
                _findingRepository.Update(finding);
            }

            //Delete Evidences
            foreach (var deleteEvidence in DeleteEvidencesUrls)
            {
                _findingEvidenceRepository.Delete(finding.FindingID, deleteEvidence);
            }

            //Add Evidences
            foreach (var newEvidence in NewEvidencesUrls)
            {
                _findingEvidenceRepository.Add(finding.FindingID, newEvidence);
            }

            //Left obtain Responsible SGC ID
            string IDRESPONSIBLESGC = finding.ResponsibleUserID;
            _findingStatesHistoryRepository.Add(finding.FindingID, finding.FindingStateID, IDRESPONSIBLESGC);

            finding = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, false);
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleSGC());
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailSectorBoss());
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailColaboratorSB());
            var email = _findingRepository.GetResponsibleUserEmail(finding.ResponsibleUserID);
            EmailAddresses.Add(email);

            this.EmailSubject = Data.EmailStrings.GetSubjectFinding(finding.FindingTypeName, "approve");
            this.EmailMessage = Data.EmailStrings.GetMessageFinding(finding, _emailSettings.Url, "approve");

            _hub.Clients.All.SendAsync("transferfindingsdata", finding);
            //Get Responsible User Email.
            return ExecutionResult.Next();
        }
    }
}
