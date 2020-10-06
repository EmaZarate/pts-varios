using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Audit.Data
{
    public class EmailStrings
    {
        public static string GetSubjectAudit(string tipoAuditoria, string emailType)
        {
            string subject = "";
            switch (emailType)
            {
                case "schedule":
                    subject = "Nueva Auditoría Programada";
                    break;
                case "replan":
                    subject = "Reprogramación de Auditoría";
                    break;
                case "plan":
                    subject = "Auditoría Planificada";
                    break;  
                case "approveplan":
                    subject = "Planificación de Auditoría Aprobada";
                    break;
                case "rejectplan":
                    subject = "Planificación de Auditoria Rechazada";
                    break;
                case "emmitreport":
                    subject = "Informe de Auditoría";
                    break;
                case "close":
                    subject = "Informe de Auditoría Aprobado";
                    break;
                case "rejectreport":
                    subject = "Informe de Auditoría Rechazado";
                    break;
                default:
                    subject = "";
                    break;
            }

            return ($"Hoshin Cloud - {subject} - {tipoAuditoria}");
        }

        public static string GetMessageAudit(AuditWorkflowData audit, string frontendUrl, string emailType)
        {
            string url = $"{frontendUrl}/quality/audits/{audit.AuditID}/detail";
            string title = "";

            switch (emailType)
            {
                case "schedule":
                    title = "Ha sido programada una nueva auditoría.";
                    break;
                case "replan":
                    title = "Ha sido reprogramada una auditoría.";
                    break;
                case "plan":
                    title = "Ha sido planificada una auditoría y requiere aprobación.";
                    break;
                case "approveplan":
                    title = "Ha sido aprobada la planificación de una auditoría.";
                    break;
                case "rejectplan":
                    title = "Ha sido rechazada la planificación de una auditoría.";
                    break;
                case "emmitreport":
                    title = "Ha sido emitido el informe de una auditoría y requiere aprobación.";
                    break;
                case "close":
                    title = "Ha sido aprobado el informe de una auditoría.";
                    break;
                case "rejectreport":
                    title = "Ha sido rechazado el informe de una auditoría.";
                    break;
                default:
                    title = "";
                    break;
            }

            var normasRows = "";
            foreach(var name in audit.AuditStandards.Select(x => x.StandardName))
            {
                normasRows += $"<tr><td>{name}</td></tr>";
            }
                
            var auditorFullName = (audit.AuditorID == null ? audit.ExternalAuditor : audit.AuditorFullName);

            string content = $"<p><b>Id de Auditoría</b>: {audit.AuditID}</p>";
            content += $"<p><b>Tipo de Auditoría</b>: {audit.AuditTypeName}</p>";
            content += $"<p><b>Normas</b>: <table>{normasRows}</table></p>";
            content += $"<p><b>Sector</b>: {audit.SectorName}</p>";
            content += $"<p><b>Auditor Asignado</b>: {auditorFullName}</p>";
            content += $"<p><b>Fecha de Inicio</b>: {audit.AuditInitDate.ToString("dd/MM/yyyy")}</p>";
            if (emailType == "rejectplan" || emailType == "rejectreport")
                content += $"<p><b>Causa del Rechazo</b>: {(emailType == "rejectplan" ? audit.ApprovePlanComments : audit.ApproveReportComments)}</p>";

            string bodyHtml = $"<html>" +
                                $"<body>" +
                                    $"<p>Estimado usuario: </p>" +
                                    $"<p>{title}</p>" +
                                    $"{content}" +
                                    $"<p>Puede acceder desde aquí: <a href={url}>Ver auditoría.</a></p>" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" +
                                $"</body>" +
                            $"</html>";

            return bodyHtml;
        }
    }
}
