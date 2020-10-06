using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.GenericSteps
{
    public class ChangeState : StepBody
    {
        public string OldState { get; set; }
        public string NewState { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            OldState = NewState;

            return ExecutionResult.Next();
        }
    }
}
