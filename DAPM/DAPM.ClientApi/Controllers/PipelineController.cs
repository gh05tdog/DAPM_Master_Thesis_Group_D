using DAPM.ClientApi.AccessControl;
using DAPM.ClientApi.Extensions;
using DAPM.ClientApi.Models;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using Swashbuckle.AspNetCore.Annotations;

namespace DAPM.ClientApi.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("organizations/")]
    public class PipelineController : BaseController
    {
        private readonly IPipelineService pipelineService;

        public PipelineController(IAccessControlService accessControlService, IPipelineService pipelineService) : base(accessControlService)
        {
            this.pipelineService = pipelineService;
        }

        [HttpGet("{organizationId}/repositories/{repositoryId}/pipelines/{pipelineId}")]
        [SwaggerOperation(Description = "Gets a pipeline by id. This endpoint includes the " +
            "pipeline model in JSON. You need to have a collaboration agreement to retrieve this information.")]
        public async Task<ActionResult<Guid>> GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            if (!await HasRepositoryAccess(repositoryId))
                return UnauthorizedResponse("repository", repositoryId);

            if (!await HasPipelineAccess(pipelineId))
                return UnauthorizedResponse("pipeline", pipelineId);
            
            Guid id = pipelineService.GetPipelineById(organizationId, repositoryId, pipelineId, this.UserId());
            return Ok(new ApiResponse { RequestName = "GetPipelineById", TicketId = id });
        }

        [HttpPost("{organizationId}/repositories/{repositoryId}/pipelines/{pipelineId}/executions")]
        [SwaggerOperation(Description = "Creates a new execution instance for a pipeline previously saved in the system. The execution is created but not started")]
        public async Task<ActionResult<Guid>> CreatePipelineExecutionInstance(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            if (!await HasRepositoryAccess(repositoryId))
                return UnauthorizedResponse("repository", repositoryId);

            if (!await HasPipelineAccess(pipelineId))
                return UnauthorizedResponse("pipeline", pipelineId);
            
            Guid id = pipelineService.CreatePipelineExecution(organizationId, repositoryId, pipelineId, this.UserId());
            return Ok(new ApiResponse { RequestName = "CreatePipelineExecutionInstance", TicketId = id });
        }

        [HttpPost("{organizationId}/repositories/{repositoryId}/pipelines/{pipelineId}/executions/{executionId}/commands/start")]
        [SwaggerOperation(Description = "Posts a start command to the defined pipeline execution. The start command will start the pipeline execution.")]
        public async Task<ActionResult<Guid>> PostStartCommand(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId)
        {
            if (!await HasRepositoryAccess(repositoryId))
                return UnauthorizedResponse("repository", repositoryId);

            if (!await HasPipelineAccess(pipelineId))
                return UnauthorizedResponse("pipeline", pipelineId);
            
            Guid id = pipelineService.PostStartCommand(organizationId, repositoryId, pipelineId, executionId, this.UserId());
            return Ok(new ApiResponse { RequestName = "PostStartCommand", TicketId = id });
        }

        [HttpGet("{organizationId}/repositories/{repositoryId}/pipelines/{pipelineId}/executions/{executionId}/status")]
        [SwaggerOperation(Description = "Gets the status of a running execution")]
        public async Task<ActionResult<Guid>> GetPipelineExecutionStatus(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId)
        {
            if (!await HasRepositoryAccess(repositoryId))
                return UnauthorizedResponse("repository", repositoryId);

            if (!await HasPipelineAccess(pipelineId))
                return UnauthorizedResponse("pipeline", pipelineId);
            
            Guid id = pipelineService.GetExecutionStatus(organizationId, repositoryId, pipelineId, executionId, this.UserId());
            return Ok(new ApiResponse { RequestName = "GetExecutionStatus", TicketId = id });
        }
    }
}
