using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class ResourceServiceTests
{
    private ResourceService CreateService()
    {
        var connection = new DbConnectionFactory(TestHelper.ConnectionString);
        var repository = new ResourceRepository(connection, new ResourceTableInitializer(connection));

        return new ResourceService(repository, repository);
    }
    
    [Fact]
    public async Task AddUserResource_ShouldAddResource()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var resource = new ResourceDto{Id = Guid.NewGuid()};
        var userResource = new UserResourceDto{UserId = user.Id, ResourceId = resource.Id};

        await service.AddUserResource(userResource);

        var resources = await service.GetResourcesForUser(user);
        Assert.Contains(resources, p => p.Id == resource.Id);
    }
    
    [Fact]
    public async Task RemoveUserResource_ShouldRemoveResource()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var resource = new ResourceDto{Id = Guid.NewGuid()};
        var userResource = new UserResourceDto{UserId = user.Id, ResourceId = resource.Id};

        await service.AddUserResource(userResource);
        await service.RemoveUserResource(userResource);

        var resources = await service.GetResourcesForUser(user);
        Assert.DoesNotContain(resources, p => p.Id == resource.Id);
    }
    
    [Fact]
    public async Task ReadAllResources_ShouldReturnResources()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var resource = new ResourceDto{Id = Guid.NewGuid()};
        var userResource = new UserResourceDto{UserId = user.Id, ResourceId = resource.Id};

        await service.AddUserResource(userResource);

        var resources = await service.GetAllUserResources();
        Assert.Contains(resources, p => p.ResourceId == resource.Id);
    }
    
    [Fact]
    public async Task UserHasAccessToResource_WhenUserHasAccessToResource_ReturnsTrue()
    {
        var service = CreateService();

        var userResource = new UserResourceDto{UserId = Guid.NewGuid(), ResourceId = Guid.NewGuid()};
        await service.AddUserResource(userResource);
        
        var result = await service.UserHasAccessToResource(userResource);
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task UserHasAccessToResource_WhenUserDoesNotHaveAccessToResource_ReturnsFalse()
    {
        var service = CreateService();

        var userResource = new UserResourceDto{UserId = Guid.NewGuid(), ResourceId = Guid.NewGuid()};
        
        var result = await service.UserHasAccessToResource(userResource);
        
        Assert.False(result);
    }
}