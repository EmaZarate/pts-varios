using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.Message.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveAction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Hoshin.Quality.Application.UseCases.ExtendDueDateCorrectiveAction
{
    public class ExtendDueDateCorrectiveActionUseCase : IExtendDueDateCorrectiveActionUseCase
    {
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly ICorrectiveActionStatesHistoryRepository _correctiveActionStatesHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly ISectorPlantRepository _sectorPlantRepository;
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IUserRepository _userRepository;
        public MailPriority MailPriority { get; set; }
        private readonly EmailSettings _emailSettings;

        public ExtendDueDateCorrectiveActionUseCase(
            ICorrectiveActionRepository correctiveActionRepository,
            ICorrectiveActionStatesHistoryRepository correctiveActionStatesHistoryRepository,
            IHttpContextAccessor httpContextAccessor,
            IEmailSender emailSender,
            IMapper mapper,
            ISectorPlantRepository sectorPlantRepository,
            IUserRepository userRepository,
            ICorrectiveActionStateRepository correctiveActionStateRepository,
            IOptions<EmailSettings> emailSettings)

        {
            _correctiveActionRepository = correctiveActionRepository;
            _correctiveActionStatesHistoryRepository = correctiveActionStatesHistoryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
            _sectorPlantRepository = sectorPlantRepository;
            _userRepository = userRepository;
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _emailSettings = emailSettings.Value;
        }

        public bool Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction)
        {
            var emailAddress = new List<string>();
            var ccEmailAddresses = new List<string>();
            var bccEmailAddresses = new List<string>();
            var userID = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var emailSubject = "";
            string content = "";

            var correctiveAc = _correctiveActionRepository.GetOne(correctiveAction.CorrectiveActionID);

            string url = $"{_emailSettings.Url}/quality/corrective-actions/{correctiveAction.CorrectiveActionID}/detail";
            emailSubject = $"HoshinCloud - Extensión fecha de {(correctiveAction.CorrectiveActionState.Code == "EFP" ? "Planificación de AC" : "Evaluación de AC")}";
            content = $"<p>Estimado usuario:</p>";
            content += $"<p>Se ha extendido la fecha de vencimiento de {(correctiveAction.CorrectiveActionState.Code == "EFP" ? "Planificación" : "Evaluación")} de una Acción Correctiva" + "</p>";
            content += $"<p><b>Id de Acción Correctiva</b>: {correctiveAction.CorrectiveActionID}</p>";
            content += $"<p><b>Descripción</b>: {correctiveAction.Description}</p>";
            content += $"<p><b>Sector</b>: {correctiveAc.SectorPlantLocationName}</p>";
            content += $"<p><b>Responsable Asignado</b>: {correctiveAc.ResponsibleUserFullName}</p>";
            content += $"<p><b>Evaluador Asignado</b>: {correctiveAc.ReviewerUserFullName}</p>";
            content += $"<p><b>Nueva fecha de vencimiento {(correctiveAction.CorrectiveActionState.Code == "EFP" ? "Planificación" : "Evaluación")}</b>: " + correctiveAction.DeadlineDatePlanification.ToString("dd/MM/yyyy") + "</p>";
            content += $"<p>Puede acceder desde aquí: <a href={url}>Ver Acción Correctiva.</a></p>";
            content += $"<p>Saludos cordiales.</p>";

            correctiveAction.CorrectiveActionStateID = _correctiveActionStateRepository.GetByCode(correctiveAction.CorrectiveActionState.Code == "EFP" ? "ABI" : "TRT");
            _correctiveActionStatesHistoryRepository.Add(correctiveAction.CorrectiveActionID, correctiveAction.CorrectiveActionStateID, userID);
            _correctiveActionRepository.Update(correctiveAction);
            emailAddress.Add(correctiveAction.ResponisbleUser.Email);
            emailAddress.AddRange(_sectorPlantRepository.GetSectorPlantReferredEmail(Convert.ToInt32(correctiveAction.PlantTreatmentID), Convert.ToInt32(correctiveAction.SectorTreatmentID)));
            emailAddress.AddRange(_userRepository.GetUsersEmailResponsibleSGC());
            _emailSender.SendEmailAsync(emailAddress.ToArray(), ccEmailAddresses.ToArray(), bccEmailAddresses.ToArray(), emailSubject, content, true, MailPriority);

            return true;
        }
    }
}
