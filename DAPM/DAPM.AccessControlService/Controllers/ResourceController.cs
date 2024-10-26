using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/resource")]
public class ResourceController
{
    private readonly IResourceService resourceService;

    public ResourceController(IResourceService resourceService)
    {
        this.resourceService = resourceService;
    }
}