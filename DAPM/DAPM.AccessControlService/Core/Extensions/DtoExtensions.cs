using DAPM.AccessControlService.Core.Domain.Entities;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Extensions;

public static class DtoExtensions
{
    public static UserId ToUserId(this UserDto user)
    {
        return new UserId(user.Id);
    }
    
    public static RepositoryId ToRepositoryId(this RepositoryDto repository)
    {
        return new RepositoryId(repository.Id);
    }
    
    public static PipelineId ToPipelineId(this PipelineDto pipeline)
    {
        return new PipelineId(pipeline.Id);
    }
    
    public static ResourceId ToResourceId(this ResourceDto resource)
    {
        return new ResourceId(resource.Id);
    }
}