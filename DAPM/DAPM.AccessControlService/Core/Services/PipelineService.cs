using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Extensions;
using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services;

public class PipelineService : IPipelineService
{
    private readonly IPipelineRepository pipelineRepository;
    private readonly IUserPipelineQueries userPipelineQueries;

    public PipelineService(IPipelineRepository pipelineRepository, IUserPipelineQueries userPipelineQueries)
    {
        this.pipelineRepository = pipelineRepository;
        this.userPipelineQueries = userPipelineQueries;
    }

    public async Task<bool> AddUserPipeline(UserPipelineDto userPipeline)
    {
        await pipelineRepository.CreateUserPipeline(userPipeline.ToUserPipeline());
        return true;
    }

    public async Task<ICollection<PipelineDto>> GetPipelinesForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var pipelineIds = await pipelineRepository.ReadPipelinesForUser(userId);
        return pipelineIds.Select(p => new PipelineDto{Id = p.Id}).ToList();
    }
    
    public async Task<bool> RemoveUserPipeline(UserPipelineDto userPipeline)
    {
        await pipelineRepository.DeleteUserPipeline(userPipeline.ToUserPipeline());
        return true;
    }
    
    public async Task<ICollection<UserPipelineDto>> GetAllUserPipelines()
    {
        var userPipelines = await pipelineRepository.ReadAllUserPipelines();
        return userPipelines.Select(up => new UserPipelineDto
        {
            UserId = up.UserId.Id,
            PipelineId = up.PipelineId.Id
        }).ToList();
    }

    public async Task<bool> UserHasAccessToPipeline(UserPipelineDto userPipeline)
    {
        return await userPipelineQueries.UserHasAccessToPipeline(userPipeline.ToUserPipeline());
    }
}