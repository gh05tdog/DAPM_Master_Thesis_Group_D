using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Database;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class ResourceServiceTests
{
    private ResourceService CreateService()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        var repository = new ResourceRepository(connection, new ResourceTableInitializer(connection));

        return new ResourceService(repository);
    }
    
    [Fact]
    public async Task AddUserResource_ShouldAddResource()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var resource = new ResourceDto{Id = Guid.NewGuid()};

        await service.AddUserResource(user, resource);

        var resources = await service.GetResourcesForUser(user);
        Assert.Contains(resources, p => p.Id == resource.Id);
    }
    
    [Fact]
    public async Task RemoveUserResource_ShouldRemoveResource()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var resource = new ResourceDto{Id = Guid.NewGuid()};

        await service.AddUserResource(user, resource);
        await service.RemoveUserResource(user, resource);

        var resources = await service.GetResourcesForUser(user);
        Assert.DoesNotContain(resources, p => p.Id == resource.Id);
    }
}