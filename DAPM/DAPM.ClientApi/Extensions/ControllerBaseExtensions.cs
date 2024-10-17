using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace DAPM.ClientApi.Extensions;

public static class ControllerBaseExtensions
{
    public static Guid GetUserId(this ControllerBase controller) 
        => Guid.Parse(controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
}