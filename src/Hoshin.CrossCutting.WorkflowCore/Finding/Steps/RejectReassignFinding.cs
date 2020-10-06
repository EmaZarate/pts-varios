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
    public class RejectReassignFinding : StepBody
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IReassignmentsFindingHistoryRepository _reassignmentsFindingHistoryRepository;
        private readonly IFindingStatesHistoryRepository _findingStatesHistoryRepository;
        private IHubContext<FindingsHub> _hub;

        public string ResponsibleUserId { get; set; }
        public string RejectComment { get; set; }
        public int FindingID { get; set; }
        public string ReassignedUserID { get; set; }
        public string EmailSubject { get; set; }
        private readonly EmailSettings _emailSettings;
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        public RejectReassignFinding(IFindingRepository findingRepository,
            IReassignmentsFindingHistoryRepository reassignmentsFindingHistoryRepository,
            IFindingStatesHistoryRepository findingStatesHistoryRepository,
            IHubContext<FindingsHub> hub,
            IOptions<EmailSettings> emailSettings
            )
        {
            _findingRepository = findingRepository;
            _reassignmentsFindingHistoryRepository = reassignmentsFindingHistoryRepository;
            _findingStatesHistoryRepository = findingStatesHistoryRepository;
            EmailAddresses = new List<string>();
            _emailSettings = emailSettings.Value;
            _hub = hub;

        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {

            var finding = _findingRepository.GetOneByWorkflowId(context.Workflow.Id);
            var newState = _findingStatesHistoryRepository.GetPreviousState(this.FindingID, finding.FindingStateID);
            finding.RejectComment = this.RejectComment;
            finding.FindingStateID = newState;
            _findingRepository.Update(finding);

            _reassignmentsFindingHistoryRepository.Add(this.FindingID, this.ReassignedUserID, this.ResponsibleUserId, "Rejected", this.RejectComment);

            var RESPONSIBLE_SGC_USER_ID = this.ResponsibleUserId;
            _findingStatesHistoryRepository.Add(this.FindingID, newState, RESPONSIBLE_SGC_USER_ID);

            EmailAddresses.Add(_findingRepository.GetResponsibleUserEmail(finding.ResponsibleUserID));

            this.EmailSubject = EmailStrings.GetSubjectFinding(finding.FindingTypeName, "rejectreassignment");
            this.EmailMessage = EmailStrings.GetMessageFinding(finding, _emailSettings.Url, "rejectreassignment");
            //Left email of SGC
            finding = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, false);
            _hub.Clients.All.SendAsync("transferfindingsdata", finding);
            return ExecutionResult.Next();
        }
    }
}
