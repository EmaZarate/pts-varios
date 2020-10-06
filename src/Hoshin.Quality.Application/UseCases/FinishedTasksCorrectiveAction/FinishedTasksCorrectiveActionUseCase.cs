using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.FinishedTasksCorrectiveAction
{
    public class FinishedTasksCorrectiveActionUseCase : IFinishedTasksCorrectiveActionUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskStateRepository _taskStateRepository;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly IWorkflowCore _workflowCore;

        public FinishedTasksCorrectiveActionUseCase(
            ITaskRepository taskRepository, 
            ITaskStateRepository taskStateRepository,
            ICorrectiveActionRepository correctiveActionRepository,
            IWorkflowCore workflowCore
            )
        {
            _taskRepository = taskRepository;
            _taskStateRepository = taskStateRepository;
            _correctiveActionRepository = correctiveActionRepository;
            _workflowCore = workflowCore;
        }

        public async Task Execute(int correctiveActionId)
        {
            var correctiveAction = _correctiveActionRepository.GetOne(correctiveActionId);
            var correctiveActionTasks = _taskRepository.GetAllForCorrectiveActionWithOutStates(correctiveActionId);
            var taskStateCompletedId = _taskStateRepository.GetIdByCode("COM");
            var quantityWithoutFinish = correctiveActionTasks.Where(t => t.TaskStateID != taskStateCompletedId).ToList().Count;

            if(quantityWithoutFinish == 0)
            {
                await _workflowCore.ExecuteEvent("FinishedTasks", correctiveAction.WorkflowId, new CorrectiveActionWorkflowData());
            }
        }
    }
}
