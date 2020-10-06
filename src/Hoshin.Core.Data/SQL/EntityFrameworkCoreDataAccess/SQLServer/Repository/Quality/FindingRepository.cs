using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Finding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Hoshin.CrossCutting.WorkflowCore.Audit;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class FindingRepository : CrossCutting.WorkflowCore.Repositories.IFindingRepository, Hoshin.Quality.Application.Repositories.IFindingRepository
    {
        private const int EN_ESPERA_APROBACION = 10;
        private const int FINALIZADO = 11;
        private const int FINALIZADO_NO_OK = 12;
        private const int VENCIDO = 13;
        private const int CERRADO = 6;

        private readonly IServiceProvider _serviceProvider;
        public FindingRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Finding Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var finding = _ctx.Findings
                    .Include(x => x.EmitterUser)
                        .ThenInclude(x => x.JobSectorPlant.SectorPlant.Sector)
                    .Include(x => x.EmitterUser)
                        .ThenInclude(x => x.JobSectorPlant.SectorPlant.Plant)
                    //.Include(x => x.AuditStandardAspect)
                    .Include(x => x.ResponsibleUser)
                    .Include(x => x.FindingType)
                    .Include(x => x.FindingState)
                    .Include(x => x.FindingComments)
                    .Include(x => x.SectorPlantTreatment)
                        .ThenInclude(y => y.Sector)
                    .Include(x => x.SectorPlantTreatment)
                        .ThenInclude(y => y.Plant)
                    .Include(x => x.SectorPlantLocation)
                        .ThenInclude(y => y.Sector)
                    .Include(x => x.SectorPlantLocation)
                        .ThenInclude(y => y.Plant)
                    .Include(x => x.FindingsEvidences)
                    .Where(x => x.FindingID == id).FirstOrDefault();

                return _mapper.Map<Findings, Finding>(finding);
            }
        }

        public Finding GetWithoutIncludes(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _mapper.Map<Findings, Finding>(_ctx.Findings.Where(x => x.FindingID == id).FirstOrDefault());
            }
        }

        public List<Finding> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var findings = this.MakeGetAllQuery(_ctx)
                    .ToList();
                return _mapper.Map<List<Findings>, List<Finding>>(findings);
            }
        }





        /*------------ Recupera los hallazgos a través de un Stored Procedure, NO SE ESTÁ USANDO ------------*/
        public List<FindingSP> GetAllWithSP()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var findings3 = _ctx.Query<FindingsSP>().FromSql("EXEC [dbo].[sp_getallfindings]").ToList();

                return _mapper.Map<List<FindingsSP>, List<FindingSP>>(findings3);
            }
        }

        public List<Finding> GetAllFromSectorPlant(int plantId, int sectorId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var findings = this.MakeGetAllQuery(_ctx)
                        .Where(x => x.PlantTreatmentID == plantId && x.SectorTreatmentID == sectorId)
                    .ToList();
                return _mapper.Map<List<Findings>, List<Finding>>(findings);
            }
        }

        public List<Finding> GetAllFromUser(string userId, int plantId, int sectorId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var findings = this.MakeGetAllQuery(_ctx)
                        .Where(x => x.PlantTreatmentID == plantId && x.SectorTreatmentID == sectorId)
                        .Where(x => x.ResponsibleUserID == userId)
                    .ToList();
                return _mapper.Map<List<Findings>, List<Finding>>(findings);
            }
        }

        public List<Finding> GetAllApprovedInProgress()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var findings = _ctx.Findings.Include(x => x.FindingType)
                                            .Where(x => x.FindingStateID != EN_ESPERA_APROBACION &&
                                                        x.FindingStateID != FINALIZADO &&
                                                        x.FindingStateID != FINALIZADO_NO_OK &&
                                                        x.FindingStateID != VENCIDO &&
                                                        x.FindingStateID != CERRADO &&
                                                        x.CorrectiveAction == null).ToList();

                return _mapper.Map<List<Findings>, List<Finding>>(findings);
            }
        }
        private IQueryable<Findings> MakeGetAllQuery(SQLHoshinCoreContext _ctx)
        {

            return _ctx.Findings
                .Include(x => x.EmitterUser)
                    .ThenInclude(x => x.JobSectorPlant.SectorPlant.Sector)
                .Include(x => x.EmitterUser)
                    .ThenInclude(x => x.JobSectorPlant.SectorPlant.Plant)
                .Include(x => x.ResponsibleUser)
                .Include(x => x.FindingType)
                .Include(x => x.FindingState)
                .Include(x => x.FindingComments)
                .Include(x => x.SectorPlantTreatment)
                .ThenInclude(y => y.Sector)
                .Include(x => x.SectorPlantTreatment)
                .ThenInclude(y => y.Plant)
                .Where(f => f.WorkflowId != null);


        }
        public List<FindingComment> GetFindingComments(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var finding = _ctx.Findings
                    .Include(x => x.FindingComments)
                    .Where(x => x.FindingID == id).FirstOrDefault();

                return _mapper.Map<List<Entities.Quality.FindingComments>, List<FindingComment>>(finding.FindingComments.ToList());
            }
        }
        public int GetCount()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                return _ctx.Findings.Count();
            }
        }
        public FindingWorkflowData Add(FindingWorkflowData finding)
        {
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                Findings mappedFinding = _mapper.Map<FindingWorkflowData, Findings>(finding);

                Findings createdFinding = _ctx.Findings.Add(mappedFinding).Entity;
                _ctx.SaveChanges();

                return _mapper.Map<Findings, FindingWorkflowData>(createdFinding);
            }
        }

        public bool setWorkflowID (int id, string workflowid)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var finding = _ctx.Findings.Where(x => x.FindingID == id).FirstOrDefault();
                finding.WorkflowId = workflowid;
                finding.CreatedDate = DateTime.Now;
                _ctx.Findings.Update(finding);
                _ctx.SaveChanges();
                return true;
            }    
        }

        public FindingWorkflowData GetOneByWorkflowId(string workflowId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _mapper.Map<Findings, FindingWorkflowData>(_ctx.Findings.Where(x => x.WorkflowId == workflowId)
                    .Include(x => x.FindingState)
                    .Include(x => x.ResponsibleUser)
                    .Include(x => x.FindingType)
                    .Include(x => x.FindingComments)
                    .Include(x => x.SectorPlantLocation)
                        .ThenInclude(x => x.Sector)
                    .Include(x => x.SectorPlantTreatment)
                       .ThenInclude(y => y.Sector)
                    .Include(x => x.SectorPlantTreatment)
                       .ThenInclude(y => y.Plant)
                    .Include(x => x.EmitterUser)
                       .ThenInclude(x => x.JobSectorPlant.SectorPlant.Sector)
                    .Include(x => x.EmitterUser)
                       .ThenInclude(x => x.JobSectorPlant.SectorPlant.Plant)
                    .FirstOrDefault());
        
            }
        }

        public string GetResponsibleUserEmail(string responsibleUserId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.Users.Find(responsibleUserId).Email;
            }

        }

        public string GetFindingTypeName(int findingTypeId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.FindingTypes.Find(findingTypeId).Name;
            }
        }

        public FindingWorkflowData Update(FindingWorkflowData finding)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                
                var findingData = _ctx.Findings.Where(x => x.FindingID == finding.FindingID).FirstOrDefault();
                //if (findingData.AuditID != 0)
                //{
                //    finding.AuditID = Convert.ToInt32(findingData.AuditID);
                //    finding.AspectID = Convert.ToInt32(findingData.AspectID);
                //    finding.StandardID = Convert.ToInt32(findingData.StandardID);
                //}
                findingData.Description =   finding.Description;
                findingData.PlantLocationID = finding.PlantLocationID == 0 ? findingData.PlantLocationID : finding.PlantLocationID;
                findingData.SectorLocationID = finding.SectorLocationID == 0 ? findingData.SectorLocationID : finding.SectorLocationID;
                findingData.PlantTreatmentID =  finding.PlantTreatmentID == 0 ? findingData.PlantTreatmentID : finding.PlantTreatmentID;
                findingData.SectorTreatmentID = finding.SectorTreatmentID == 0 ? findingData.SectorTreatmentID : finding.SectorTreatmentID;
                findingData.ResponsibleUserID = finding.ResponsibleUserID;
                findingData.FindingTypeID = finding.FindingTypeID;
                findingData.ExpirationDate = finding.ExpirationDate;
                findingData.FindingStateID = finding.FindingStateID;
                findingData.ContainmentAction = finding.ContainmentAction;
                findingData.CauseAnalysis = finding.CauseAnalysis;
                findingData.FinalComment = finding.FinalComment;
                //Findings f = _mapper.Map<FindingWorkflowData, Findings>(finding);
                if (!string.IsNullOrWhiteSpace(finding.Comment))
                {
                    Entities.Quality.FindingComments fc = new Entities.Quality.FindingComments();
                    fc.Date = DateTime.Now;
                    fc.Comment = finding.Comment;
                    fc.FindingID = findingData.FindingID;
                    fc.CreatedByUserID = findingData.ResponsibleUserID;
                    findingData.FindingComments.Add(fc);
                }
                _ctx.Findings.Update(findingData);
                _ctx.SaveChanges();

                return _mapper.Map<Findings, FindingWorkflowData>(findingData);
            }
        }

        public bool UpdateExpirationDate(Finding finding)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                Findings f = _mapper.Map<Finding, Findings>(finding);

                var updateOperation = _ctx.Findings.Attach(f);
                updateOperation.Property(x => x.ExpirationDate).IsModified = true;
                updateOperation.Property(x => x.FindingStateID).IsModified = true;
                _ctx.SaveChanges();
                return true;
            }
        }

        public bool Update(Finding finding)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var findingToUpdate = _mapper.Map<Finding, Findings>(finding);

                _ctx.Update(findingToUpdate);
                _ctx.SaveChanges();

                return true;
            }
        }

        public FindingWorkflowData UpdateIsInProcessWorkflow(int findingID, bool isInProcessWorkflow)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var finding = _ctx.Findings.Where(x => x.FindingID == findingID)
                    .Include(x =>x.FindingState)
                    .Include(x => x.ResponsibleUser)
                    .Include(x => x.FindingType)
                    .Include(x => x.FindingComments)
                    .Include(x => x.SectorPlantLocation)
                        .ThenInclude(x => x.Sector)
                    .Include(x => x.SectorPlantTreatment)
                       .ThenInclude(y => y.Sector)
                    .Include(x => x.SectorPlantTreatment)
                       .ThenInclude(y => y.Plant)
                    .Include(x => x.EmitterUser)
                       .ThenInclude(x => x.JobSectorPlant.SectorPlant.Sector)
                    .Include(x => x.EmitterUser)
                       .ThenInclude(x => x.JobSectorPlant.SectorPlant.Plant)
                    .FirstOrDefault();
                finding.IsInProcessWorkflow = isInProcessWorkflow;
                _ctx.Update(finding);
                _ctx.SaveChanges();

                return _mapper.Map<Findings, FindingWorkflowData>(finding);
            }
        }

        public bool Delete(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var findingToDelete = _ctx.Findings.Where(x => x.FindingID == id).FirstOrDefault();
                _ctx.Findings.Remove(findingToDelete);
                _ctx.SaveChanges();

                return true;
            }
        }

        public List<FindingWorkflowData> GetAllByAuditID(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _auditStandardAspectRepository = scope.ServiceProvider.GetService(typeof(Hoshin.Quality.Application.Repositories.IAuditStandardAspectRepository)) as Hoshin.Quality.Application.Repositories.IAuditStandardAspectRepository;
                var auditStandardAspects = _auditStandardAspectRepository.GetAllforAudit(id);
                var findings = new List<Findings>();
                foreach (var auditStandardAspect in auditStandardAspects)
                {
                    if (!(auditStandardAspect.NoAudited) && !(auditStandardAspect.WithoutFindings))
                    {
                        var finding = _ctx.Findings.Where(x => x.AuditID == auditStandardAspect.AuditID && x.StandardID == auditStandardAspect.StandardID && x.AspectID == auditStandardAspect.AspectID).ToList();
                        findings.AddRange(finding);
                    }
                }
                return _mapper.Map<List<Findings>, List<FindingWorkflowData>>(findings);    
            }
        }
    }
}
