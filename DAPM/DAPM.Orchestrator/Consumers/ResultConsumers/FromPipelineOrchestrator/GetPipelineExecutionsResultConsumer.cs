using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPipelineOrchestrator;

namespace DAPM.Orchestrator.Consumers.ResultConsumers.FromPipelineOrchestrator
{
    public class GetPipelineExecutionsResultConsumer : IQueueConsumer<GetPipelineExecutionsResultMessage>
    {
        private IOrchestratorEngine _orchestratorEngine;

        public GetPipelineExecutionsResultConsumer(IOrchestratorEngine orchestratorEngine)
        {
            _orchestratorEngine = orchestratorEngine;
        }

        public Task ConsumeAsync(GetPipelineExecutionsResultMessage message)
        {
            OrchestratorProcess process = _orchestratorEngine.GetProcess(message.ProcessId);
            process.OnGetPipelineExecutionsResult(message);

            return Task.CompletedTask;
        }
    }
}
