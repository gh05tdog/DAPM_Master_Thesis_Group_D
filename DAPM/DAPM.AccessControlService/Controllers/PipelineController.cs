using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/pipeline")]
public class PipelineController
{
   private readonly IPipelineService pipelineService;

   public PipelineController(IPipelineService pipelineService)
   {
      this.pipelineService = pipelineService;
   }
}