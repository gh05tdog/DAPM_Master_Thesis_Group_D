using DAPM.PipelineOrchestratorMS.Api.Engine.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPipelineOrchestrator;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using RabbitMQLibrary.Models;

namespace DAPM.PipelineOrchestratorMS.Api.Consumers
{
    public class GetPipelineExecutionStatusConsumer : IQueueConsumer<GetPipelineExecutionStatusMessage>
    {
        private IPipelineOrchestrationEngine _pipelineOrchestrationEngine;
        private IQueueProducer<GetPipelineExecutionStatusResultMessage> _getPipelineExecutionStatusResultProducer;
        public ILogger<GetPipelineExecutionStatusConsumer> _logger;

        public GetPipelineExecutionStatusConsumer(IPipelineOrchestrationEngine pipelineOrchestrationEngine,
            IQueueProducer<GetPipelineExecutionStatusResultMessage> getPipelineExecutionStatusResultProducer,
            ILogger<GetPipelineExecutionStatusConsumer> logger)
        {
            _pipelineOrchestrationEngine = pipelineOrchestrationEngine;
            _getPipelineExecutionStatusResultProducer = getPipelineExecutionStatusResultProducer;
            _logger = logger;
        }

        public Task ConsumeAsync(GetPipelineExecutionStatusMessage message)
        {
            var status = _pipelineOrchestrationEngine.GetPipelineExecutionStatus(message.ExecutionId);
            _logger.LogInformation($"Pipeline execution status retrieved for execution id {message.ExecutionId}");
            _logger.LogInformation($"Pipeline execution status: {status.State}");

            var currentStepsDtos = new List<StepStatusDTO>();
            foreach(var step in status.CurrentSteps)
            {
                var stepDto = new StepStatusDTO()
                {
                    StepId = step.StepId,
                    ExecutionerPeer = step.ExecutionerPeer,
                    ExecutionTime = step.ExecutionTime,
                    StepType = step.StepType,
                };
                currentStepsDtos.Add(stepDto);
            }


            var pipelineExecutionStatusDto = new PipelineExecutionStatusDTO()
            {
                ExecutionTime = status.ExecutionTime,
                State = status.State.ToString(),
                CurrentSteps = currentStepsDtos,
            };

            var getPipelineExecutionStatusResultMessage = new GetPipelineExecutionStatusResultMessage()
            {
                ProcessId = message.ProcessId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Status = pipelineExecutionStatusDto
            };

            _getPipelineExecutionStatusResultProducer.PublishMessage(getPipelineExecutionStatusResultMessage);
            return Task.CompletedTask;
        }
    }
}
