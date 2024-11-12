using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;

namespace DAPM.Orchestrator.Consumers.StartProcessConsumers
{
    public class GetPipelineExecutionsRequestConsumer : IQueueConsumer<GetPipelineExecutionsRequest>
    {

        IOrchestratorEngine _engine;
        public GetPipelineExecutionsRequestConsumer(IOrchestratorEngine engine)
        {
            _engine = engine;
        }
        public Task ConsumeAsync(GetPipelineExecutionsRequest message)
        {
            _engine.StartGetPipelineExecutionsProcess(message.TicketId);
            return Task.CompletedTask;
        }
    }
}