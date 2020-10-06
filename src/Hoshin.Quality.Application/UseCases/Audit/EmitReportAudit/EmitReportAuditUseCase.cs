using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Domain.Audit;

namespace Hoshin.Quality.Application.UseCases.Audit.EmitReportAudit
{
    public class EmitReportAuditUseCase : IEmitReportAuditUseCase
    {
        private readonly IMapper _mapper;
        private readonly IWorkflowCore _workflowCore;
        public EmitReportAuditUseCase(IWorkflowCore workflowCore, IMapper mapper)
        {
            _workflowCore = workflowCore;
            _mapper = mapper;
        }
        public async Task<bool> Execute(AuditWorkflowData audit)
        {
            try
            {
                audit.EventData = "ToEmmit";
                await _workflowCore.ExecuteEvent("ToEmmit", audit.WorkflowId, audit);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
