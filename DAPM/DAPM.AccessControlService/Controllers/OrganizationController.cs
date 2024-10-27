using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/organization")]
[Authorize(Policy = "OrganizationManager")]
public class OrganizationController(IOrganizationService organizationService) : ControllerBase
{
    [HttpPost]
    [Route("add-user-organization")]
    public async Task<IActionResult> AddUserOrganization([FromBody] UserOrganizationDto userOrganization)
    {
        var response = await organizationService.AddUserOrganization(userOrganization);
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
    public async Task<IActionResult> RemoveUserOrganization([FromBody] UserOrganizationDto userOrganization)
    {
        var response = await organizationService.RemoveUserOrganization(userOrganization);
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