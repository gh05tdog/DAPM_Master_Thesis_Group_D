using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.DTOs;
using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Database;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class ResourceServiceTests
{
    private async Task<ResourceService> CreateService()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        var repository = new ResourceRepository(connection);
        await repository.InitializeScheme();

        return new ResourceService(repository);
    }
    
    [Fact]
    public async Task AddUserResource_ShouldAddResource()
    {
        var service = await CreateService();

        var user = new UserDto(Guid.NewGuid());
        var resource = new ResourceDto(Guid.NewGuid());

        await service.AddUserResource(user, resource);

        var resources = await service.GetResourcesForUser(user);
        Assert.Contains(resources, p => p.Id == resource.Id);
    }
}