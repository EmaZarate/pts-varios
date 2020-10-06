using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
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
    public class GenerateFindingReassignment : StepBody
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IReassignmentsFindingHistoryRepository _reassignmentsFindingHistoryRepository;
        private readonly IFindingStatesHistoryRepository _findingStatesHistoryRepository;
        private readonly IFindingStateRepository _findingStateRepository;
        private readonly EmailSettings _emailSettings;
        private IHubContext<FindingsHub> _hub;
        private IUserWorkflowRepository _userWorkflowRepository;

        public string ResponsibleUserId { get; set; }
        public int FindingID { get; set; }
        public string ReassignedUserID { get; set; }
        public int PlantTreatmentID { get; set; }
        public int SectorTreatmentID { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        public GenerateFindingReassignment(IFindingRepository findingRepository,
            IReassignmentsFindingHistoryRepository reassignmentsFindingHistoryRepository,
            IFindingStatesHistoryRepository findingStatesHistoryRepository,
            IFindingStateRepository findingStateRepository,
            IOptions<EmailSettings> emailSettings,
            IHubContext<FindingsHub> hub,
            IUserWorkflowRepository userWorkflowRepository)
        {
            _findingStateRepository = findingStateRepository;
            _findingRepository = findingRepository;
            _reassignmentsFindingHistoryRepository = reassignmentsFindingHistoryRepository;
            _findingStatesHistoryRepository = findingStatesHistoryRepository;
            EmailAddresses = new List<string>();
            _emailSettings = emailSettings.Value;
            _hub = hub;
            _userWorkflowRepository = userWorkflowRepository;
        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            int newFindingStateId;
            var finding = _findingRepository.GetOneByWorkflowId(context.Workflow.Id);
            if (finding.FindingStateID == _findingStateRepository.GetOneByCode("PDR"))
            {
                //ReassignWithApprovalStep
                newFindingStateId = _findingStatesHistoryRepository.GetPreviousState(this.FindingID, finding.FindingStateID);
            }
            else
            {
                //ReassignWithoutApprovalStep
                newFindingStateId = finding.FindingStateID;
            }
            //last responsable user mail
            EmailAddresses.Add(_findingRepository.GetResponsibleUserEmail(this.ResponsibleUserId));
            finding.ResponsibleUserID = this.ReassignedUserID;
            finding.FindingStateID = newFindingStateId;
            finding.PlantTreatmentID = this.PlantTreatmentID;
            finding.SectorTreatmentID = this.SectorTreatmentID;
            _findingRepository.Update(finding);

            //Left get SGC Responsible user id
            string SGC_RESPONSIBLE_USER_ID = this.ResponsibleUserId;
            var result = _reassignmentsFindingHistoryRepository.Add(this.FindingID, this.ReassignedUserID, SGC_RESPONSIBLE_USER_ID, "Approve");

            _findingStatesHistoryRepository.Add(this.FindingID, newFindingStateId, SGC_RESPONSIBLE_USER_ID);

            string oldResponsibleUser = finding.ResponsibleUserFullName;
            finding = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, false);
            var email = _findingRepository.GetResponsibleUserEmail(this.ReassignedUserID);
            EmailAddresses.Add(email);
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleFinding());
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailColaboratorSB());
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailSectorBoss());
            this.EmailSubject = EmailStrings.GetSubjectFinding(finding.FindingTypeName, "reassign");
            this.EmailMessage = EmailStrings.GetMessageFinding(finding, _emailSettings.Url, "reassign", oldResponsibleUser);

            _hub.Clients.All.SendAsync("transferfindingsdata", finding);
            return ExecutionResult.Next();
        }
    }
}
