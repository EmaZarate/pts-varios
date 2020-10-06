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
    public class ScheduleAudit : StepBody
    {
        private readonly IAuditRepository _auditRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly IAuditStateRepository _auditStateRepository;
        private readonly EmailSettings _emailSettings;

        public ScheduleAudit(
            IAuditRepository auditRepository,
            ISectorPlantRepository sectorPlantRepository,
            IAuditStateRepository auditStateRepository,
            IOptions<EmailSettings> emailSettings)
        {
            EmailAddresses = new List<string>();
            _auditRepository = auditRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _auditStateRepository = auditStateRepository;
            _emailSettings = emailSettings.Value;
        }

        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public string AuditorID { get; set; }
        public string ExternalAuditor { get; set; }
        public DateTime AuditInitDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuditTypeID { get; set; }
        public List<int> AuditStandard { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            AuditWorkflowData auditWorkflowData = new AuditWorkflowData();
            auditWorkflowData.PlantID = PlantID;
            auditWorkflowData.SectorID = SectorID;
            auditWorkflowData.AuditorID = AuditorID;
            auditWorkflowData.ExternalAuditor = ExternalAuditor;
            auditWorkflowData.CreationDate = CreationDate;
            auditWorkflowData.AuditInitDate = AuditInitDate;
            auditWorkflowData.AuditTypeID = AuditTypeID;
            auditWorkflowData.AuditStandard = AuditStandard;
            auditWorkflowData.AuditStateID = _auditStateRepository.GetOneByCode("PRO");
            auditWorkflowData.WorkflowId = context.Workflow.Id;

            _auditRepository.Add(auditWorkflowData);

            //Responsible of sector-plant
            List<string> sectorPlantReferrents = _sectorPlantRepository.GetSectorPlantReferredEmail(PlantID, SectorID);
            EmailAddresses.AddRange(sectorPlantReferrents);
            
            //If auditor is internal, send email
            if(!string.IsNullOrEmpty(AuditorID))
            {
                EmailAddresses.Add(_auditRepository.GetAuditorEmail(AuditorID));
            }

            auditWorkflowData = _auditRepository.GetOneByWorkflowId(auditWorkflowData.WorkflowId);
            var emailType = "schedule";

            this.EmailSubject = EmailStrings.GetSubjectAudit(auditWorkflowData.AuditTypeName, emailType);
            this.EmailMessage = EmailStrings.GetMessageAudit(auditWorkflowData, _emailSettings.Url, emailType);

            return ExecutionResult.Next();
        }
    }
}
