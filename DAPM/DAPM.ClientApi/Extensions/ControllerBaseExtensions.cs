using System.Security.Claims;
using DAPM.AccessControlService.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DAPM.ClientApi.Extensions;

public static class ControllerBaseExtensions
{
    public static Guid UserId(this ControllerBase controller) 
        => Guid.Parse(controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    
    public static UserDto User(this ControllerBase controller) 
        => new UserDto { Id = controller.UserId() };
}