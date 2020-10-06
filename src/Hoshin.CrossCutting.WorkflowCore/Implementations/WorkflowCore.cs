using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Hoshin.CrossCutting.WorkflowCore.Implementations
{
    public class WorkflowCore : IWorkflowCore
    {
        #region Members

        private readonly IWorkflowRegistry _registry;
        private readonly IPersistenceProvider _workflowStore;
        private readonly IWorkflowHost _host;
        private readonly IDefinitionLoader _loader;
        private readonly IServiceProvider _serviceProvider;
        private readonly Workflows.Workflows _workflows;

        #endregion

        #region Constructor

        public WorkflowCore(IWorkflowRegistry registry, IPersistenceProvider persistenceProvider, IServiceProvider serviceProvider, IOptions<Workflows.Workflows> workflows)
        {
            _serviceProvider = serviceProvider;
            _host = serviceProvider.GetService<IWorkflowHost>();
            _registry = registry;
            _workflowStore = persistenceProvider;
            _loader = serviceProvider.GetService<IDefinitionLoader>();
            _workflows = workflows.Value;
            _host.Start();
            this.RegisterWorkflow();
        }

        #endregion

        #region Methods

        public string RegisterWorkflowByJson(object jsonFlow)
        {
            try
            {
                _loader.LoadDefinition(_workflows.FindingFlow.ToString());
                _loader.LoadDefinition(_workflows.AuditFlow.ToString());
                _loader.LoadDefinition(_workflows.CorrectiveActionFlow.ToString());
                return "Flujo registrado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> DeleteWorkflowInstance(string workflowId)
        {
            try
            {
                await _host.TerminateWorkflow(workflowId);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string RegisterWorkflow()
        {
            try
            {
                _loader.LoadDefinition(_workflows.FindingFlow.ToString());
                _loader.LoadDefinition(_workflows.AuditFlow.ToString());
                _loader.LoadDefinition(_workflows.CorrectiveActionFlow.ToString());
                return "Flujo registrado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public async Task<string> StartFlow<TDataWorkflow>(TDataWorkflow tDataWorkflow) where TDataWorkflow : class, IDataWorkflow, new()
        {
            return await _host.StartWorkflow(tDataWorkflow.Flow, tDataWorkflow.FlowVersion, tDataWorkflow);
        }

        public async Task ExecuteEvent(string eventName, string workflowId, string eventData)
        {
            await _host.PublishEvent(eventName, workflowId, eventData);
        }

        public async Task<TDataWorkflow> GetFlowInstance<TDataWorkflow>(string instanceId) where TDataWorkflow : class, IDataWorkflow, new()
        {
            return await _workflowStore.GetWorkflowInstance(instanceId) as TDataWorkflow;
        }

        public async Task ExecuteEvent(string eventName, string workflowId, object eventData)
        {
            await _host.PublishEvent(eventName, workflowId, eventData);
        }

        #endregion
    }
}
