using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DAPM.ClientApi.Models;

namespace DAPM.ClientApi.Controllers;


[ApiController]
[EnableCors("AllowAll")]
[Route("test/authentication")]
public class AuthenticationTestController : ControllerBase
{
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

    [Authorize]
    [HttpGet("user/test")]
    [SwaggerOperation(Description = "Get user information")]
    public ActionResult<string> GetUser()
    {
        var user = new User {
            Id = "e86d9920-a202-4e9d-bfa4-de4117a4c5e5",
            Username = "test",
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com",
            EmailVerified = true,
            Enabled = true,
        };
        return Ok(user);
    }
}