using DAPM.PipelineOrchestratorMS.Api.Engine.Interfaces;
using DAPM.PipelineOrchestratorMS.Api.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Models;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.PipelineOrchestratorMS.Api.Engine
{
    public class PipelineOrchestrationEngine : IPipelineOrchestrationEngine
    {

        private readonly ILogger<IPipelineOrchestrationEngine> _logger;
        private IServiceProvider _serviceProvider;
        private Dictionary<Guid, IPipelineExecution> _pipelineExecutions;

        public PipelineOrchestrationEngine(ILogger<IPipelineOrchestrationEngine> logger, IServiceProvider serviceProvider)
        {
            _pipelineExecutions = new Dictionary<Guid, IPipelineExecution>();
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Guid CreatePipelineExecutionInstance(PipelineDTO pipelineDTO)
        {
            //outout the pipeline
            Guid guid = Guid.NewGuid();
            _logger.LogInformation($"Creating a new pipeline execution instance with nodes: " + pipeline.Nodes.Count());
            _logger.LogInformation($"Creating a new pipeline execution instance with edges: " + pipeline.Edges.Count());
            
            foreach (var node in pipeline.Nodes)
            {
                _logger.LogInformation($"Node: " + node?.Type);
                _logger.LogInformation($"Node: " + node?.Data?.InstantiationData?.Resource.Name);
                _logger.LogInformation($"Node: " + node?.Data?.InstantiationData?.Repository.name);
                _logger.LogInformation($"Node: " + node?.Data?.InstantiationData?.Organization.name);
                _logger.LogInformation($"Node: " + node?.Data?.InstantiationData?.Algorithm.Name);
            }

            var pipelineExecution = new PipelineExecution(guid, pipelineDTO, _serviceProvider);
            
            _pipelineExecutions[guid] = pipelineExecution;
            _logger.LogInformation($"A new execution instance has been created");
            
            return guid;
        }

        public void ProcessActionResult(ActionResultDTO actionResultDto)
        {
            var execution = GetPipelineExecution(actionResultDto.ExecutionId);
            execution.ProcessActionResult(actionResultDto);
        }

        public void ExecutePipelineStartCommand(Guid executionId)
        {
            var execution = GetPipelineExecution(executionId);
            execution.StartExecution();
        }


        private IPipelineExecution GetPipelineExecution(Guid executionId)
        {
            return _pipelineExecutions[executionId];
        }

        public List<Guid> GetPipelineExecutions(Guid pipelineId)
        {
            List<Guid>_executionsID = new List<Guid>();
            _logger.LogInformation("GetPipelineExecutions called");
            foreach (var execution in _pipelineExecutions)
            {
                _logger.LogInformation($"Execution: {execution.Key}");
                if (execution.Value.GetPipelineId() == pipelineId)
                {
                    _logger.LogInformation($"Execution: {execution.Key} is for pipeline {pipelineId}");
                    _executionsID.Add(execution.Key);
                }
            }

            return _executionsID;
        }

        public PipelineExecutionStatus GetPipelineExecutionStatus(Guid executionId)
        {
            return _pipelineExecutions[executionId].GetStatus();
        }
    }
}
