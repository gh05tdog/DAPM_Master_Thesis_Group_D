using System.Security.Claims;
using DAPM.ClientApi.AccessControl;
using DAPM.ClientApi.Extensions;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Models.AccessControl;
using Swashbuckle.AspNetCore.Annotations;

namespace DAPM.ClientApi.Controllers;

[ApiController]
[EnableCors("AllowAll")]
[Route("test/authentication")]
public class AuthenticationTestController : ControllerBase
{
    private readonly IAccessControlService accessControlService;

    public AuthenticationTestController(IAccessControlService accessControlService)
    {
        this.accessControlService = accessControlService;
    }

    [AllowAnonymous]
    [HttpGet("anonymous")]
    [SwaggerOperation(Description = "Get as anonymous")]
    public ActionResult<string> Anonymous()
    {
        return Ok("Anonymous");
    }
    
    [Authorize]
    [HttpGet("authorize")]
    [SwaggerOperation(Description = "Get as authorized")]
    public ActionResult<string> Authorize()
    {
        return Ok("Authorized");
    }
    
    [AllowAnonymous]
    [HttpGet("addUserAccessToPipeline")]
    [SwaggerOperation(Description = "Add a user to a pipeline")]
    public async Task<ActionResult<string>> AddAccessPipeline()
    {
        var success = await accessControlService.AddUserToPipeline(new UserDto{Id = Guid.NewGuid()}, new PipelineDto{Id = Guid.NewGuid()});
        if (success)
            return Ok("Success");
        
        return BadRequest("Failed");
    }
    
    [Authorize]
    [HttpGet("addUserAccessToPipelineAuthorized")]
    [SwaggerOperation(Description = "Add a user to a pipeline but requires authorization")]
    public async Task<ActionResult<string>> AddAccessPipelineAuthorized()
    {
        var success = await accessControlService.AddUserToPipeline(new UserDto{Id = this.UserId()}, new PipelineDto{Id = Guid.NewGuid()});
        if (success)
            return Ok("Success");
        
        return BadRequest("Failed");
    }
}