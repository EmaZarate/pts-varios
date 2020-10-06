using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.SignalR;
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
    public class ReassignFinding : StepBody
    {
        public string ResponsibleUserId { get; set; }
        public int FindingID { get; set; }
        public string ReassignedUserID { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        private readonly IReassignmentsFindingHistoryRepository _reassignmentsFindingHistoryRepository;
        private readonly IFindingRepository _findingRepository;
        private readonly IFindingStateRepository _findingStateRepository;
        private readonly IFindingStatesHistoryRepository _findingStatesHistoryRepository;
        private IHubContext<FindingsHub> _hub;
        private readonly EmailSettings _emailSettings;
        private readonly IUserWorkflowRepository _userWorkflowRepository;

        public ReassignFinding(IReassignmentsFindingHistoryRepository reassignmentsFindingHistoryRepository,
            IFindingRepository findingRepository,
            IFindingStateRepository findingStateRepository,
            IFindingStatesHistoryRepository findingStatesHistoryRepository,
            IHubContext<FindingsHub> hub,
            IUserWorkflowRepository userWorkflowRepository,
            IOptions<EmailSettings> emailSettings
            )
        {
            EmailAddresses = new List<string>();
            _reassignmentsFindingHistoryRepository = reassignmentsFindingHistoryRepository;
            _findingRepository = findingRepository;
            _findingStateRepository = findingStateRepository;
            _findingStatesHistoryRepository = findingStatesHistoryRepository;
            _hub = hub;
            _userWorkflowRepository = userWorkflowRepository;
            _emailSettings = emailSettings.Value;

        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            int newStateId = _findingStateRepository.GetOneByCode("PDR");
            var finding = _findingRepository.GetOneByWorkflowId(context.Workflow.Id);

            finding.FindingStateID = newStateId;

            _findingRepository.Update(finding);

            _findingStatesHistoryRepository.Add(this.FindingID, newStateId, this.ResponsibleUserId);
            //I Need te Emitter User for reassignment, New responsible User and SGC Responsible To send email
            //REASSIGN FINDING
            var result = _reassignmentsFindingHistoryRepository.Add(this.FindingID, this.ReassignedUserID, this.ResponsibleUserId, "Pendiente");

            if (result)
            {
                //Get emails to notify

                //email of request reassignment user
                var email = _userWorkflowRepository.GetUsersEmailResponsibleFinding();
                EmailAddresses.AddRange(email);
                string reasignedUserName = _userWorkflowRepository.GetFullName(this.ReassignedUserID);
                this.EmailSubject = Data.EmailStrings.GetSubjectFinding(finding.FindingTypeName, "generatereassignment");
                this.EmailMessage = Data.EmailStrings.GetMessageFinding(finding, _emailSettings.Url, "generatereassignment", reasignedUserName);
            }
            finding = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, false);
            _hub.Clients.All.SendAsync("transferfindingsdata", finding);
            return ExecutionResult.Next();
        }
    }
}