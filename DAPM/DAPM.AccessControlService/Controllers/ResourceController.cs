using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/resource")]
public class ResourceController(IResourceService resourceService) : ControllerBase
{
    [HttpPost]
    [Route("add-user-resource")]
    public async Task<IActionResult> AddUserResource([FromBody] AddUserResourceRequestMessage message)
    {
        var response = await resourceService.AddUserResource(message.User, message.Resource);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-resources-for-user/{userId}")]
    public async Task<IActionResult> GetResourcesForUser(Guid userId)
    {
        var response = await resourceService.GetResourcesForUser(new UserDto{ Id = userId});
        return Ok(response);
    }
    
        
    [HttpPost]
    [Route("remove-user-resource")]
    public async Task<IActionResult> RemoveUserResource([FromBody] RemoveUserResourceRequestMessage message)
    {
        var response = await resourceService.RemoveUserResource(message.User, message.Resource);
        return Ok(response);
    }

    
    
    [HttpGet]
    [Route("get-all-user-resources")]
    public async Task<IActionResult> GetAllUserResources()
    {
        var response = await resourceService.GetAllUserResources();
        return Ok(response);
    }
}