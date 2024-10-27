using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control")]
[Authorize(Policy = "Manager")]
public class AccessControlController : ControllerBase
{
    private readonly IOrganizationService organizationService;
    private readonly IPipelineService pipelineService;
    private readonly IRepositoryService repositoryService;
    private readonly IResourceService resourceService;

    public AccessControlController(IOrganizationService organizationService, IPipelineService pipelineService, 
        IRepositoryService repositoryService, IResourceService resourceService)
    {
        this.organizationService = organizationService;
        this.pipelineService = pipelineService;
        this.repositoryService = repositoryService;
        this.resourceService = resourceService;
    }
    
    [HttpPost]
    [Route("check-access")]
    public async Task<IActionResult> CheckUserAccess([FromBody] UserAccessRequestDto userAccessRequest)
    {
        var response = new UserAccessResponseDto();
        
        if (userAccessRequest.User is null)
            return BadRequest("User is required.");
        
        if (userAccessRequest.Organization is not null)
        {
            var access = await organizationService.UserHasAccessToOrganization(new UserOrganizationDto { UserId = userAccessRequest.User.Id, OrganizationId = userAccessRequest.Organization.Id });
            response.HasOrganizationAccess = access;
        }
        
        if (userAccessRequest.Pipeline is not null)
        {
            var access = await pipelineService.UserHasAccessToPipeline(new UserPipelineDto { UserId = userAccessRequest.User.Id, PipelineId = userAccessRequest.Pipeline.Id });
            response.HasPipelineAccess = access;
        }
        
        if (userAccessRequest.Repository is not null)
        {
            var access = await repositoryService.UserHasAccessToRepository(new UserRepositoryDto { UserId = userAccessRequest.User.Id, RepositoryId = userAccessRequest.Repository.Id });
            response.HasRepositoryAccess = access;
        }
        
        if (userAccessRequest.Resource is not null)
        {
            var access = await resourceService.UserHasAccessToResource(new UserResourceDto { UserId = userAccessRequest.User.Id, ResourceId = userAccessRequest.Resource.Id });
            response.HasResourceAccess = access;
        }
        
        return Ok(response);
    }
}
