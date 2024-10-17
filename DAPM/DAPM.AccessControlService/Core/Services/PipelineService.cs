using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.DTOs;
using DAPM.AccessControlService.Core.Services.Abstractions;

namespace DAPM.AccessControlService.Core.Services;

public class PipelineService : IPipelineService
{
    private readonly IPipelineRepository pipelineRepository;

    public PipelineService(IPipelineRepository pipelineRepository)
    {
        this.pipelineRepository = pipelineRepository;
    }

    public async Task AddUserPipeline(UserDto user, PipelineDto pipeline)
    {
        var userId = user.ToUserId();
        var pipelineId = pipeline.ToPipelineId();
        await pipelineRepository.AddUserPipeline(userId, pipelineId);
    }

    public async Task<ICollection<PipelineDto>> GetPipelinesForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var pipelineIds = await pipelineRepository.GetPipelinesForUser(userId);
        return pipelineIds.Select(p => new PipelineDto(p)).ToList();
    }
}