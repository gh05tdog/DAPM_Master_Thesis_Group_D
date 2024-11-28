using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;

namespace DAPM.Orchestrator.Consumers.StartProcessConsumers
{
    public class GetPipelineExecutionStatusRequestConsumer : IQueueConsumer<GetPipelineExecutionStatusRequest>
    {

        IOrchestratorEngine _engine;
        ILogger<GetPipelineExecutionStatusRequestConsumer> _logger;
        public GetPipelineExecutionStatusRequestConsumer(IOrchestratorEngine engine, ILogger<GetPipelineExecutionStatusRequestConsumer> logger)
        {
            _engine = engine;
            _logger = logger;
        }
        public Task ConsumeAsync(GetPipelineExecutionStatusRequest message)
        {
            _logger.LogInformation("GetPipelineExecutionStatusRequest received");
            _engine.StartGetPipelineExecutionStatusProcess(message.TicketId, message.ExecutionId);
            return Task.CompletedTask;
        }
    }
}
