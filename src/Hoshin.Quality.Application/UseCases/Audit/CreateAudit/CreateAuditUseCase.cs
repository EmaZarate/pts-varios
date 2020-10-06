using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Audit.CreateAudit
{
    public class CreateAuditUseCase: ICreateAuditUseCase
    {
        private readonly IWorkflowCore _workflowCore;
        private readonly IMapper _mapper;

        public CreateAuditUseCase(IWorkflowCore workflowCore, IMapper mapper)
        {
            _workflowCore = workflowCore;
            _mapper = mapper;
        }
        public async Task<AuditWorkflowData> Execute(AuditWorkflowData audit)
        {
            audit.Flow = "Audit";
            audit.FlowVersion = 1;
            var flowId = await _workflowCore.StartFlow(audit);
            return audit;
        }
    }
}
