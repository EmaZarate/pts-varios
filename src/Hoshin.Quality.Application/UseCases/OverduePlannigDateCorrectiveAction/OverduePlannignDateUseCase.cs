using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.Message.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveAction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Hoshin.Quality.Application.UseCases.OverduePlannigDate
{
    public class OverduePlannignDateUseCase : IOverduePlannignDateUseCase
    {
        private readonly IMapper _mapper;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly IEmailSender _emailSender;
        private readonly EmailSettings _emailSettings;
        private readonly IUserRepository _userReopository;
        private readonly ICorrectiveActionStateRepository _CorrectiveActionStateRepository;
        private readonly ICorrectiveActionStatesHistoryRepository _correctiveActionStatesHistoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OverduePlannignDateUseCase(IMapper mapper, ICorrectiveActionRepository correctiveActionRepository,
             IEmailSender emailSender,
             IUserRepository userReopository,
             ICorrectiveActionStateRepository CorrectiveActionStateRepository,
             ICorrectiveActionStatesHistoryRepository correctiveActionStatesHistoryRepository,
             IHttpContextAccessor httpContextAccessor,
             IOptions<EmailSettings> emailSettings
            ) {
            _mapper = mapper;
            _correctiveActionRepository = correctiveActionRepository;
            _emailSender = emailSender;
            _userReopository = userReopository;
            _CorrectiveActionStateRepository = CorrectiveActionStateRepository;
            _correctiveActionStatesHistoryRepository = correctiveActionStatesHistoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _emailSettings = emailSettings.Value;
        }


        public async void ExecuteAsync(Domain.CorrectiveAction.CorrectiveAction correctiveAction, string observation, DateTime overdueTime, int correctiveActionID)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var correctiveAc = _correctiveActionRepository.GetOne(correctiveActionID);
            var users = _userReopository.GetAll();
            var idUser = "";
            foreach (var u in users)
            {
                var roles = u.Roles;
                foreach (var r in roles)
                {
                    if (r == "Aprobador de AC")
                    {
                        idUser = u.Id;
                    }
                }
            }
            var ResponsibleUser = _userReopository.Get(idUser);
            var email = ResponsibleUser.Email;
            
            List<string> EmailsToNotify = new List<string>();
            EmailsToNotify.Add(email);
            int ACid = 0;
            string url = $"{_emailSettings.Url}/quality/corrective-actions/{correctiveAction.CorrectiveActionID}/detail";
            var emailSubject = "HoshinCloud - Solicitud extensión fecha de Planificación de AC";
            string content = $"<html>";
            content += $"<body>";
            content += $"<p>Estimado usuario:</p>";
            content += $"<p>Se solicita la extensión de la fecha de Planificación de la Acción Correctiva.</p>";
            content += $"<p><b>Id de Acción Correctiva</b>: {correctiveAction.CorrectiveActionID}</p>";
            content += $"<p><b>Descripción</b>: {correctiveAc.Description}</p>";
            content += $"<p><b>Sector</b>: {correctiveAc.SectorPlantTreatmentName}</p>";
            content += $"<p><b>Responsable Asignado</b>: {correctiveAc.ResponsibleUserFullName}</p>";
            content += $"<p><b>Evaluador Asignado</b>: {correctiveAc.ReviewerUserFullName}</p>";
            content += $"<p><b>Fecha vencimiento de planificación: </b> {correctiveAc.DeadlineDatePlanification.ToString("dd/MM/yyyy")}</p>";
            content += $"<p><b>Estado: </b> {correctiveAc.CorrectiveActionStateName}</p>";
            content += $"<p><b>Nueva fecha de vencimiento Planificación</b>: {overdueTime.ToString("dd/MM/yyyy")}</p>";
            content += $"<p>Puede acceder desde aquí: <a href={url}>Ver Acción Correctiva.</a></p>";
            content += $"<p></p>";
            content += $"<p>Saludos cordiales</p>";
            content += $"</body>";
            content += $"</html>";

            await _emailSender.SendEmailAsync(EmailsToNotify.ToArray(), new List<string>().ToArray(), new List<string>().ToArray(), emailSubject, content, true, System.Net.Mail.MailPriority.High);
            
            var allACstates = _CorrectiveActionStateRepository.GetAll();
            foreach (var s in allACstates)
            {
                if (s.Code == "EFP")
                {
                    ACid = s.CorrectiveActionStateID;
                }
            }
            correctiveAc.CorrectiveActionStateID = ACid;
            _correctiveActionStatesHistoryRepository.Add(correctiveAc.CorrectiveActionID, correctiveAc.CorrectiveActionStateID, userID);
            _correctiveActionRepository.Update(correctiveAc);
        }
    }
}
