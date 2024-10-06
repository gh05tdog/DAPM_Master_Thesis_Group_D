using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
}