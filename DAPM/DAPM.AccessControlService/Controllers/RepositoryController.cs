using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/repository")]
public class RepositoryController
{
    private readonly IRepositoryService repositoryService;

    public RepositoryController(IRepositoryService repositoryService)
    {
        this.repositoryService = repositoryService;
    }
}