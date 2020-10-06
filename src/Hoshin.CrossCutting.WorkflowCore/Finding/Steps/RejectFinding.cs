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
    public class RejectFinding : StepBody
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IFindingStateRepository _findingStateRepository;
        private readonly IFindingStatesHistoryRepository _findingStatesHistoryRepository;
        private readonly IFindingEvidenceRepository _findingEvidenceRepository;
        private readonly EmailSettings _emailSettings;
        private IHubContext<FindingsHub> _hub;

        public RejectFinding(
            IFindingRepository findingRepository,
            IFindingStateRepository findingStateRepository,
            IFindingStatesHistoryRepository findingStatesHistoryRepository,
            IFindingEvidenceRepository findingEvidenceRepository,
            IOptions<EmailSettings> emailSettings,
            IHubContext<FindingsHub> hub
        )
        {
            _findingRepository = findingRepository;
            _findingStateRepository = findingStateRepository;
            _findingStatesHistoryRepository = findingStatesHistoryRepository;
            _findingEvidenceRepository = findingEvidenceRepository;
            EmailAddresses = new List<string>();
            _emailSettings = emailSettings.Value;
            _hub = hub;
        }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        public string FinalComment { get; set; }
        public List<string> NewEvidencesUrls { get; set; }
        public List<string> DeleteEvidencesUrls { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            FindingWorkflowData finding = _findingRepository.GetOneByWorkflowId(context.Workflow.Id);
            finding.FinalComment = this.FinalComment;
            int newStateId = _findingStateRepository.GetOneByCode("RCZ");
            finding.FindingStateID = newStateId;
            _findingRepository.Update(finding);

            ////Delete Evidences
            //foreach (var deleteEvidence in DeleteEvidencesUrls)
            //{
            //    _findingEvidenceRepository.Delete(finding.FindingID, deleteEvidence);
            //}

            ////Add Evidences
            //foreach (var newEvidence in NewEvidencesUrls)
            //{
            //    _findingEvidenceRepository.Add(finding.FindingID, newEvidence);
            //}

            //WE NEED GET THE RESPONSIBLESGCUSERID
            var RESPONSIBLESGCUSERID = finding.ResponsibleUserID;
            _findingStatesHistoryRepository.Add(finding.FindingID, finding.FindingStateID, RESPONSIBLESGCUSERID);

            finding = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, false);
            //usuario creador de hallazgo
            EmailAddresses.Add(_findingRepository.GetResponsibleUserEmail(finding.EmitterUserID));
            this.EmailSubject = EmailStrings.GetSubjectFinding(finding.FindingTypeName, "reject");
            this.EmailMessage = EmailStrings.GetMessageFinding(finding, _emailSettings.Url, "reject");

            _hub.Clients.All.SendAsync("transferfindingsdata", finding);
            return ExecutionResult.Next();
        }
    }
}
