using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Extensions;
using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services;

public class PipelineService : IPipelineService
{
    private readonly IPipelineRepository pipelineRepository;

    public PipelineService(IPipelineRepository pipelineRepository)
    {
        this.pipelineRepository = pipelineRepository;
    }

    public async Task<bool> AddUserPipeline(UserDto user, PipelineDto pipeline)
    {
        var userId = user.ToUserId();
        var pipelineId = pipeline.ToPipelineId();
        await pipelineRepository.CreateUserPipeline(userId, pipelineId);
        return true;
    }

    public async Task<ICollection<PipelineDto>> GetPipelinesForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var pipelineIds = await pipelineRepository.ReadPipelinesForUser(userId);
        return pipelineIds.Select(p => new PipelineDto{Id = p.Id}).ToList();
    }
}