using Hoshin.CrossCutting.SignalR;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CloseFinding
{
    public class CloseFindingUseCase : ICloseFindingUseCase
    {
        private readonly IWorkflowCore _workflowCore;
        private readonly IFindingRepository _findingRepository;
        private IHubContext<FindingsHub> _hub;

        public CloseFindingUseCase(
            IWorkflowCore workflowCore,
            IFindingRepository findingRepository,
            IHubContext<FindingsHub> hub)
        {
            _workflowCore = workflowCore;
            _findingRepository = findingRepository;
            _hub = hub;
        }
        public async Task<bool> Execute(FindingWorkflowData finding)
        {
            //_findingRepository.Update(finding);
            try
            {
                var findingWorkflow = _findingRepository.UpdateIsInProcessWorkflow(finding.FindingID, true);
                await _workflowCore.ExecuteEvent("Close", finding.WorkflowId, finding);
                await _hub.Clients.All.SendAsync("transferfindingsdata", findingWorkflow);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
