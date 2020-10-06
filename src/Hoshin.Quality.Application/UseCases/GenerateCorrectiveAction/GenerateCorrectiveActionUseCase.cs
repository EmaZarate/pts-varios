using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Hoshin.Quality.Application.UseCases.GenerateCorrectiveAction
{
    public class GenerateCorrectiveActionUseCase : IGenerateCorrectiveActionUseCase
    {
        private readonly IWorkflowCore _workflowCore;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenerateCorrectiveActionUseCase(IWorkflowCore workflowCore, IHttpContextAccessor httpContextAccessor)
        {
            _workflowCore = workflowCore;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Execute(CorrectiveActionWorkflowData correctiveAction)
        {
            correctiveAction.EmitterUserID = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            await _workflowCore.ExecuteEvent("FinishedAnalysis", correctiveAction.WorkflowId, correctiveAction);
        }
    }
}
