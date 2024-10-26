using DAPM.AccessControlService.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Controllers;

[ApiController]
[Route("api/access-control/pipeline")]
public class PipelineController(IPipelineService pipelineService) : ControllerBase
{
   [HttpPost]
   [Route("add-user-pipeline")]
   public async Task<IActionResult> AddUserPipeline([FromBody] AddUserPipelineRequestMessage message)
   {
      var response = await pipelineService.AddUserPipeline(message.User, message.Pipeline);
      return Ok(response);
   }
   
   [HttpGet]
   [Route("get-pipelines-for-user/{userId}")]
   public async Task<IActionResult> GetPipelinesForUser(Guid userId)
   {
      var response = await pipelineService.GetPipelinesForUser(new UserDto{ Id = userId});
      return Ok(response);
   }
   
   [HttpPost]
   [Route("remove-user-pipeline")]
   public async Task<IActionResult> RemoveUserPipeline([FromBody] RemoveUserPipelineRequestMessage message)
   {
      var response = await pipelineService.RemoveUserPipeline(message.User, message.Pipeline);
      return Ok(response);
   }
   
   [HttpGet]
   [Route("get-all-user-pipelines")]
   public async Task<IActionResult> GetAllUserPipelines()
   {
      var response = await pipelineService.GetAllUserPipelines();
      return Ok(response);
   }
}