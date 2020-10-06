using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.Audit.Steps
{
    public class CloseAudit: StepBody
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IAuditStateRepository _auditStateRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        private readonly IFindingRepository _findingRepository;
        private readonly IWorkflowCore _workflowCore;
        private readonly EmailSettings _emailSettings;

        public string ApproveReportComments { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }

        public CloseAudit(
            IAuditRepository auditRepository,
            IAuditStateRepository auditStateRepository,
            ISectorPlantRepository sectorPlantRepository,
            IUserWorkflowRepository userWorkflowRepository,
            IFindingRepository findingRepository,
            IWorkflowCore workflowCore,
            IOptions<EmailSettings> emailSettings)
        {
            EmailAddresses = new List<string>();
            _auditRepository = auditRepository;
            _auditStateRepository = auditStateRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _userWorkflowRepository = userWorkflowRepository;
            _findingRepository = findingRepository;
            _workflowCore = workflowCore;
            _emailSettings = emailSettings.Value;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            AuditWorkflowData auditData = _auditRepository.GetOneByWorkflowId(context.Workflow.Id);
            auditData.ApproveReportComments = ApproveReportComments;
            auditData.AuditStateID = _auditStateRepository.GetOneByCode("IAP");

            List<string> sectorPlantReferrents = _sectorPlantRepository.GetSectorPlantReferredEmail(auditData.PlantID, auditData.SectorID);
            EmailAddresses.AddRange(sectorPlantReferrents);
            _auditRepository.ApproveOrRejectReportAudit(auditData);
            //If auditor is internal, send email
            if (!string.IsNullOrEmpty(auditData.AuditorID))
            {
                EmailAddresses.Add(_auditRepository.GetAuditorEmail(auditData.AuditorID));
            }

            //Add responsible of SGC 
            List<string> responsibleSGC = _userWorkflowRepository.GetUsersEmailResponsibleSGC();
            EmailAddresses.AddRange(responsibleSGC);

            auditData = _auditRepository.GetOneByWorkflowId(auditData.WorkflowId);
            var emailType = "close";

            this.EmailSubject = EmailStrings.GetSubjectAudit(auditData.AuditTypeName, emailType);
            this.EmailMessage = EmailStrings.GetMessageAudit(auditData, _emailSettings.Url, emailType);

            var findingsWorkFlow = _findingRepository.GetAllByAuditID(auditData.AuditID);

            foreach (var finding in findingsWorkFlow)
            {
                finding.Flow = "Finding";
                finding.FlowVersion = 1;
                _workflowCore.StartFlow(finding);
            }

           // _auditStandardAspectRepository.
            return ExecutionResult.Next();
        }
    }
}
