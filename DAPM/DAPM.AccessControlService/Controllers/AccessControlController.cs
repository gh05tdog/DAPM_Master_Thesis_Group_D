using DAPM.AccessControlService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control")]
public class AccessControlController : ControllerBase
{
    private readonly IAccessControlFacade accessControlFacade;

    public AccessControlController(IAccessControlFacade accessControlFacade)
    {
        this.accessControlFacade = accessControlFacade;
    }

    [HttpPost]
    [Route("add-user-resource")]
    public async Task<IActionResult> AddUserResource([FromBody] AddUserResourceRequestMessage message)
    {
        var response = await accessControlFacade.AddUserResource(message);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("add-user-repository")]
    public async Task<IActionResult> AddUserRepository([FromBody] AddUserRepositoryRequestMessage message)
    {
        var response = await accessControlFacade.AddUserRepository(message);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("add-user-pipeline")]
    public async Task<IActionResult> AddUserPipeline([FromBody] AddUserPipelineRequestMessage message)
    {
        var response = await accessControlFacade.AddUserPipeline(message);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-pipelines-for-user/{userId}")]
    public async Task<IActionResult> GetPipelinesForUser(Guid userId)
    {
        var message = new GetPipelinesForUserRequestMessage{ User = new UserDto{ Id = userId} };
        var response = await accessControlFacade.GetPipelinesForUser(message);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-repositories-for-user/{userId}")]
    public async Task<IActionResult> GetRepositoriesForUser(Guid userId)
    {
        var message = new GetRepositoriesForUserRequestMessage{ User = new UserDto{ Id = userId} };
        var response = await accessControlFacade.GetRepositoriesForUser(message);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-resources-for-user/{userId}")]
    public async Task<IActionResult> GetResourcesForUser(Guid userId)
    {
        var message = new GetResourcesForUserRequestMessage{ User = new UserDto{ Id = userId} };
        var response = await accessControlFacade.GetResourcesForUser(message);
        return Ok(response);
    }
}