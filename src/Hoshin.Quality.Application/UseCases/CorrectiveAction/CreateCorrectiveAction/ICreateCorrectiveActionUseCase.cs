using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.CreateCorrectiveAction
{
    public interface ICreateCorrectiveActionUseCase
    {
        Task Execute(CorrectiveActionWorkflowData correctiveAction);
    }
}
