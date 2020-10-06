using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Mapper
{
    public class WorkflowDataToEntityMapper : Profile
    {
        public WorkflowDataToEntityMapper()
        {
            CreateMap<FindingWorkflowData, Findings>();
            CreateMap<AuditWorkflowData, Audits>()
                .ForMember(x => x.AuditStandards, y => y.Ignore());
            CreateMap<AuditStandardAspects, AuditStandardAspect>();
            CreateMap<CorrectiveActionWorkflowData, CorrectiveActions>();
        }
    }
}
