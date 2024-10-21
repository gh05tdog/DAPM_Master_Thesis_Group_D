using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class ResourceServiceTests
{
    private async Task<ResourceService> CreateService()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        var repository = new ResourceRepository(connection);
        await repository.InitializeScheme(TestHelper.ResourceInitSql);

        return new ResourceService(repository);
    }
    
    [Fact]
    public async Task AddUserResource_ShouldAddResource()
    {
        var service = await CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var resource = new ResourceDto{Id = Guid.NewGuid()};

        await service.AddUserResource(user, resource);

        var resources = await service.GetResourcesForUser(user);
        Assert.Contains(resources, p => p.Id == resource.Id);
    }
}