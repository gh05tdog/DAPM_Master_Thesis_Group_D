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

            List<Guid> executions = _pipelineOrchestrationEngine.GetPipelineExecutions(message.PipelineId);

            var pipelineExecutionDto = new PipelineExecutionDTO()
                {
                    ExecutionIds = new List<Guid>(),
                    PipelineId = message.PipelineId /*execution.Value.Pipeline*/,
                };            
            foreach (var execution in executions)
            {
                pipelineExecutionDto.ExecutionIds.Add(execution);
            }
            
            var pipelineExecutionList = new List<PipelineExecutionDTO>();
            pipelineExecutionList.Add(pipelineExecutionDto);

            var resultMessage = new GetPipelineExecutionsResultMessage()
            {
                ProcessId = message.ProcessId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Succeeded = true,
                Executions = pipelineExecutionList
            };

            _getPipelineExecutionsProducer.PublishMessage(resultMessage);

            return Task.CompletedTask;
        }
    }
}
