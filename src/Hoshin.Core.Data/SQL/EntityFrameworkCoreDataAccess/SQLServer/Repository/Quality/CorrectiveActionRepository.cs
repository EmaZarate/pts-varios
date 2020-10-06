using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveAction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Hoshin.CrossCutting.Message.Interfaces;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Application.Repositories;
using Microsoft.Extensions.Options;
using Hoshin.CrossCutting.Message;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class CorrectiveActionRepository : CrossCutting.WorkflowCore.Repositories.ICorrectiveActionRepository, ICorrectiveActionRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISectorPlantRepository _sectorPlantRepository;

        private readonly EmailSettings _emailSettings;

        public CorrectiveActionRepository(IServiceProvider serviceProvider,
                                          ISectorPlantRepository sectorPlantRepository,
                                          IOptions<EmailSettings> emailSettings)
        {
            _serviceProvider = serviceProvider;
            _sectorPlantRepository = sectorPlantRepository;
            _emailSettings = emailSettings.Value;
        }

        public List<CorrectiveAction> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActions = _ctx.CorrectiveActions.Include(x => x.CorrectiveActionState)
                                                                .Include(x => x.SectorPlantLocation)
                                                                    .ThenInclude(y => y.Sector)
                                                                .Include(x => x.SectorPlantLocation)
                                                                    .ThenInclude(y => y.Plant)
                                                                .Include(x => x.SectorPlantTreatment)
                                                                    .ThenInclude(y => y.Sector)
                                                                .Include(x => x.SectorPlantTreatment)
                                                                    .ThenInclude(y => y.Plant)
                                                                .Include(x => x.ResponisbleUser)
                                                                .Include(x => x.ReviewerUser)
                                                                .Include(x => x.CorrectiveActionFishbones)
                                                                 .ThenInclude(x => x.CorrectiveActionFishboneCauses)
                                                                .Include(x => x.Evidences)
                                                                .Include(x => x.Finding)
                                                                    .ThenInclude(x => x.FindingType)
                                                                .Include(x => x.UserCorrectiveActions)
                                                                .OrderByDescending(x => x.CorrectiveActionID)
                                                                .ToList();

                return _mapper.Map<List<CorrectiveActions>, List<CorrectiveAction>>(correctiveActions);
            }
        }

        public List<CorrectiveAction> GetAllFromSectorPlant(int plantId, int sectorId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActions = _ctx.CorrectiveActions.Include(x => x.CorrectiveActionState)
                                                                .Include(x => x.SectorPlantLocation)
                                                                    .ThenInclude(y => y.Sector)
                                                                .Include(x => x.SectorPlantLocation)
                                                                    .ThenInclude(y => y.Plant)
                                                                .Include(x => x.SectorPlantTreatment)
                                                                    .ThenInclude(y => y.Sector)
                                                                .Include(x => x.SectorPlantTreatment)
                                                                    .ThenInclude(y => y.Plant)
                                                                .Include(x => x.ResponisbleUser)
                                                                .Include(x => x.ReviewerUser)
                                                                .Include(x => x.CorrectiveActionFishbones)
                                                                 .ThenInclude(x => x.CorrectiveActionFishboneCauses)
                                                                .Include(x => x.Evidences)
                                                                .Include(x => x.Finding)
                                                                    .ThenInclude(x => x.FindingType)
                                                                .Include(x => x.UserCorrectiveActions)
                                                                .OrderByDescending(x => x.CorrectiveActionID)
                                                                .Where(x => x.PlantTreatmentID == plantId && x.SectorTreatmentID == sectorId)
                                                                .ToList();

                return _mapper.Map<List<CorrectiveActions>, List<CorrectiveAction>>(correctiveActions);
            }
        }

        public List<CorrectiveAction> GetAllFromUser(string userId, int plantId, int sectorId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActions = _ctx.CorrectiveActions.Include(x => x.CorrectiveActionState)
                                                                .Include(x => x.SectorPlantLocation)
                                                                    .ThenInclude(y => y.Sector)
                                                                .Include(x => x.SectorPlantLocation)
                                                                    .ThenInclude(y => y.Plant)
                                                                .Include(x => x.SectorPlantTreatment)
                                                                    .ThenInclude(y => y.Sector)
                                                                .Include(x => x.SectorPlantTreatment)
                                                                    .ThenInclude(y => y.Plant)
                                                                .Include(x => x.ResponisbleUser)
                                                                .Include(x => x.ReviewerUser)
                                                                 .Include(x => x.CorrectiveActionFishbones)
                                                                 .ThenInclude(x => x.CorrectiveActionFishboneCauses)
                                                                .Include(x => x.Evidences)
                                                                .Include(x => x.Finding)
                                                                    .ThenInclude(x => x.FindingType)
                                                                .Include(x => x.UserCorrectiveActions)
                                                                .OrderByDescending(x => x.CorrectiveActionID)
                                                                .Where(x => x.PlantTreatmentID == plantId && x.SectorTreatmentID == sectorId)
                                                                .Where(x => x.ResponsibleUserID == userId || x.ReviewerUserID == userId)
                                                                .ToList();

                return _mapper.Map<List<CorrectiveActions>, List<CorrectiveAction>>(correctiveActions);
            }
        }

        public CorrectiveActionWorkflowData Add(CorrectiveActionWorkflowData correctiveAction)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                CorrectiveActions mappedCorrectiveAction = _mapper.Map<CorrectiveActionWorkflowData, CorrectiveActions>(correctiveAction);

                _ctx.CorrectiveActions.Add(mappedCorrectiveAction);
                _ctx.SaveChanges();

                return _mapper.Map<CorrectiveActions, CorrectiveActionWorkflowData>(mappedCorrectiveAction);
            }
        }

        public CorrectiveAction Add(CorrectiveAction correctiveAction)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                CorrectiveActions newCorrectiveAction = _mapper.Map<CorrectiveAction, CorrectiveActions>(correctiveAction);

                _ctx.CorrectiveActions.Add(newCorrectiveAction);
                _ctx.SaveChanges();

                return _mapper.Map<CorrectiveActions, CorrectiveAction>(newCorrectiveAction);
            }
        }

        public CorrectiveAction GetOne(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActions = _ctx.CorrectiveActions.Include(x => x.CorrectiveActionState)
                                                                .Include(x => x.SectorPlantLocation)
                                                                    .ThenInclude(y => y.Sector)
                                                                .Include(x => x.SectorPlantLocation)
                                                                    .ThenInclude(y => y.Plant)
                                                                .Include(x => x.SectorPlantTreatment)
                                                                    .ThenInclude(y => y.Sector)
                                                                .Include(x => x.SectorPlantTreatment)
                                                                    .ThenInclude(y => y.Plant)
                                                                .Include(x => x.ResponisbleUser)
                                                                .Include(x => x.ReviewerUser)
                                                                //includes for UserCorrectiveActions
                                                                .Include(x => x.UserCorrectiveActions)
                                                                    .ThenInclude(z => z.Users)
                                                                //Includes for Fishbone
                                                                .Include(x => x.CorrectiveActionFishbones)
                                                                    .ThenInclude(y => y.CorrectiveActionFishboneCauses)
                                                                        .ThenInclude(z => z.CorrectiveActionFishboneCauseWhys)
                                                                .Include(x => x.Evidences)
                                                                .Include(x => x.Finding)
                                                                    .ThenInclude(x => x.FindingType)
                                                                .Where(x => x.CorrectiveActionID == id)
                                                                .FirstOrDefault();

                return _mapper.Map<CorrectiveActions, CorrectiveAction>(correctiveActions);
            }
        }

        public int GetCount()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                return _ctx.CorrectiveActions.Count();
            }
        }

        public void Update(CorrectiveActionWorkflowData correctiveAction)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                //var correctiveActionToUpdate = _ctx.CorrectiveActions.FirstOrDefault(x => x.CorrectiveActionID == correctiveAction.CorrectiveActionID);
                //correctiveActionToUpdate.CorrectiveActionStateID = correctiveAction.CorrectiveActionStateID;
                //correctiveActionToUpdate.MaxDateEfficiencyEvaluation = correctiveAction.MaxDateEfficiencyEvaluation;
                //correctiveActionToUpdate.MaxDateImplementation = correctiveAction.MaxDateImplementation;
                //correctiveActionToUpdate.DeadlineDateEvaluation = correctiveAction.DeadlineDateEvaluation;
                //correctiveActionToUpdate.DeadlineDatePlanification = correctiveAction.DeadlineDatePlanification;
                //correctiveActionToUpdate.Impact = correctiveAction.Impact;
                //_ctx.CorrectiveActions.Update(correctiveActionToUpdate);
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                //var correctiveActionToUpdate = _ctx.CorrectiveActions.FirstOrDefault(x => x.CorrectiveActionID == correctiveAction.CorrectiveActionID);
                //correctiveActionToUpdate.CorrectiveActionStateID = correctiveAction.CorrectiveActionStateID;
                //correctiveActionToUpdate.MaxDateEfficiencyEvaluation = correctiveAction.MaxDateEfficiencyEvaluation;
                //correctiveActionToUpdate.MaxDateImplementation = correctiveAction.MaxDateImplementation;
                //correctiveActionToUpdate.Impact = correctiveAction.Impact;
                _ctx.CorrectiveActions.Update(_mapper.Map<CorrectiveActionWorkflowData,CorrectiveActions>(correctiveAction));
                _ctx.SaveChanges();
            }
        }

        
        public void Delete(CorrectiveAction correctiveAction)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var entity = _ctx.CorrectiveActions.Where(x => x.CorrectiveActionID == correctiveAction.CorrectiveActionID).FirstOrDefault();
                var historyStates = _ctx.CorrectiveActionStatesHistory.Where(x => x.CorrectiveActionID == correctiveAction.CorrectiveActionID);
                _ctx.CorrectiveActionStatesHistory.RemoveRange(historyStates);
                _ctx.Remove(entity);
                _ctx.SaveChanges();
            }
        }

        public string GetWorkflowId(int correctiveActionId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                return _ctx.CorrectiveActions.FirstOrDefault(x => x.CorrectiveActionID== correctiveActionId).WorkflowId;
            }
        }

        public CorrectiveActionWorkflowData GetOneByWorkflowId(string workflowId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                CorrectiveActions correctiveAction = _ctx.CorrectiveActions
                                                            .Include(x => x.CorrectiveActionState)
                                                            .Include(x => x.ResponisbleUser)
                                                            .Include(x => x.ReviewerUser)
                                                            .Include(x => x.SectorPlantTreatment)
                                                                .ThenInclude(x => x.Sector)
                                                            .Where(x => x.WorkflowId == workflowId)
                                                            .FirstOrDefault();

                return _mapper.Map<CorrectiveActions, CorrectiveActionWorkflowData>(correctiveAction);
            }
        }
        public CorrectiveAction Update(CorrectiveAction updateCorrectiveAction)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveAction = _mapper.Map<CorrectiveAction, CorrectiveActions>(updateCorrectiveAction);

                _ctx.Entry(correctiveAction).State = EntityState.Modified;
                _ctx.SaveChanges();

                return _mapper.Map<CorrectiveActions, CorrectiveAction>(correctiveAction);
            }
        }
        public CorrectiveAction UpdateByReassign(CorrectiveAction correctiveAction)
        {
            this.Update(correctiveAction);
            this.SendEmailAuditRescheduling(correctiveAction) ;
            return correctiveAction;
        }
        private void SendEmailAuditRescheduling( CorrectiveAction correctiveAction)
        {
            List<string> listMails = new List<string>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _emailSender = scope.ServiceProvider.GetService(typeof(IEmailSender)) as IEmailSender;
                Users newResponsibleUser = _ctx.Users.Where(x => x.Id == correctiveAction.ResponsibleUserID).SingleOrDefault();
                Users lastResponsibleUser = _ctx.Users.Where(x => x.Id == correctiveAction.LastResponsibleUserID).SingleOrDefault();
                Sectors sector = _ctx.Sectors.Where(x => x.SectorID == correctiveAction.SectorTreatmentID).SingleOrDefault();
                listMails.AddRange(_sectorPlantRepository.GetSectorPlantReferredEmail(Convert.ToInt32(correctiveAction.PlantTreatmentID), Convert.ToInt32(correctiveAction.SectorTreatmentID)));
                listMails.Add(newResponsibleUser.Email);
                listMails.Add(lastResponsibleUser.Email);
                string newResponsibleName = newResponsibleUser.FirstName + " " + newResponsibleUser.Surname;
                string lastResponsibleName = lastResponsibleUser.FirstName + " " + lastResponsibleUser.Surname;
                string description = correctiveAction.Description;
                string sectorName = sector.Name;
                string estado = correctiveAction.CorrectiveActionState.Name;
                int correctiveActionID = correctiveAction.CorrectiveActionID;
                _emailSender.SendEmailAsync(listMails.ToArray(), new string[0], new string[0], $"HoshinCloud – Reasignación de Acción Correctiva",
                    SetMessageEmail(newResponsibleName,
                                    lastResponsibleName,
                                    description,
                                    sectorName,
                                    estado,
                                    correctiveActionID), true, System.Net.Mail.MailPriority.High);
            }
        }
        private string SetMessageEmail(string newResponsibleName,string lastResponsibleName, string description, 
                                       string sector, string estado, int correctiveActionID)
        {
            string url = $"{_emailSettings.Url}/quality/corrective-actions/{correctiveActionID}/detail";
            string content = $"<p><b>Id de AC</b>: {correctiveActionID}</p>";
            content += $"<p><b>Descripción</b>: {description}</p>";
            content += $"<p><b>Sector Tratamiento</b>: {sector}</p>";
            content += $"<p><b>Responsable asignado anterior</b>: {lastResponsibleName}</p>";
            content += $"<p><b>Responsable asignado actual</b>: {newResponsibleName}</p>";
            content += $"<p><b>Estado del AC</b>: {estado}</p>";

            string bodyHtml = $"<html>" +
                                $"<body>" +
                                    $"<p>Estimado usuario: </p>" +
                                    $"<p>Se realizó la Reasignación de una Acción Correctiva.</p>" +
                                    $"{content}" +
                                    $"<p>Puede acceder desde aquí: <a href={url}>Ver Acción Correctiva.</a></p>" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" +
                                $"</body>" +
                            $"</html>";

            return bodyHtml;
        }
        public void ChangeTasksState(int correctiveActionId, int taskNewStateID)
        {
            const int CORRECTIVE_ACTION_ENTITY_TYPE = 1;
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var taskOfCorrectiveAction =
                    _ctx.Tasks
                        .Where(t => t.EntityID == correctiveActionId && t.EntityType == CORRECTIVE_ACTION_ENTITY_TYPE)
                        .ToList();

                taskOfCorrectiveAction.ForEach((t) =>
                {
                    t.TaskStateID = taskNewStateID;
                });

                _ctx.UpdateRange(taskOfCorrectiveAction);
                _ctx.SaveChanges();
            }
        }
        public List<string> GetEmailOfTasksResposibles(int correctiveActionId)
        {
            const int CORRECTIVE_ACTION_ENTITY_TYPE = 1;
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.Tasks
                        .Include(e => e.ResponsibleUser)
                        .Where(t => t.EntityID == correctiveActionId && t.EntityType == CORRECTIVE_ACTION_ENTITY_TYPE)
                        .Select(t => t.ResponsibleUser.Email)
                        .ToList();
            }
        }

        public void EditImpact(string impact,DateTime MaxDateImplementation, DateTime MaxDateEfficiencyEvaluation, int correctiveActionID)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var correctiveActionDataBase =  _ctx.CorrectiveActions.Where(x => x.CorrectiveActionID == correctiveActionID).FirstOrDefault();
                correctiveActionDataBase.Impact = impact;
                correctiveActionDataBase.MaxDateEfficiencyEvaluation = MaxDateEfficiencyEvaluation;
                correctiveActionDataBase.MaxDateImplementation = MaxDateImplementation;
                _ctx.CorrectiveActions.Update(correctiveActionDataBase);
                _ctx.SaveChanges();
            }
        }
    }
}
