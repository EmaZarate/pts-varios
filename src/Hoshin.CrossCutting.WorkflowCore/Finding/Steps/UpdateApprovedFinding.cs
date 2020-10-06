using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.Finding.Steps
{
    public class UpdateApprovedFinding : StepBody
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IFindingStateRepository _findingStateRepository;
        private readonly IFindingStatesHistoryRepository _findingStatesHistoryRepository;
        private readonly IFindingEvidenceRepository _findingEvidenceRepository;
        private IHubContext<FindingsHub> _hub;
        public UpdateApprovedFinding(
            IFindingRepository findingRepository, 
            IFindingStateRepository findingStateRepository, 
            IFindingStatesHistoryRepository findingStatesHistoryRepository,
            IFindingEvidenceRepository findingEvidenceRepository,
            IHubContext<FindingsHub> hub
            )
        {
            _findingRepository = findingRepository;
            _findingStateRepository = findingStateRepository;
            _findingStatesHistoryRepository = findingStatesHistoryRepository;
            _findingEvidenceRepository = findingEvidenceRepository;
            _hub = hub;
        }
        public DateTime ExpirationDate { get; set; }
        public string ContainmentAction { get; set; }
        public string CauseAnalysis { get; set; }
        public string Comment { get; set; }
        public List<string> NewEvidencesUrls { get; set; }
        public List<string> DeleteEvidencesUrls { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            FindingWorkflowData finding = _findingRepository.GetOneByWorkflowId(context.Workflow.Id);

            finding.ExpirationDate = this.ExpirationDate;
            finding.ContainmentAction = this.ContainmentAction;
            finding.CauseAnalysis = this.CauseAnalysis;
            finding.Comment = this.Comment;

            int newStateId = _findingStateRepository.GetOneByCode("ENC");

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

            //Add to history if the first time edit
            if (finding.FindingStateID != newStateId)
            {
                _findingStatesHistoryRepository.Add(finding.FindingID, newStateId, finding.ResponsibleUserID);
            }

            finding.FindingStateID = newStateId;
            _findingRepository.Update(finding);
            finding = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, false);
            _hub.Clients.All.SendAsync("transferfindingsdata", finding);
            return ExecutionResult.Next();
        }
    }
}
