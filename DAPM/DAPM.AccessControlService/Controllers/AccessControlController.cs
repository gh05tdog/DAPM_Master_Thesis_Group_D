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
    
    [HttpPost]
    [Route("add-user-organization")]
    public async Task<IActionResult> AddUserOrganization([FromBody] AddUserOrganizationRequestMessage message)
    {
        var response = await accessControlFacade.AddUserOrganization(message);
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
    
    [HttpGet]
    [Route("get-organizations-for-user/{userId}")]
    public async Task<IActionResult> GetOrganizationsForUser(Guid userId)
    {
        var message = new GetOrganizationsForUserRequestMessage{ User = new UserDto{ Id = userId} };
        var response = await accessControlFacade.GetOrganizationsForUser(message);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("remove-user-resource")]
    public async Task<IActionResult> RemoveUserResource([FromBody] RemoveUserResourceRequestMessage message)
    {
        var response = await accessControlFacade.RemoveUserResource(message);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("remove-user-repository")]
    public async Task<IActionResult> RemoveUserRepository([FromBody] RemoveUserRepositoryRequestMessage message)
    {
        var response = await accessControlFacade.RemoveUserRepository(message);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("remove-user-pipeline")]
    public async Task<IActionResult> RemoveUserPipeline([FromBody] RemoveUserPipelineRequestMessage message)
    {
        var response = await accessControlFacade.RemoveUserPipeline(message);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("remove-user-organization")]
    public async Task<IActionResult> RemoveUserOrganization([FromBody] RemoveUserOrganizationRequestMessage message)
    {
        var response = await accessControlFacade.RemoveUserOrganization(message);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all-user-organizations")]
    public async Task<IActionResult> GetAllUserOrganizations()
    {
        var response = await accessControlFacade.GetAllUserOrganizations();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all-user-pipelines")]
    public async Task<IActionResult> GetAllUserPipelines()
    {
        var response = await accessControlFacade.GetAllUserPipelines();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all-user-repositories")]
    public async Task<IActionResult> GetAllUserRepositories()
    {
        var response = await accessControlFacade.GetAllUserRepositories();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all-user-resources")]
    public async Task<IActionResult> GetAllUserResources()
    {
        var response = await accessControlFacade.GetAllUserResources();
        return Ok(response);
    }
}