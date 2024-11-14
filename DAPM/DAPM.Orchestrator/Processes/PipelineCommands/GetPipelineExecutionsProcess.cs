using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPipelineOrchestrator;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using RabbitMQLibrary.Messages.Repository;

namespace DAPM.Orchestrator.Processes.PipelineCommands
{
    public class GetPipelineExecutionsProcess : OrchestratorProcess
    {

        private ILogger<OrchestratorEngine> _logger;
        private Guid _ticketId;

        public GetPipelineExecutionsProcess(OrchestratorEngine engine, ILogger<OrchestratorEngine> logger,IServiceProvider serviceProvider,
            Guid ticketId, Guid processId) : base(engine, serviceProvider, processId)
        {
            _ticketId = ticketId;
            _logger = logger;
        }

        public override void StartProcess()
        {
            var getPipelineExecutionsProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetPipelineExecutionsMessage>>();

            var message = new GetPipelineExecutionsMessage()
            {
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
            };
            _logger.LogInformation("Sending GetPipelineExecutionsMessage");

            getPipelineExecutionsProducer.PublishMessage(message);
        }


        public override void OnGetPipelineExecutionsResult(GetPipelineExecutionsResultMessage message)
        {
            var getPipelineExecutionsResultProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<GetPipelineExecutionsRequestResult>>();

            var resultMessage = new GetPipelineExecutionsRequestResult()
            {
                TicketId = _ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Executions = message.Executions
            };
            _logger.LogInformation("Sending GetPipelineExecutionsRequestResult");
            getPipelineExecutionsResultProducer.PublishMessage(resultMessage);
        }
    }
}
