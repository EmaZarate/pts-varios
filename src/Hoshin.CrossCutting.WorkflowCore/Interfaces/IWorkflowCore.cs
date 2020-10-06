using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.WorkflowCore.Interfaces
{
    public interface IWorkflowCore
    {
        //string RegisterWorkflow();
        string RegisterWorkflowByJson(object jsonFlow);
        //Task<TDataWorkflow> StartFlow<TDataWorkflow>(TDataWorkflow tDataWorkflow) where TDataWorkflow : class, IDataWorkflow, new();
        Task<string> StartFlow<TDataWorkflow>(TDataWorkflow tDataWorkflow) where TDataWorkflow : class, IDataWorkflow, new();
        Task ExecuteEvent(string eventName, string workflowId, string eventData);
        Task ExecuteEvent(string eventName, string workflowId, object eventData);
        Task<TDataWorkflow> GetFlowInstance<TDataWorkflow>(string instanceId) where TDataWorkflow : class, IDataWorkflow, new();
        Task<bool> DeleteWorkflowInstance(string workflowId);


    }
}
