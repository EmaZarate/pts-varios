using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain;
using Hoshin.Quality.Domain.Finding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class AuditStandardAspectRepository : IAuditStandardAspectRepository, CrossCutting.WorkflowCore.Repositories.IAuditStandardAspectRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public AuditStandardAspectRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public List<AuditStandardAspect> GetAllforAudit(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                var audit = _ctx
                    .AuditStandardAspects
                    .Include(x => x.Aspect)
                    .Include(x => x.Findings)
                        .ThenInclude(y => y.FindingType)
                    .Where(x => x.AuditID == id)
                    .ToList();
                return _mapper.Map<List<Entities.Quality.AuditStandardAspect>, List<AuditStandardAspect>>(audit);
            }
        }

        public AuditStandardAspect Add(AuditStandardAspect auditStandardAspect)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditStandardAspectRepo = _mapper.Map<AuditStandardAspect, Entities.Quality.AuditStandardAspect>(auditStandardAspect);

                _ctx.AuditStandardAspects.Add(auditStandardAspectRepo);
                _ctx.SaveChanges();

                return _mapper.Map<Entities.Quality.AuditStandardAspect, AuditStandardAspect>(auditStandardAspectRepo);
            }
        }

        public List<AuditStandardAspects> AddRange(List<AuditStandardAspects> auditStandardAspect)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditStandardAspectRepo = _mapper.Map<List<AuditStandardAspects>, List<Entities.Quality.AuditStandardAspect>>(auditStandardAspect);

                _ctx.AuditStandardAspects.AddRange(auditStandardAspectRepo);
                _ctx.SaveChanges();

                return _mapper.Map<List<Entities.Quality.AuditStandardAspect>, List<AuditStandardAspects>>(auditStandardAspectRepo);
            }
        }

        public bool AddFinding(Finding finding)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var findingStateID = _ctx.FindingsStates.Where(x => x.Code == "ESP").FirstOrDefault().FindingStateID;

                var auditStandardAspect =
                    _ctx.AuditStandardAspects
                        .Include(x => x.Findings)
                        .Where(x => x.AuditID == finding.AuditID && x.StandardID == finding.StandardID && x.AspectID == finding.AspectID)
                        .FirstOrDefault();

                if(auditStandardAspect.AspectStateID == 18) //ESTADO PENDIENTE
                {
                    auditStandardAspect.AspectStateID = 19; //ESTADO REVISADO
                }

                auditStandardAspect.NoAudited = false;
                auditStandardAspect.WithoutFindings = false;
                
                var mappedFinding = _mapper.Map<Finding, Entities.Quality.Findings>(finding);
                mappedFinding.FindingStateID = findingStateID;

                var findingAdded = _ctx.Findings.Add(mappedFinding);
                auditStandardAspect.Findings.Add(findingAdded.Entity);

                this.SetAuditInitiated(auditStandardAspect.AuditID, _ctx);

                _ctx.Update(auditStandardAspect);
                _ctx.SaveChanges();

                return true;
            }
        }

        public AuditStandardAspect Get(int auditId, int standardId, int aspectId)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;


                var auditStandardAspect = _ctx
                    .AuditStandardAspects
                    .Include(x => x.Findings)
                    .Where(x => x.AuditID == auditId && x.StandardID == standardId && x.AspectID == aspectId)
                    .FirstOrDefault();

                return _mapper.Map<Entities.Quality.AuditStandardAspect, AuditStandardAspect>(auditStandardAspect);
            }
        }

        public void SetPendingState(AuditStandardAspect auditStandardAspect)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditStandardAspectToUpdate = _ctx
                    .AuditStandardAspects
                    .Where(x => 
                            x.AuditID == auditStandardAspect.AuditID && 
                            x.StandardID == auditStandardAspect.StandardID && 
                            x.AspectID == auditStandardAspect.AspectID)
                    .FirstOrDefault();



                auditStandardAspectToUpdate.AspectStateID = _ctx.AspectStates.FirstOrDefault(x => x.Name == "Pendiente" && x.Active == true).AspectStateID;
                _ctx.AuditStandardAspects.Update(auditStandardAspectToUpdate);
                _ctx.SaveChanges();
            }
        }

        public void SetWithoutFinding(AuditStandardAspect auditStandardAspect)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditStandardAspectToUpdate = _ctx
                    .AuditStandardAspects
                    .Include(x => x.Findings)
                    .Where(x =>
                            x.AuditID == auditStandardAspect.AuditID &&
                            x.StandardID == auditStandardAspect.StandardID &&
                            x.AspectID == auditStandardAspect.AspectID)
                    .FirstOrDefault();

                auditStandardAspectToUpdate.Findings.Clear();
                auditStandardAspectToUpdate.WithoutFindings = true;
                auditStandardAspectToUpdate.NoAudited = false;

                this.SetAuditInitiated(auditStandardAspect.AuditID, _ctx);

                _ctx.AuditStandardAspects.Update(auditStandardAspectToUpdate);
                _ctx.SaveChanges();
            }
        }

        public void SetNoAudited(AuditStandardAspect auditStandardAspect)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditStandardAspectToUpdate = _ctx
                    .AuditStandardAspects
                    .Include(x => x.Findings)
                    .Where(x =>
                            x.AuditID == auditStandardAspect.AuditID &&
                            x.StandardID == auditStandardAspect.StandardID &&
                            x.AspectID == auditStandardAspect.AspectID)
                    .FirstOrDefault();

                auditStandardAspectToUpdate.Findings.Clear();
                auditStandardAspectToUpdate.NoAudited = true;
                auditStandardAspectToUpdate.WithoutFindings = false;
                auditStandardAspectToUpdate.Description = auditStandardAspect.Description;

                this.SetAuditInitiated(auditStandardAspect.AuditID, _ctx);

                _ctx.AuditStandardAspects.Update(auditStandardAspectToUpdate);
                _ctx.SaveChanges();
            }
        }

        private void SetAuditInitiated(int auditId, SQLHoshinCoreContext ctx)
        {
            var audit = ctx.Audits.FirstOrDefault((x) => x.AuditID == auditId);
            var stateInitiated = ctx.AuditStates.FirstOrDefault(x => x.Code == "INI").AuditStateID;

            if(audit.AuditStateID != stateInitiated)
            {
                audit.AuditStateID = stateInitiated;
                ctx.Update(audit);
            }
            
        }
    }
}
