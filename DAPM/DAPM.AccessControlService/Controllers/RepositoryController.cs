using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/repository")]
public class RepositoryController(IRepositoryService repositoryService) : ControllerBase
{
    [HttpPost]
    [Route("add-user-repository")]
    public async Task<IActionResult> AddUserRepository([FromBody] AddUserRepositoryRequestMessage message)
    {
        var response = await repositoryService.AddUserRepository(message.User, message.Repository);
        return Ok(response);
    }
     
    [HttpGet]
    [Route("get-repositories-for-user/{userId}")]
    public async Task<IActionResult> GetRepositoriesForUser(Guid userId)
    {
        var response = await repositoryService.GetRepositoriesForUser(new UserDto{ Id = userId});
        return Ok(response);
    }
    
    [HttpPost]
    [Route("remove-user-repository")]
    public async Task<IActionResult> RemoveUserRepository([FromBody] RemoveUserRepositoryRequestMessage message)
    {
        var response = await repositoryService.RemoveUserRepository(message.User, message.Repository);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all-user-repositories")]
    public async Task<IActionResult> GetAllUserRepositories()
    {
        var response = await repositoryService.GetAllUserRepositories();
        return Ok(response);
    }
}