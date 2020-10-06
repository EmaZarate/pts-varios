using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.Audit.Steps
{
    public class RejectReportAudit: StepBody
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IAuditStateRepository _auditStateRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        private readonly EmailSettings _emailSettings;

        public string ApproveReportComments { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }

        public RejectReportAudit(IAuditRepository auditRepository,
            IAuditStateRepository auditStateRepository,
            ISectorPlantRepository sectorPlantRepository,
            IUserWorkflowRepository userWorkflowRepository,
            IOptions<EmailSettings> emailSettings)
        {
            EmailAddresses = new List<string>();
            _auditRepository = auditRepository;
            _auditStateRepository = auditStateRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _userWorkflowRepository = userWorkflowRepository;
            _emailSettings = emailSettings.Value;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            AuditWorkflowData auditData = _auditRepository.GetOneByWorkflowId(context.Workflow.Id);
            auditData.ApproveReportComments = ApproveReportComments;
            auditData.AuditStateID = _auditStateRepository.GetOneByCode("IRZ");

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
            var emailType = "rejectreport";

            this.EmailSubject = EmailStrings.GetSubjectAudit(auditData.AuditTypeName, emailType);
            this.EmailMessage = EmailStrings.GetMessageAudit(auditData, _emailSettings.Url, emailType);

            return ExecutionResult.Next();
        }
    }
}
