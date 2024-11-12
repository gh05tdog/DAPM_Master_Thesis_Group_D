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
    
    public static OrganizationId ToOrganizationId(this OrganizationDto organization)
    {
        return new OrganizationId(organization.Id);
    }
    
    public static UserOrganization ToUserOrganization(this UserOrganizationDto userOrganization)
    {
        return new UserOrganization(new UserId(userOrganization.UserId), new OrganizationId(userOrganization.OrganizationId));
    }
    
    public static UserPipeline ToUserPipeline(this UserPipelineDto userPipeline)
    {
        return new UserPipeline(new UserId(userPipeline.UserId), new PipelineId(userPipeline.PipelineId));
    }
    
    public static UserResource ToUserResource(this UserResourceDto userResource)
    {
        return new UserResource(new UserId(userResource.UserId), new ResourceId(userResource.ResourceId));
    }
    
    public static UserRepository ToUserRepository(this UserRepositoryDto userRepository)
    {
        return new UserRepository(new UserId(userRepository.UserId), new RepositoryId(userRepository.RepositoryId));
    }
}