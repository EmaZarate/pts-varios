using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.Finding.Steps
{
    public class NewFinding : StepBody
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IFindingStateRepository _findingStateRepository;
        private readonly IFindingStatesHistoryRepository _findingStatesHistoryRepository;
        private readonly IFindingEvidenceRepository _findingEvidenceRepository;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        private readonly EmailSettings _emailSettings;
        private IHubContext<FindingsHub> _hub;
        public NewFinding(IFindingRepository findingRepository, IFindingStatesHistoryRepository findingStatesHistoryRepository,
                          IFindingEvidenceRepository findingEvidenceRepository, IFindingStateRepository findingStateRepository,
                          IOptions<EmailSettings> emailSettings, IUserWorkflowRepository userWorkflowRepository,
                          IHubContext<FindingsHub> hub)
        {
            _findingRepository = findingRepository;
            _findingStatesHistoryRepository = findingStatesHistoryRepository;
            _findingEvidenceRepository = findingEvidenceRepository;
            _findingStateRepository = findingStateRepository;
            EmailAddresses = new List<string>();
            _emailSettings = emailSettings.Value;
            _hub = hub;
            _userWorkflowRepository = userWorkflowRepository;
        }
        public int FindingID { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserId { get; set; }
        public int PlantLocationId { get; set; }
        public int SectorLocationId { get; set; }
        public int PlantTreatmentId { get; set; }
        public int SectorTreatmentId { get; set; }
        public int FindingTypeId { get; set; }
        public int FindingStateId { get; set; }
        public string EmitterUserId { get; set; }
        public List<string> NewEvidencesUrls { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            if (context.PersistenceData == null && FindingID == 0)
            {
                //NEW FINDING
                FindingWorkflowData finding = new FindingWorkflowData(Description, PlantLocationId, SectorLocationId, PlantTreatmentId, SectorTreatmentId, FindingTypeId, EmitterUserId, ResponsibleUserId);
                finding.WorkflowId = context.Workflow.Id;
                finding.FindingStateID = _findingStateRepository.GetOneByCode("ESP");

                //Go to DB and save the Finding.
                FindingWorkflowData createdFinding = _findingRepository.Add(finding);

                //Add State to history
                _findingStatesHistoryRepository.Add(createdFinding.FindingID, createdFinding.FindingStateID, createdFinding.EmitterUserID);

                //Add Evidence
                foreach (var evidence in NewEvidencesUrls)
                {
                    _findingEvidenceRepository.Add(createdFinding.FindingID, evidence);
                }

                //Get the email of the Responsible User
                //EmailAddresses.Add(_findingRepository.GetResponsibleUserEmail(ResponsibleUserId));
                //GetType the email of the finding responsible
                EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleFinding());

                createdFinding = _findingRepository.GetOneByWorkflowId(createdFinding.WorkflowId);

                this.EmailSubject = EmailStrings.GetSubjectFinding(createdFinding.FindingTypeName, "new");
                this.EmailMessage = EmailStrings.GetMessageFinding(createdFinding, _emailSettings.Url, "new");
                
                _hub.Clients.All.SendAsync("transferfindingsdata", createdFinding);
                //_hub.Clients.All.SendCoreAsync("transferfindingsdata", createdFinding)
                return ExecutionResult.Next();
            }
            else
            {
                _findingRepository.setWorkflowID(FindingID, context.Workflow.Id);
                return ExecutionResult.Next();
            }
        }
    }
}