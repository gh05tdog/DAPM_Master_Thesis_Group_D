﻿using DAPM.PipelineOrchestratorMS.Api.Models;
using RabbitMQLibrary.Models;

namespace DAPM.PipelineOrchestratorMS.Api.Engine.Interfaces
{
    public interface IPipelineOrchestrationEngine
    {
        public Guid CreatePipelineExecutionInstance(PipelineDTO pipelineDTO);
        public void ExecutePipelineStartCommand(Guid executionId);
        public List<Guid> GetPipelineExecutions(Guid pipelineId);
        public PipelineExecutionStatus GetPipelineExecutionStatus(Guid executionId);
        public void ProcessActionResult(ActionResultDTO actionResultDto);
    }
}
