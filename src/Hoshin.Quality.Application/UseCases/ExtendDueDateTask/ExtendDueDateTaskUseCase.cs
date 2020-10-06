using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Hoshin.CrossCutting.Message;
using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Message.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Hoshin.Quality.Application.UseCases.ExtendDueDateTask
{
    public class ExtendDueDateTaskUseCase : IExtendDueDateTaskUseCase
    {
        private readonly IMapper _mapper;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICorrectiveActionStatesHistoryRepository _correctiveActionStatesHistoryRepository;
        private readonly IEmailSender _emailSender;
        private readonly EmailSettings _emailSettings;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskStateRepository _taskStateRepository;
        private readonly IUserRepository _userRepository;
        public MailPriority MailPriority { get; set; }
        public ExtendDueDateTaskUseCase(
            IMapper mapper,
            ICorrectiveActionRepository correctiveActionRepository,
            ICorrectiveActionStateRepository correctiveActionStateRepository,
            IHttpContextAccessor httpContextAccessor,
            ICorrectiveActionStatesHistoryRepository correctiveActionStatesHistoryRepository,
            IEmailSender emailSender,
            ITaskRepository taskRepository,
            ITaskStateRepository taskStateRepository,
            IUserRepository userRepository,
            IOptions<EmailSettings> emailSettings
            )
        {
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _correctiveActionRepository = correctiveActionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _correctiveActionStatesHistoryRepository = correctiveActionStatesHistoryRepository;
            _emailSender = emailSender;
            _taskRepository = taskRepository;
            _taskStateRepository = taskStateRepository;
            _userRepository = userRepository;
            _emailSettings = emailSettings.Value;

        }
        public bool Execute(Task task)
        {
            var emailAddress = new List<string>();
            var ccEmailAddresses = new List<string>();
            var bccEmailAddresses = new List<string>();
            var emailSubject = "Extensión fecha de vencimiento de tarea de AC.";
            var userID = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var correctiveAction = _correctiveActionRepository.GetOne(task.EntityID);
            if(task.ImplementationPlannedDate > correctiveAction.MaxDateImplementation)
            {
                var previousMaxdateImplementation = correctiveAction.MaxDateImplementation;
                correctiveAction.MaxDateImplementation = Convert.ToDateTime(task.ImplementationPlannedDate);

                int correctiveActionStateID = _correctiveActionStateRepository.GetByCode("PLN");
                correctiveAction.CorrectiveActionStateID = correctiveActionStateID;
                _correctiveActionStatesHistoryRepository.Add(correctiveAction.CorrectiveActionID, correctiveAction.CorrectiveActionStateID, userID);
                double days = (correctiveAction.MaxDateImplementation - previousMaxdateImplementation).TotalDays;
                correctiveAction.MaxDateEfficiencyEvaluation = correctiveAction.MaxDateEfficiencyEvaluation.AddDays(days);
                correctiveAction.DeadlineDateEvaluation = correctiveAction.DeadlineDateEvaluation.AddDays(days);
                _correctiveActionRepository.Update(correctiveAction);
                emailAddress.Add(correctiveAction.ResponisbleUser.Email);
            }
            int taskStateID = _taskStateRepository.GetIdByCode("ECU");
            task.TaskStateID = taskStateID;
            _taskRepository.Update(task);
            var userTask = _userRepository.Get(task.ResponsibleUserID);
            emailAddress.Add(userTask.Email);
            var url = $"{_emailSettings.Url}/quality/corrective-actions/{correctiveAction.CorrectiveActionID}/detail";
            var emailMessage = $"<html>" +
                                $"<body>" +
                                    $"<p>Estimado Usuario: </p>" +
                                    $"<p>Se ha extendido la fecha de vencimiento de una tarea de AC: </p>" +

                                    $"<p><b>Id tarea: </b>{task.TaskID}.</p>" +
                                    $"<p><b>Id de Acción Correctiva: </b>{correctiveAction.CorrectiveActionID}.</p>" +
                                    $"<p><b>Descripción de la tarea: </b>{task.Description}.</p>" +
                                    $"<p><b>Responsable asignado a la tarea: </b>{userTask.FullName}.</p>" +
                                    $"<p><b>Nueva fecha vencimiento de la tarea: </b>{task.ImplementationPlannedDate.ToString("dd/MM/yyyy")}.</p>" +
                                    $"<p><b>Fecha de implementación total de la AC: </b>{correctiveAction.MaxDateImplementation.ToString("dd/MM/yyyy")}.</p>" +
                                    $"<p>Puede acceder desde aquí: <a href={url}>Ver tarea de Acción Correctiva</a>.</p>" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" +
                                $"</body>" +
                            $"</html>";
            _emailSender.SendEmailAsync(emailAddress.ToArray(), ccEmailAddresses.ToArray(), bccEmailAddresses.ToArray(), emailSubject, emailMessage, true, MailPriority);
            return true;
        }
    }
}
