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

    public Task AddUserPipeline(UserDto user, PipelineDto pipeline)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<PipelineDto>> GetPipelinesForUser(UserDto user)
    {
        throw new NotImplementedException();
    }
}