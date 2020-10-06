using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Steps
{
    public class WaitForCustom: StepBody
    {
        public string EventKey { get; set; }

        public string EventName { get; set; }

        public DateTime EffectiveDate { get; set; }

        public object EventData { get; set; }


        public override ExecutionResult Run(IStepExecutionContext context)
        {
            if (!context.ExecutionPointer.EventPublished)
            {
                DateTime effectiveDate = DateTime.MinValue;
                // TODO: This will always execute.
                if (EffectiveDate != null)
                {
                    effectiveDate = EffectiveDate;
                }

                return ExecutionResult.WaitForEvent(EventName, EventKey, effectiveDate);
            }

            EventData = (context.ExecutionPointer.EventData as CorrectiveActionWorkflowData).EventData;
            context.Workflow.Data = context.ExecutionPointer.EventData;
            return ExecutionResult.Next();
        }
    }
}
