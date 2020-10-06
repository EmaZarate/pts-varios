using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.Message.Interfaces;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.OverdueEvaluateDateCorrectiveAction;
using Hoshin.Quality.Domain.CorrectiveAction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.OverdueEvaluateDateUseCase
{
   public class OverdueEvaluateDateUseCase : IOverdueEvaluateDateUseCase
    {
        private readonly IMapper _mapper;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly IEmailSender _emailSender;
        private readonly EmailSettings _emailSettings;
        private readonly IUserRepository _userReopository;
        private readonly ICorrectiveActionStateRepository _CorrectiveActionStateRepository;
        private readonly ICorrectiveActionStatesHistoryRepository _correctiveActionStatesHistoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Hoshin.CrossCutting.WorkflowCore.Repositories.IUserWorkflowRepository _userWorkflowRepository;
    

        public OverdueEvaluateDateUseCase(IMapper mapper, ICorrectiveActionRepository correctiveActionRepository,
            IEmailSender emailSender,
            IUserRepository userReopository,
            ICorrectiveActionStateRepository CorrectiveActionStateRepository,
            ICorrectiveActionStatesHistoryRepository correctiveActionStatesHistoryRepository,
            IHttpContextAccessor httpContextAccessor,
            IOptions<EmailSettings> emailSettings,
            Hoshin.CrossCutting.WorkflowCore.Repositories.IUserWorkflowRepository userWorkflowRepository
            )
        {
            _mapper = mapper;
            _correctiveActionRepository = correctiveActionRepository;
            _emailSender = emailSender;
            _userReopository = userReopository;
            _CorrectiveActionStateRepository = CorrectiveActionStateRepository;
            _correctiveActionStatesHistoryRepository = correctiveActionStatesHistoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _emailSettings = emailSettings.Value;
            _userWorkflowRepository = userWorkflowRepository;
        }

        public async void ExecuteAsync(Domain.CorrectiveAction.CorrectiveAction correctiveAction, string observation, DateTime overdueTime, int correctiveActionID)
        {
            var userID = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            var correctiveAc = _correctiveActionRepository.GetOne(correctiveAction.CorrectiveActionID);
            var users = _userReopository.GetAll();
            //var idUser = "";
            //foreach (var u in users)
            //{
            //    var roles = u.Roles;
            //    foreach (var r in roles)
            //    {
            //        if (r == "Aprobador de AC")
            //        {
            //            idUser = u.Id;
            //        }
            //    }
            //}
          
            string url = $"{_emailSettings.Url}/quality/corrective-actions/{correctiveAction.CorrectiveActionID}/detail";
            List<string> EmailsToNotify = new List<string>();
            EmailsToNotify.AddRange(_userWorkflowRepository.GetUsersEmailCorrectiveActionApprover());
            int ACid = 0;

            string emailSubject = "HoshinCloud - Solicitud extensión fecha de Evaluación de AC.";
            string content = $"<html>" +
                                $"<body>" +
                                $"<p>Estimado usuario: </p>" +
                                $"<p>Se solicita la extensión de la fecha de Evaluación de la Acción Correctiva:</p>" +
                                $"<p><b>Id de Acción Correctiva: </b> {correctiveAction.CorrectiveActionID}</p>" +
                                $"<p><b>Descripción: </b> {correctiveAc.Description}</p>" +
                                $"<p><b>Sector: </b> {correctiveAc.SectorPlantTreatmentName}</p>" +
                                $"<p><b>Responsable asignado: </b> {correctiveAc.ResponsibleUserFullName}</p>" +
                                $"<p><b>Evaluador asignado: </b>{correctiveAc.ReviewerUserFullName}</p>" +
                                $"<p><b>Fecha vencimiento de Evaluación: </b>{correctiveAc.DeadlineDateEvaluation.ToString("dd/MM/yyyy")}</p>" +
                                $"<p><b>Estado :</b>{correctiveAc.CorrectiveActionStateName}</p>" +
                                $"<p><b>Fecha vencimiento Evaluación solicitada: </b>{overdueTime.ToString("dd/MM/yyyy")}</p>" +
                                $"<p>Puede acceder desde aquí: <a href={url}>Ver Acción Correctiva.</a></p>" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" + 
                                $"</body>" +
                            $"</html>";


            await _emailSender.SendEmailAsync(EmailsToNotify.ToArray(), new List<string>().ToArray(), new List<string>().ToArray(), emailSubject, content, true, System.Net.Mail.MailPriority.High);

            var allACstates = _CorrectiveActionStateRepository.GetAll();
            foreach (var s in allACstates)
            {
                if (s.Code == "EFE")
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
