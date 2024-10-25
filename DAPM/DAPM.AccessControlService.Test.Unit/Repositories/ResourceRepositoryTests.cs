using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Infrastructure.Database;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class ResourceRepositoryTests
{
    private readonly ResourceRepository repository;
    
    public ResourceRepositoryTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        repository = new ResourceRepository(connection, new ResourceTableInitializer(connection));
    }

    [Fact]
    public async Task AddUserResource_ShouldAddResource()
    {
        var userId = new UserId(Guid.NewGuid());
        var resourceId = new ResourceId(Guid.NewGuid());

        await repository.AddUserResource(userId, resourceId);

        var resources = await repository.GetResourcesForUser(userId);
        Assert.Contains(resources, p => p.Id == resourceId.Id);
    }
}