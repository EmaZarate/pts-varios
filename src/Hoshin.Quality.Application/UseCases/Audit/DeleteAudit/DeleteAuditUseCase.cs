using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Message.Interfaces;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Application.Exceptions.Audit;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain;
using Hoshin.Quality.Domain.CorrectiveAction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Audit.DeleteAudit
{
    public class DeleteAuditUseCase : IDeleteAuditUseCase
    {
        private List<string> ALLOWED_CODES = new List<string>() {
            "PRO",
            "PPA",
            "PRZ",
            "PLA"
        };
        private readonly IWorkflowCore _workflowCore;
        private readonly IAuditRepository _auditRepository;
        private readonly IEmailSender _emailSender;
        private readonly ISectorPlantRepository _sectorPlantRepository;

        public DeleteAuditUseCase(IWorkflowCore workflowCore, IAuditRepository auditRepository, IEmailSender emailSender, ISectorPlantRepository sectorPlantRepository)
        {
            _workflowCore = workflowCore;
            _auditRepository = auditRepository;
            _emailSender = emailSender;
            _sectorPlantRepository = sectorPlantRepository;
        }
        public async Task<bool> Execute(int auditId, string workflowId)
        {
            Domain.Audit.Audit audit = _auditRepository.Get(auditId);
            List<string> EmailsToNotify = new List<string>();

            EmailsToNotify.AddRange(_sectorPlantRepository.GetSectorPlantReferredEmail(audit.PlantID, audit.SectorID));

            //Internal Auditr
            if (audit.Auditor != null)
            {
                EmailsToNotify.Add(audit.Auditor.Email);
            }
            //LEFT EMAIL OF SGC RESPONSIBLE

            if (ALLOWED_CODES.Contains(audit.AuditState.Code))
            {
                try
                {
                    string standards = "";
                    foreach (AuditStandard normas in audit.AuditStandards)
                    {
                        standards += $"<tr><td>{normas.Standard.Name}</td></tr>";

                    }
                    var emailSubject = $"HoshinCloud - Auditoría eliminada - {audit.AuditTypeName}";

                    string content = $"<p><b>Id de Auditoría</b>: {audit.AuditID}</p>";
                    content += $"<p><b>Tipo de Auditoría</b>: {audit.AuditTypeName}</p>";
                    content += $"<p><b>Normas</b>: <table>{standards}</table></p>";
                    content += $"<p><b>Sector</b>: {audit.SectorPlantName}</p>";
                    content += $"<p><b>Auditor Asignado</b>: {audit.AuditorName}</p>";
                    content += $"<p><b>Fecha de Inicio</b>: {audit.AuditInitDate.ToString("dd/MM/yyyy")}</p>";

                    string bodyHtml = $"<html>" +
                                        $"<body>" +
                                            $"<p>Estimado usuario: </p>" +
                                            $"<p>Ha sido reprogramada una auditoría.</p>" +
                                            $"{content}" +
                                            $"<p></p>" +
                                            $"<p>Saludos cordiales.</p>" +
                                        $"</body>" +
                                    $"</html>";

                    await _workflowCore.DeleteWorkflowInstance(workflowId);
                    _auditRepository.Delete(auditId);
                    Task.Run(() => _emailSender.SendEmailAsync(EmailsToNotify.ToArray(), new List<string>().ToArray(), new List<string>().ToArray(), emailSubject, bodyHtml, true, System.Net.Mail.MailPriority.High));
                    return true;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                throw new InvalidAuditStateException("STATE: " + audit.AuditState.Code);
            }
        }
    }
}
