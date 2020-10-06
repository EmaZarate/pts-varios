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
    public class ApprovePlanAudit : StepBody
    {
        public string ApprovePlanComments { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }

        private readonly IAuditStateRepository _auditStateRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly EmailSettings _emailSettings;
        private readonly IUserWorkflowRepository _userWorkflowRepository;

        public ApprovePlanAudit(
            IAuditStateRepository auditStateRepository,
            IAuditRepository auditRepository,
            ISectorPlantRepository sectorPlantRepository,
            IOptions<EmailSettings> emailSettings,
            IUserWorkflowRepository userWorkflowRepository)
        {
            EmailAddresses = new List<string>();
            _auditRepository = auditRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _auditStateRepository = auditStateRepository;
            _emailSettings = emailSettings.Value;
            _userWorkflowRepository = userWorkflowRepository;
        }
          

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            AuditWorkflowData auditWorkflowData = _auditRepository.GetOneByWorkflowId(context.Workflow.Id);
            auditWorkflowData.AuditStateID = _auditStateRepository.GetOneByCode("PLA");
            auditWorkflowData.ApprovePlanComments = ApprovePlanComments;

            _auditRepository.ApproveOrRejectAuditPlan(auditWorkflowData);

            List<string> sectorPlantReferrents = _sectorPlantRepository.GetSectorPlantReferredEmail(auditWorkflowData.PlantID, auditWorkflowData.SectorID);
            EmailAddresses.AddRange(sectorPlantReferrents);
            
            //If auditor is internal, send email

            if (!string.IsNullOrEmpty(auditWorkflowData.AuditorID))
            {
                EmailAddresses.Add(_auditRepository.GetAuditorEmail(auditWorkflowData.AuditorID));
            }

            //Left responsible of SGC 

            auditWorkflowData = _auditRepository.GetOneByWorkflowId(auditWorkflowData.WorkflowId);
            var emailType = "approveplan";

            this.EmailSubject = EmailStrings.GetSubjectAudit(auditWorkflowData.AuditTypeName, emailType);
            this.EmailMessage = EmailStrings.GetMessageAudit(auditWorkflowData, _emailSettings.Url, emailType);

            return ExecutionResult.Next();
        }
    }
}
