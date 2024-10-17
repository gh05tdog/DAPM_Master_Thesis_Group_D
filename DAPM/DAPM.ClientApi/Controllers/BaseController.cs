using DAPM.AccessControlService.Core.Dtos;
using DAPM.ClientApi.Extensions;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DAPM.ClientApi.Controllers;

public abstract class BaseController : ControllerBase
{
    private readonly IAccessControlService accessControlService;

    protected BaseController(IAccessControlService accessControlService)
    {
        accessControlService = accessControlService;
    }
    
    protected async Task<bool> HasRepositoryAccess(Guid repositoryId)
    {
        return await accessControlService.UserHasAccessToRepository(this.User(), new RepositoryDto { Id = repositoryId });
    }

    protected async Task<bool> HasPipelineAccess(Guid pipelineId)
    {
        return await accessControlService.UserHasAccessToPipeline(this.User(), new PipelineDto { Id = pipelineId });
    }
    
    protected async Task<bool> HasResourceAccess(Guid resourceId)
    {
        return await accessControlService.UserHasAccessToResource(this.User(), new ResourceDto { Id = resourceId });
    }

    protected ActionResult UnauthorizedResponse(string resource, Guid resourceId)
    {
        return Unauthorized($"User with id '{this.UserId()}' does not have access to {resource} with id '{resourceId}'");
    }
}