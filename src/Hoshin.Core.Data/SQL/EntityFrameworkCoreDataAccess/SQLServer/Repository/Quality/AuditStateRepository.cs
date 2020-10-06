using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AuditState;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class AuditStateRepository : IAuditStateRepository, CrossCutting.WorkflowCore.Repositories.IAuditStateRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public AuditStateRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public AuditState Add(AuditState newAuditState)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                AuditStates auditStateRetriver = ValidateDescriptionAuditState(_ctx, _mapper, newAuditState);
                if (auditStateRetriver is null) return null;
                _ctx.AuditStates.Add(auditStateRetriver);
                _ctx.SaveChanges();
                return _mapper.Map<AuditStates, AuditState>(auditStateRetriver);
            }
        }

        public AuditState Get(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditState = _ctx.AuditStates.Where(x => x.AuditStateID == id).SingleOrDefault();
                return _mapper.Map<AuditStates, AuditState>(auditState);
            }
        }
        public AuditState Get(string code)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditState = _ctx.AuditStates.Where(x => x.Code == code).SingleOrDefault();
                return _mapper.Map<AuditStates, AuditState>(auditState);
            }
        }

        public List<AuditState> GetAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var auditState = _ctx.AuditStates.ToList();
                return _mapper.Map<List<AuditStates>, List<AuditState>>(auditState);
            }
        }

        public int GetOneByCode(string code)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                return _ctx.AuditStates.Where(x => x.Code == code).FirstOrDefault().AuditStateID;
            }
        }

        public AuditState Update(AuditState updateAuditState)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                AuditStates auditStateRetriver = ValidateDescriptionAuditState(_ctx, _mapper, updateAuditState);
                if (auditStateRetriver is null) return null;
                _ctx.AuditStates.Update(auditStateRetriver);
                _ctx.SaveChanges();
                return _mapper.Map<AuditStates, AuditState>(auditStateRetriver);
            }
        }


        private AuditStates ValidateDescriptionAuditState(SQLHoshinCoreContext _ctx, IMapper _mapper, AuditState auditState)
        {

            AuditStates auditStateValidation = _ctx.AuditStates.Where(x => (x.Name == auditState.Name || x.Code == auditState.Code || x.Color == auditState.Color) && x.AuditStateID != auditState.AuditStateID).FirstOrDefault();
            if (auditStateValidation == null)
            {
                return _mapper.Map<AuditState, AuditStates>(auditState);
            }

            return null;
        }
    }
}
