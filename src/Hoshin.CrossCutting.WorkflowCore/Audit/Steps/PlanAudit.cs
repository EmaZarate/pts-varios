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
    public class PlanAudit : StepBody
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IAuditStateRepository _auditStateRepository;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly EmailSettings _emailSettings;
        private readonly IUserWorkflowRepository _userWorkflowRepository;

        public PlanAudit(IAuditRepository auditRepository,
            IAuditStateRepository auditStateRepository,
            ISectorPlantRepository sectorPlantRepository,
            IOptions<EmailSettings> emailSettings,
            IUserWorkflowRepository userWorkflowRepository)
        {
            EmailAddresses = new List<string>();
            _auditRepository = auditRepository;
            _auditStateRepository = auditStateRepository;
            _sectorPlantRepository = sectorPlantRepository;
            _emailSettings = emailSettings.Value;
            _userWorkflowRepository = userWorkflowRepository;
        }


        public string AuditTeam { get; set; }
        public List<AuditStandardAspects> AuditStandardAspects { get; set; }
        public DateTime AuditInitDate { get; set; }
        public DateTime AuditInitTime { get; set; }
        public DateTime AuditFinishDate { get; set; }
        public DateTime AuditFinishTime { get; set; }
        public DateTime DocumentsAnalysisDate { get; set; }
        public int DocumentAnalysisDuration { get; set; }
        public DateTime ReportEmittedDate { get; set; }
        public DateTime CloseMeetingDate { get; set; }
        public int CloseMeetingDuration { get; set; }
        public DateTime CloseDate { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            AuditWorkflowData auditData = _auditRepository.GetOneByWorkflowId(context.Workflow.Id);
            auditData.AuditTeam = AuditTeam;
            auditData.AuditInitDate = AuditInitDate;
            auditData.AuditInitTime = AuditInitTime;
            auditData.AuditFinishDate = AuditFinishDate;
            auditData.AuditFinishTime = AuditFinishTime;
            auditData.DocumentsAnalysisDate = DocumentsAnalysisDate;
            auditData.DocumentAnalysisDuration = DocumentAnalysisDuration;
            auditData.ReportEmittedDate = ReportEmittedDate;
            auditData.CloseMeetingDate = CloseMeetingDate;
            auditData.CloseMeetingDuration = CloseMeetingDuration;
            auditData.CloseDate = CloseDate;
            auditData.AuditStateID = _auditStateRepository.GetOneByCode("PPA");
            _auditRepository.PlanAudit(auditData, AuditStandardAspects);
           

            List<string> sectorPlantReferrents = _sectorPlantRepository.GetSectorPlantReferredEmail(auditData.PlantID, auditData.SectorID);
            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleSGC());
            
            //EmailAddresses.AddRange(sectorPlantReferrents);

            //If auditor is internal, send email
            //if (!string.IsNullOrEmpty(auditData.AuditorID))
            //{
            //    EmailAddresses.Add(_auditRepository.GetAuditorEmail(auditData.AuditorID));
            //}

            //Left responsible of SGC 

            auditData = _auditRepository.GetOneByWorkflowId(auditData.WorkflowId);
            var emailType = "plan";

            this.EmailSubject = EmailStrings.GetSubjectAudit(auditData.AuditTypeName, emailType);
            this.EmailMessage = EmailStrings.GetMessageAudit(auditData, _emailSettings.Url, emailType);

            return ExecutionResult.Next();
        }
    }
}
