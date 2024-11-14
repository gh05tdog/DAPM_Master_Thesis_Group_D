using DAPM.PipelineOrchestratorMS.Api.Engine.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPipelineOrchestrator;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using RabbitMQLibrary.Models;

namespace DAPM.PipelineOrchestratorMS.Api.Consumers
{
    public class GetPipelineExecutionsConsumer : IQueueConsumer<GetPipelineExecutionsMessage>
    {

        private ILogger<GetPipelineExecutionsConsumer> _logger;
        private IPipelineOrchestrationEngine _pipelineOrchestrationEngine;
        private IQueueProducer<GetPipelineExecutionsResultMessage> _getPipelineExecutionsProducer;

        public GetPipelineExecutionsConsumer(ILogger<GetPipelineExecutionsConsumer> logger, 
            IPipelineOrchestrationEngine pipelineOrchestrationEngine,
            IQueueProducer<GetPipelineExecutionsResultMessage> getPipelineExectuionResultProducer)
        {
            _logger = logger;
            _pipelineOrchestrationEngine = pipelineOrchestrationEngine;
            _getPipelineExecutionsProducer = getPipelineExectuionResultProducer;
        }
        public Task ConsumeAsync(GetPipelineExecutionsMessage message)
        {
            _logger.LogInformation("GetPipelineExecutionsMessage received");

            var executions = _pipelineOrchestrationEngine.GetPipelineExecutions(/*message.Pipeline.Pipeline*/);

            var pipelineExecutionsDtos = new PipelineExecutionsDTO();
            
            foreach (var execution in executions)
            {
                var pipelineExecutionDto = new PipelineExecutionDTO()
                {
                    ExecutionId = execution.Key,
                    PipelineId = "whatever"/*execution.Value.Pipeline*/,
                };
                pipelineExecutionsDtos.PipelineExecutions.Add(pipelineExecutionDto);
            }
            


            var resultMessage = new GetPipelineExecutionsResultMessage()
            {
                ProcessId = message.ProcessId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Succeeded = true,
                Executions = pipelineExecutionsDtos
            };

            _getPipelineExecutionsProducer.PublishMessage(resultMessage);

            return Task.CompletedTask;
        }
    }
}
