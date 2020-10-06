using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.Quality.Domain.Audit;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.CrossCutting.Message.Interfaces;
using Newtonsoft.Json;
using Hoshin.Core.Application.Repositories;
using Hoshin.CrossCutting.Message;
using Microsoft.Extensions.Options;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class AuditRepository: CrossCutting.WorkflowCore.Repositories.IAuditRepository, Hoshin.Quality.Application.Repositories.IAuditRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CrossCutting.WorkflowCore.Repositories.ISectorPlantRepository _sectorPlantRepository;
        private readonly EmailSettings _emailSettings;
        const string ASPECT_STATE_PENDING = "Pendiente";

        public AuditRepository(IServiceProvider serviceProvider, CrossCutting.WorkflowCore.Repositories.ISectorPlantRepository sectorPlantRepository, IOptions<EmailSettings> emailSettings)
        {
            _serviceProvider = serviceProvider;
            _sectorPlantRepository = sectorPlantRepository;
            _emailSettings = emailSettings.Value;
        }

        public AuditWorkflowData Add(AuditWorkflowData audit)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                Audits mappedAudit = _mapper.Map<AuditWorkflowData, Audits>(audit);
                mappedAudit.AuditStandards = new List<AuditStandard>();
                foreach (int item in audit.AuditStandard)
                {
                    mappedAudit.AuditStandards.Add(new AuditStandard { StandardID = item, AuditID = 0 });
                }

                _ctx.Audits.Add(mappedAudit);
                _ctx.SaveChanges();

                return _mapper.Map<Audits, AuditWorkflowData>(mappedAudit);
            }
            
        }



        public bool ApproveOrRejectAuditPlan(AuditWorkflowData audit)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditResult = _ctx.Audits
                                    .Where(x => x.AuditID == audit.AuditID)
                                    .FirstOrDefault();

                auditResult.ApprovePlanComments = audit.ApprovePlanComments;
                auditResult.AuditStateID = audit.AuditStateID;

                _ctx.Audits.Update(auditResult);
                _ctx.SaveChanges();

                return true;
            }
        }

        public bool ApproveOrRejectReportAudit(AuditWorkflowData audit)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditResult = _ctx.Audits
                                    .Where(x => x.AuditID == audit.AuditID)
                                    .FirstOrDefault();

                auditResult.ApproveReportComments = audit.ApproveReportComments;
                auditResult.AuditStateID = audit.AuditStateID;

                _ctx.Audits.Update(auditResult);
                _ctx.SaveChanges();

                return true;
            }
        }

        public bool PlanAudit(AuditWorkflowData audit, List<AuditStandardAspects> auditStandardAspects)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _aspectStateRepository = scope.ServiceProvider.GetService(typeof(IAspectStatesRepository)) as AspectStatesRepository;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;


                var auditResult = _ctx.Audits
                                    .Where(x => x.AuditID == audit.AuditID)
                                    .Include(x => x.AuditStandards)
                                        .ThenInclude(x => x.AuditStandardAspects)
                                    .FirstOrDefault();

                auditResult.AuditTeam = audit.AuditTeam;
                auditResult.AuditInitDate = audit.AuditInitDate;
                auditResult.AuditInitTime = audit.AuditInitTime;
                auditResult.AuditFinishDate = audit.AuditFinishDate;
                auditResult.AuditFinishTime = audit.AuditFinishTime;
                auditResult.DocumentsAnalysisDate = audit.DocumentsAnalysisDate;
                auditResult.DocumentAnalysisDuration = audit.DocumentAnalysisDuration;
                auditResult.ReportEmittedDate = audit.ReportEmittedDate;
                auditResult.CloseMeetingDate = audit.CloseMeetingDate;
                auditResult.CloseMeetingDuration = audit.CloseMeetingDuration;
                auditResult.CloseDate = audit.CloseDate;
                auditResult.AuditStateID = audit.AuditStateID;

                var auditStandardAspectRepo = _mapper.Map<List<AuditStandardAspects>, List<Entities.Quality.AuditStandardAspect>>(auditStandardAspects);

                auditResult.AuditStandards.ToList().ForEach(x =>
                {
                    x.AuditStandardAspects.Clear();
                    foreach (var audstandasp in auditStandardAspectRepo)
                    {
                        audstandasp.AspectStateID = _aspectStateRepository.GetAspectStateID(ASPECT_STATE_PENDING);
                        if (audstandasp.AuditID == x.AuditID && audstandasp.StandardID == x.StandardID)
                        {

                                x.AuditStandardAspects.Add(audstandasp);

                        }
                    }
                });

                _ctx.Audits.Update(auditResult);
                _ctx.SaveChanges();

                return true;
            }
        }
        
        public bool EmmitReport(AuditWorkflowData audit)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditResult = _ctx.Audits
                                        .Where(x => x.AuditID == audit.AuditID)
                                        .FirstOrDefault();

                auditResult.AuditStateID = audit.AuditStateID;
                auditResult.Conclusion = audit.Conclusion;
                auditResult.Recomendations = audit.Recomendations;

                _ctx.Audits.Update(auditResult);
                _ctx.SaveChanges();

                return true;
            }
        }

        public bool Update(Audit audit)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _emailSender = scope.ServiceProvider.GetService(typeof(IEmailSender)) as IEmailSender;
                var _userRepository = scope.ServiceProvider.GetService(typeof(Application.Repositories.IUserRepository)) as Application.Repositories.IUserRepository;

                var auditResult = _ctx.Audits.Include(x => x.AuditStandards).ThenInclude(x => x.Standard).Include(x => x.SectorPlant).Include(x => x.Auditor)
                    .Include(x => x.AuditReschedulingHistories).Include(x => x.AuditType).Include(x => x.AuditState).Where(x => x.AuditID == audit.AuditID).FirstOrDefault();


                SectorsPlants sectorsPlants = _ctx.SectorsPlants.Include(x => x.Plant).Include(x => x.Sector).Where(x => x.SectorID == audit.SectorID && x.PlantID == audit.PlantID).SingleOrDefault();
                auditResult.SectorPlant = sectorsPlants;
                auditResult.AuditInitDate = audit.AuditInitDate;

                AuditReschedulingHistory auditReschedulingHistory = new AuditReschedulingHistory();
                auditReschedulingHistory.AuditID = audit.AuditID;
                auditReschedulingHistory.DateRescheduling = DateTime.Now;
                auditResult.AuditReschedulingHistories.Add(auditReschedulingHistory);



                List<string> sectorPlantReferrents = _sectorPlantRepository.GetSectorPlantReferredEmail(auditResult.PlantID, auditResult.SectorID);
                sectorPlantReferrents.AddRange(_sectorPlantRepository.GetSectorPlantReferredEmail(audit.PlantID, audit.SectorID));

                List<string> userRoles = _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "responsable sgc").Select(x => x.User.Email).ToList();
                sectorPlantReferrents.AddRange(userRoles);

                #region Envio de mail

                if (audit.AuditorID != null)
                {
                    if (auditResult.AuditorID != null)
                    {
                        var email = _userRepository.Get(audit.AuditorID).Email;
                        if (auditResult.Auditor.Email == email)
                        {
                            sectorPlantReferrents.Add(auditResult.Auditor.Email);
                        }
                        else
                        {
                            sectorPlantReferrents.Add(email);
                            sectorPlantReferrents.Add(auditResult.Auditor.Email);
                        }
                    }
                    else
                    {
                        List<string> auditorInterno = _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "auditor interno").Select(x => x.User.Email).ToList();
                        sectorPlantReferrents.AddRange(auditorInterno);
                    }
                }
                else if (audit.AuditTypeID == 10)
                {
                    if (auditResult.AuditTypeID == 1)
                    {
                        sectorPlantReferrents.Add(auditResult.Auditor.Email);
                    }
                    else
                    {
                        List<string> auditorInterno = _ctx.UserRoles.Include(x => x.User).Include(x => x.Role).Where(x => x.Role.Name.ToLower() == "auditor interno").Select(x => x.User.Email).ToList();
                        sectorPlantReferrents.AddRange(auditorInterno);
                    }
                }


                if (sectorPlantReferrents.Count > 0)
                {
                    //sectorPlantReferrents = sectorPlantReferrents.Distinct().ToList();
                    SendEmailAuditRescheduling(sectorPlantReferrents.ToArray(), new string[0], new string[0], audit);
                }

                #endregion

                if (auditResult.AuditState.Code == "PLA" || auditResult.AuditState.Code == "PRE")
                {
                    auditResult.AuditInitTime = audit.AuditInitTime;
                    auditResult.AuditFinishTime = audit.AuditFinishTime;
                    auditResult.AuditFinishDate = audit.AuditFinishDate;
                    auditResult.DocumentAnalysisDuration = audit.DocumentAnalysisDuration;
                    auditResult.DocumentsAnalysisDate = audit.DocumentsAnalysisDate;
                    auditResult.CloseMeetingDate = audit.CloseMeetingDate;
                    auditResult.CloseMeetingDuration = audit.CloseMeetingDuration;
                    auditResult.CloseDate = audit.CloseDate;
                }

                if (audit.AuditStateID == 1)
                {
                    auditResult.AuditTypeID = audit.AuditTypeID;
                    auditResult.AuditStandards = audit.AuditStandardsID.Select(x => new AuditStandard
                    {
                        AuditID = audit.AuditID,
                        StandardID = x
                    }).ToList();

                    auditResult.AuditorID = audit.AuditorID;
                    auditResult.ExternalAuditor = audit.ExternalAuditor;
                }

                _ctx.Update(auditResult);
                _ctx.SaveChanges();
            }
            return true;
        }

        private string SetMessageEmail(int auditID, DateTime initDate, string sector, string nameAuditType, List<AuditStandard> auditStandards, string auditorFullname)
        {
            string url = $"{_emailSettings.Url}/quality/audits/{auditID}/detail";
            var normasRows = "";
            foreach (var name in auditStandards.Select(x => x.Standard.Name))
            {
                normasRows += $"<tr><td>{name}</td></tr>";
            }

            string content = $"<p><b>Id de Auditoría</b>: {auditID}</p>";
            content += $"<p><b>Tipo de Auditoría</b>: {nameAuditType}</p>";
            content += $"<p><b>Normas</b>: <table>{normasRows}</table></p>";    
            content += $"<p><b>Sector</b>: {sector}</p>";
            content += $"<p><b>Auditor Asignado</b>: {auditorFullname}</p>";
            content += $"<p><b>Fecha de Inicio</b>: {initDate.ToString("dd/MM/yyyy")}</p>";

            string bodyHtml = $"<html>" +
                                $"<body>" +
                                    $"<p>Estimado usuario: </p>" +
                                    $"<p>Ha sido reprogramada una auditoría.</p>" +
                                    $"{content}" +
                                    $"<p>Puede acceder desde aquí: <a href={url}>Ver auditoría.</a></p>" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" +
                                $"</body>" +
                            $"</html>";

            return bodyHtml;
        }

        private void SendEmailAuditRescheduling(string[] toEmails, string[] ccEmails, string[] bccEmails, Audit audit)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _emailSender = scope.ServiceProvider.GetService(typeof(IEmailSender)) as IEmailSender;

                Sectors sector = _ctx.Sectors.Where(x => x.SectorID == audit.SectorID).SingleOrDefault();
                Plants plant = _ctx.Plants.Where(x => x.PlantID == audit.PlantID).SingleOrDefault();
                AuditsTypes auditsTypes = _ctx.AuditsTypes.Where(x => x.AuditTypeID == audit.AuditTypeID).SingleOrDefault();
                List<AuditStandard> auditStandards = _ctx.AuditStandard.Include(x => x.Standard).Where(x => audit.AuditStandardsID.Contains(x.StandardID) && x.AuditID == audit.AuditID).ToList();
                Users auditor = _ctx.Users.Where(x => x.Id == audit.AuditorID).SingleOrDefault();
                String auditorFullname = (auditor == null ? audit.ExternalAuditor : auditor.Surname + ", " + auditor.FirstName);

                _emailSender.SendEmailAsync(toEmails, new string[0], new string[0], $"Hoshin Cloud - Reprogramación de Auditoría - {auditsTypes.Name}", 
                    SetMessageEmail(audit.AuditID,
                                    audit.AuditInitDate,
                                    sector.Name,
                                    auditsTypes.Name,
                                    auditStandards,
                                    auditorFullname), true, System.Net.Mail.MailPriority.High);

            }
        }

        public Audit Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var audit = _ctx.Audits.Include(x => x.AuditStandards)
                    .ThenInclude(x => x.Standard).ThenInclude(x => x.Aspects)
                    .Include(x => x.AuditReschedulingHistories)
                    .Include(x => x.SectorPlant).ThenInclude(x => x.Sector)
                    .Include(x => x.SectorPlant).ThenInclude(x => x.Plant)
                    .Include(x => x.AuditType)
                    .Include(x => x.Auditor)
                    .Include(x =>x.AuditState)
                    .Where(x => x.AuditID == id).OrderByDescending(x => x.AuditID).FirstOrDefault();

                return _mapper.Map<Audits, Audit>(audit);
            }
        }

        public List<Audit> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var audits = _ctx.Audits
                    .Include(x => x.AuditStandards)
                    .ThenInclude(x => x.Standard)
                    .Include(x => x.AuditStandards).ThenInclude(x => x.AuditStandardAspects).ThenInclude(x => x.Findings)
                    .Include(x => x.AuditStandards).ThenInclude(x => x.AuditStandardAspects).ThenInclude(x => x.Aspect)
                    .Include(x => x.SectorPlant).ThenInclude(x => x.Sector)
                    .Include(x => x.SectorPlant).ThenInclude(x => x.Plant)
                    .Include(x => x.AuditType)
                    .Include(x => x.AuditState)
                    .Include(x => x.Auditor)
                    .OrderByDescending(x => x.AuditID)
                    .ToList();

                return _mapper.Map<List<Audits>, List<Audit>>(audits);
            }
        }

        public string GetAuditorEmail(string id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.Users.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
            }
        }

        public AuditWorkflowData GetOneByWorkflowId(string id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

                Audits audit = _ctx.Audits
                                    .Include(x => x.AuditType)
                                    .Include(x => x.SectorPlant)
                                        .ThenInclude(x => x.Sector)
                                    .Include(x => x.Auditor)
                                    .Include(x => x.AuditStandards)
                                        .ThenInclude(x => x.Standard)
                                    .Where(x => x.WorkflowId == id).OrderByDescending(x => x.AuditID).FirstOrDefault();

                return _mapper.Map<Audits, AuditWorkflowData>(audit);
            }
        }

        public bool Delete(int auditId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                Audits auditToRemove = _ctx.Audits.FirstOrDefault(a => a.AuditID == auditId);
                _ctx.Audits.Remove(auditToRemove);
                _ctx.SaveChanges();

                return true;
            }
        }

        public int GetCount()
        {
            using (var scope = _serviceProvider.CreateScope()) {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
              return _ctx.Audits.Count();
            }
        }

        public List<Audit> GetAllForAuditor(string id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var audits = _ctx.Audits
                    .Where(x => x.AuditorID == id)
                    .Include(x => x.AuditStandards)
                    .ThenInclude(x => x.Standard)
                    .Include(x => x.AuditStandards).ThenInclude(x => x.AuditStandardAspects).ThenInclude(x => x.Findings)
                    .Include(x => x.AuditStandards).ThenInclude(x => x.AuditStandardAspects).ThenInclude(x => x.Aspect)
                    .Include(x => x.SectorPlant).ThenInclude(x => x.Sector)
                    .Include(x => x.SectorPlant).ThenInclude(x => x.Plant)
                    .Include(x => x.AuditType)
                    .Include(x => x.AuditState)
                    .Include(x => x.Auditor)
                    .OrderByDescending(x => x.AuditID).ToList();

                return _mapper.Map<List<Audits>, List<Audit>>(audits);
            }
        }

        public List<Audit> GetAllForColaboratorOrSectorBoss(int userJobId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var audits = _ctx.Audits
                    .Include(x => x.AuditStandards)
                    .ThenInclude(x => x.Standard)
                    .Include(x => x.AuditStandards).ThenInclude(x => x.AuditStandardAspects).ThenInclude(x => x.Findings)
                    .Include(x => x.AuditStandards).ThenInclude(x => x.AuditStandardAspects).ThenInclude(x => x.Aspect)
                    .Include(x => x.SectorPlant).ThenInclude(x => x.Sector)
                    .Include(x => x.SectorPlant).ThenInclude(x => x.Plant)
                    .Include(x => x.AuditType)
                    .Include(x => x.AuditState)
                    .Include(x => x.Auditor)
                    .Where(x => x.SectorPlant.ReferringJob == userJobId || x.SectorPlant.ReferringJob2 == userJobId)
                    .OrderByDescending(x => x.AuditID).ToList();

                return _mapper.Map<List<Audits>, List<Audit>>(audits);
            }
        }
    }
}
