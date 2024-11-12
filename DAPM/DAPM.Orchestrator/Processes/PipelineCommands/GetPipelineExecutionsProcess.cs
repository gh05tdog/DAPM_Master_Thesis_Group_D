using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPipelineOrchestrator;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using RabbitMQLibrary.Messages.Repository;

namespace DAPM.Orchestrator.Processes.PipelineCommands
{
    public class GetPipelineExecutionsProcess : OrchestratorProcess
    {

        private Guid _ticketId;

        public GetPipelineExecutionsProcess(OrchestratorEngine engine, IServiceProvider serviceProvider,
            Guid ticketId, Guid processId) : base(engine, serviceProvider, processId)
        {
            _ticketId = ticketId;
        }

        public override void StartProcess()
        {
            var getPipelineExecutionsProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetPipelineExecutionsMessage>>();

            var message = new GetPipelineExecutionsMessage()
            {
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
            };

            getPipelineExecutionsProducer.PublishMessage(message);
        }


        public override void OnGetPipelineExecutionsResult(GetPipelineExecutionsResultMessage message)
        {
            var getPipelineExecutionsResultProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetPipelineExecutionsRequestResult>>();

            var resultMessage = new GetPipelineExecutionsRequestResult()
            {
                TicketId = _ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Status = message.Status,

            };

            getPipelineExecutionsResultProducer.PublishMessage(resultMessage);
        }
    }
}
