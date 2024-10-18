using System.Security.Claims;
using DAPM.ClientApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.Extensions;

public static class ControllerBaseExtensions
{
    public static Guid UserId(this ControllerBase controller) 
        => Guid.Parse(controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    
    public static UserDto User(this ControllerBase controller) 
        => new UserDto { Id = controller.UserId() };
}