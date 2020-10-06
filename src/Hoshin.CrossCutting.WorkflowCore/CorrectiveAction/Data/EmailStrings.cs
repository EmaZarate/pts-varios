using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data
{
    public class EmailStrings
    {
        public static string GetSubjectCorrectiveAction(string emailType)
        {
            string subject = "";
            switch (emailType)
            {
                case "new":
                    subject = "Nueva Acción Correctiva";
                    break;
                case "generate":
                    subject = "Acción Correctiva Planificada";
                    break;
                case "generatetask":
                    subject = "Tareas por AC";
                    break;
                case "finishedtasks":
                    subject = "Acción Correctiva Tratada";
                    break;
                case "reviewed":
                    subject = "Acción Correctiva Evaluada";
                    break;
                default:
                    subject = "";
                    break;
            }

            return ($"Hoshin Cloud - {subject}");
        }

        public static string GetMessageCorrectiveAction(CorrectiveActionWorkflowData correctiveAction, string frontendUrl, string emailType)
        {
            string url = $"{frontendUrl}/quality/corrective-actions/{correctiveAction.CorrectiveActionID}/detail";
            string title = "";

            switch (emailType)
            {
                case "new":
                    title = "Ha sido registrada una nueva acción correctiva.";
                    break;
                case "generate":
                    title = "Ha sido planificada una acción correctiva.";
                    break;
                case "generatetask":
                    title = "Se ha registrado una tarea de AC, que le fue asignada.";
                    break;
                case "finishedtasks":
                    title = "Se han completado las tareas de una acción correctiva, y la misma estará disponible para evaluación.";
                    break;
                case "reviewed":
                    title = "Ha sido evaluada una accíon correctiva.";
                    break;
                default:
                    title = "";
                    break;
            }

            string content = $"<p><b>Id de Acción Correctiva</b>: {correctiveAction.CorrectiveActionID}</p>";
            content += $"<p><b>Descripción</b>: {correctiveAction.Description}</p>";
            content += $"<p><b>Sector</b>: {correctiveAction.SectorTreatmentName}</p>";
            content += $"<p><b>Responsable Asignado</b>: {correctiveAction.ResponsibleUserFullName}</p>";
            content += $"<p><b>Evaluador Asignado</b>: {correctiveAction.ReviewerUserFullName}</p>";
            content += $"<p><b>Estado</b>: {correctiveAction.CorrectiveActionStateName}</p>";
            string evaluationResult = "";
            if (correctiveAction.isEffective)
            {
                evaluationResult = "Eficaz";
            }
            else
            {
                evaluationResult = "No Eficaz";
            }
            if (emailType == "new")
            {
                content += $"<p><b>Fecha de Vencimiento Planificación de AC</b>: {correctiveAction.DeadlineDatePlanification.ToString("dd/MM/yyyy")}</p>";
            }
            else if(emailType == "generate")
            {
                content += $"<p><b>Análisis de Causa</b>: {correctiveAction.RootReason}</p>";
                content += $"<p><b>Fecha probable de implementación total</b>: {correctiveAction.MaxDateImplementation.ToString("dd/MM/yyyy")}</p>";
            }
            else if(emailType == "finishedtasks")
            {
                content += $"<p><b>Análisis de Causa</b>: {correctiveAction.RootReason}</p>";
                content += $"<p><b>Fecha de implementación total</b>: {correctiveAction.EffectiveDateImplementation.ToString("dd/MM/yyyy")}</p>";
                content += $"<p><b>Fecha a partir de la cual estará disponible para evaluación</b>: {correctiveAction.MaxDateEfficiencyEvaluation.ToString("dd/MM/yyyy")}</p>";
                content += $"<p><b>Fecha de vencimiento para evaluación</b>: {correctiveAction.DeadlineDateEvaluation.ToString("dd/MM/yyyy")}</p>";
            }
            else if(emailType == "reviewed")
            {
                content += $"<p><b>Análisis de Causa</b>: {correctiveAction.RootReason}</p>";
                content += $"<p><b>Fecha de implementación total</b>: {correctiveAction.EffectiveDateImplementation.ToString("dd/MM/yyyy")}</p>";
                content += $"<p><b>Resultado de la evaluación</b>: {evaluationResult}</p>";
            }                                                      
            

            string bodyHtml = $"<html>" +
                                $"<body>" +
                                    $"<p>Estimado usuario: </p>" +
                                    $"<p>{title}</p>" +
                                    $"{content}" +
                                    $"<p>Puede acceder desde aquí: <a href={url}>Ver acción correctiva.</a></p>" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" +
                                $"</body>" +
                            $"</html>";

            return bodyHtml;
        }

        public static string GetMessageCorrectiveActionTasks(CorrectiveActionWorkflowData correctiveAction, List<TaskWorkflowData> tasks, string frontendUrl)
        {
            
            string title = $"Se han registrado tareas para la Acción Correctiva ID {correctiveAction.CorrectiveActionID}, una de las cuales le ha sido asignada.";

            string content = "";

            foreach (var task in tasks)
            {
                string url = $"{frontendUrl}/quality/tasks/{task.TaskID}/detail";

                content += $"<p><b>Id de tarea</b>: {task.TaskID}</p>";
                content += $"<p><b>Descripción</b>: {task.Description}</p>";
                content += $"<p><b>Responsable Asignado</b>: {task.ResponsibleUserFullName}</p>";
                content += $"<p><b>Fecha de Vencimiento</b>: {task.ImplementationPlannedDate.ToString("dd/MM/yyyy")}</p>";
                content += $"<p>Puede acceder desde aquí: <a href={url}>Ver tarea de acción correctiva.</a></p>";
                content += $"<p>---------------------------------------------------</p>";
            }

            string bodyHtml = $"<html>" +
                                $"<body>" +
                                    $"<p>Estimado usuario: </p>" +
                                    $"<p>{title}</p>" +
                                    $"<p>Detalle de las tareas: </p>" +
                                    $"{content}" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" +
                                $"</body>" +
                            $"</html>";

            return bodyHtml;
        }
    }
}
