using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/organization")]
public class OrganizationController
{
    private readonly IOrganizationService organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        this.organizationService = organizationService;
    }
}