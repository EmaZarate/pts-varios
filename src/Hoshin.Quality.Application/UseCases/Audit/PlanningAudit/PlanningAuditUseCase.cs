using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Audit.PlanningAudit
{
    public class PlanningAuditUseCase : IPlanningAuditUseCase
    {
        private readonly IMapper _mapper;
        private readonly IWorkflowCore _workflowCore;
        public PlanningAuditUseCase(IWorkflowCore workflowCore, IMapper mapper)
        {
            _workflowCore = workflowCore;
            _mapper = mapper;
        }
        public async Task<bool> Execute(AuditWorkflowData audit)
        {
            try
            {
                audit.EventData = "ToPlan";
                await _workflowCore.ExecuteEvent("ToPlan", audit.WorkflowId, audit);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
