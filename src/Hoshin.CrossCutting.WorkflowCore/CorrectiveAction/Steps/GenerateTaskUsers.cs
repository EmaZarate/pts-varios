using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Steps
{
    public class GenerateTaskUsers : StepBody
    {
        const string STATE_TASK_CODE = "NIN";

        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IUserWorkflowRepository _userWorkflowRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskStateRepository _taskStateRepository;
        private readonly EmailSettings _emailSettings;

        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<string> EmailAddresses { get; set; }
        public GenerateTaskUsers(
            ICorrectiveActionRepository correctiveActionRepository,
            ICorrectiveActionStateRepository correctiveActionStateRepository,
            IUserWorkflowRepository userWorkflowRepository,
            ITaskRepository taskRepository,
            ITaskStateRepository taskStateRepository,
            IOptions<EmailSettings> emailSettings)
        {
            EmailAddresses = new List<string>();
            _correctiveActionRepository = correctiveActionRepository;
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _userWorkflowRepository = userWorkflowRepository;
            _taskRepository = taskRepository;
            _taskStateRepository = taskStateRepository;
            _emailSettings = emailSettings.Value;
        }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var correctiveAction = _correctiveActionRepository.GetOneByWorkflowId(context.Workflow.Id);
            var newTaskStateID = _taskStateRepository.GetIdByCode(STATE_TASK_CODE);

            _correctiveActionRepository.ChangeTasksState(correctiveAction.CorrectiveActionID, newTaskStateID);

            List<TaskWorkflowData> correctiveActionsTasks = _taskRepository.GetAllForCorrectiveActionWorkflow(correctiveAction.CorrectiveActionID);

            EmailAddresses.AddRange(_userWorkflowRepository.GetUsersEmailResponsibleSGC());
            EmailAddresses.AddRange(_correctiveActionRepository.GetEmailOfTasksResposibles(correctiveAction.CorrectiveActionID));

            var emailType = "generatetask";

            this.EmailSubject = EmailStrings.GetSubjectCorrectiveAction(emailType);
            this.EmailMessage = EmailStrings.GetMessageCorrectiveActionTasks(correctiveAction, correctiveActionsTasks, _emailSettings.Url);

            return ExecutionResult.Next();
        }
    }
}
