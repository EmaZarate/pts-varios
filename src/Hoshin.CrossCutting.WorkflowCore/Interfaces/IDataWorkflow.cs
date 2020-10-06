namespace Hoshin.CrossCutting.WorkflowCore.Interfaces
{
    public interface IDataWorkflow
    {
        string WorkflowId { get; set; }
        string Flow { get; set; }
        int FlowVersion { get; set; }
        //object EventData { get; set; }
        string EventData { get; set; }
    }
}
