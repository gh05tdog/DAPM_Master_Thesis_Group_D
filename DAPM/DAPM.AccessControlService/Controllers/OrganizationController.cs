using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/organization")]
public class OrganizationController(IOrganizationService organizationService) : ControllerBase
{
    [HttpPost]
    [Route("add-user-organization")]
    public async Task<IActionResult> AddUserOrganization([FromBody] AddUserOrganizationRequestMessage message)
    {
        var response = await organizationService.AddUserOrganization(message.User, message.Organization);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-organizations-for-user/{userId}")]
    public async Task<IActionResult> GetOrganizationsForUser(Guid userId)
    {
        var response = await organizationService.GetOrganizationsForUser(new UserDto{ Id = userId});
        return Ok(response);
    }
    
    [HttpPost]
    [Route("remove-user-organization")]
    public async Task<IActionResult> RemoveUserOrganization([FromBody] RemoveUserOrganizationRequestMessage message)
    {
        var response = await organizationService.RemoveUserOrganization(message.User, message.Organization);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("get-all-user-organizations")]
    public async Task<IActionResult> GetAllUserOrganizations()
    {
        var response = await organizationService.GetAllUserOrganizations();
        return Ok(response);
    }
}