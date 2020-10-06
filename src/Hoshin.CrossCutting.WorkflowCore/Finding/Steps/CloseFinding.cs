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
    public class CloseFinding : StepBody
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IFindingStateRepository _findingStateRepository;
        private readonly IFindingStatesHistoryRepository _findingStatesHistoryRepository;
        private readonly EmailSettings _emailSettings;
        private IHubContext<FindingsHub> _hub;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        public CloseFinding(
            IFindingRepository findingRepository,
            IFindingStateRepository findingStateRepository,
            IFindingStatesHistoryRepository findingStatesHistoryRepository,
            IOptions<EmailSettings> emailSettings,
            IHubContext<FindingsHub> hub,
            IUserWorkflowRepository userWorkflowRepository)
        {
            _findingRepository = findingRepository;
            _findingStateRepository = findingStateRepository;
            _findingStatesHistoryRepository = findingStatesHistoryRepository;
            EmailAddresses = new List<string>();
            _emailSettings = emailSettings.Value;
            _hub = hub;
            _userWorkflowRepository = userWorkflowRepository;
        }
        public string FinalComment { get; set; }
        public string State { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            FindingWorkflowData finding = _findingRepository.GetOneByWorkflowId(context.Workflow.Id);
            finding.FinalComment = this.FinalComment;
            if (State == "Finalizado OK")
            {
                finding.FindingStateID = _findingStateRepository.GetOneByCode("FOK");
            }
            else if (State == "Finalizado No OK")
            {
                finding.FindingStateID = _findingStateRepository.GetOneByCode("FNK");
            }
            else if (State == "Cerrado")
            {
                finding.FindingStateID = _findingStateRepository.GetOneByCode("CER");
            }

            _findingRepository.Update(finding);
            _findingStatesHistoryRepository.Add(finding.FindingID, finding.FindingStateID, finding.ResponsibleUserID);

            finding = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, false);
            // responsible
            EmailAddresses.Add(_findingRepository.GetResponsibleUserEmail(finding.ResponsibleUserID));
            //User creator
            EmailAddresses.Add(_findingRepository.GetResponsibleUserEmail(finding.EmitterUserID));
            // SGC
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleSGC());
            // Boss, colaborator
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailSectorBoss());
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailColaboratorSB());
            this.EmailSubject = EmailStrings.GetSubjectFinding(finding.FindingTypeName, "close");
            this.EmailMessage = EmailStrings.GetMessageFinding(finding, _emailSettings.Url, "close");

            _hub.Clients.All.SendAsync("transferfindingsdata", finding);
            return ExecutionResult.Next();
        }
    }
}