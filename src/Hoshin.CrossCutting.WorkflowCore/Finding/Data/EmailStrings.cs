using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Finding.Data
{
    public class EmailStrings
    {
        public static string GetSubjectFinding(string tipoHallazgo, string emailType)
        {
            string subject = "";
            switch (emailType)
            {
                case "new":
                    subject = "Registro de Hallazgo";
                    break;
                case "approve":
                    subject = "Hallazgo Aprobado";
                    break;
                case "close":
                    subject = "Hallazgo Finalizado";
                    break;
                case "reject":
                    subject = "Hallazgo Rechazado";
                    break;
                case "generatereassignment":
                    subject = "Solicitud de Reasignación de Hallazgo";
                    break;
                case "approvereassignment":
                    subject = "Aprobación de Reasignación de Hallazgo";
                    break;
                case "reassign":
                    subject = "Reasignación de Hallazgo";
                    break;
                case "rejectreassignment":
                    subject = "Reasignación de Hallazgo rechazada";
                    break;
                case "abouttoexpire":
                    subject = "Hallazgo próximo a vencer";
                    break;
                case "expired":
                    subject = "Hallazgo Vencido";
                    break;
                default:
                    subject = "";
                    break;
            }

            return ($"Hoshin Cloud - {subject} - {tipoHallazgo}");
        }

        public static string GetMessageFinding(FindingWorkflowData finding, string frontendUrl, string emailType, string responsibleReasignedSolicited = "")
        {
            string url = $"{frontendUrl}/quality/finding/{finding.FindingID}/detail";
            string title = "";

            switch (emailType)
            {
                case "new":
                    title = "Ha sido registrado un nuevo hallazgo, que requiere aprobación.";
                    break;
                case "approve":
                    title = "Ha sido aprobado un hallazgo.";
                    break;
                case "close":
                    title = "Ha finalizado el tratamiento de un hallazgo.";
                    break;
                case "reject":
                    title = "Ha sido rechazado un hallazgo.";
                    break;
                case "generatereassignment":
                    title = "Se ha solicitado la reasignación de un hallazgo, que requiere aprobación.";
                    break;
                case "approvereassignment":
                    title = "Ha sido aprobada la reasignación de un hallazgo.";
                    break;
                case "reassign":
                    title = "Se realizó la reasignación de un hallazgo.";
                    break;
                case "rejectreassignment":
                    title = "Ha sido rechazada la reasignación de un hallazgo.";
                    break;
                case "abouttoexpire":
                    title = "El siguiente hallazgo está próximo a vencer.";
                    break;
                case "expired":
                    title = "Ha vencido un hallazgo, y no se completó su tratamiento.";
                    break;
                default:
                    title = "";
                    break;
            }

            string content = $"<p><b>Id de Hallazgo</b>: {finding.FindingID}</p>";
            content += $"<p><b>Tipo de Hallazgo</b>: {finding.FindingTypeName}</p>";
            content += $"<p><b>Descripción</b>: {finding.Description}</p>";
            content += $"<p><b>Sector Ubicación</b>: {finding.SectorPlantLocationSectorName}</p>";
            content += $"<p><b>Sector Tratamiento</b>: {finding.SectorPlantTreatmentSectorName}</p>";
            if (emailType == "reassign")
                content += $"<p><b>Responsable asignado anterior</b>: {responsibleReasignedSolicited}</p>";
            if(emailType == "reassign")
            {
                content += $"<p><b>Responsable asignado actual</b>: {finding.ResponsibleUserFullName} </p>";
            }
            else
            {
                content += $"<p><b>Responsable Asignado</b>: {finding.ResponsibleUserFullName}</p>";
            }
            if (emailType == "generatereassignment")
                content += $"<p><b>Responsable asignado solicitado</b>: {responsibleReasignedSolicited}</p>";
            content += $"<p><b>Estado del Hallazgo</b>: {finding.FindingStateName}</p>";
            if(emailType == "reject" || emailType == "rejectreassignment")
                content += $"<p><b>Causa del Rechazo</b>: {finding.RejectComment}</p>";
            if(emailType == "close")
            {
                content += $"<p><b>Acción de Contención</b>: {finding.ContainmentAction}</p>";
                content += $"<p><b>Análisis de Causa</b>: {finding.CauseAnalysis}</p>";
                content += $"<p><b>Comentarios Finales</b>: {finding.FinalComment}</p>";
            }
            
            string bodyHtml = $"<html>" +
                                $"<body>" +
                                    $"<p>Estimado usuario: </p>" +
                                    $"<p>{title}</p>" +
                                    $"{content}" +
                                    $"<p>Puede acceder desde aquí: <a href={url}>Ver hallazgo.</a></p>" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" +
                                $"</body>" +
                            $"</html>";

            return bodyHtml;
        }
    }
}