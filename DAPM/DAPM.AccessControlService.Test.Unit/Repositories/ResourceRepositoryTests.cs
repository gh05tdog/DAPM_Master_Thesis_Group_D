using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Infrastructure.Database;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class ResourceRepositoryTests
{
    private IDbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        return connection;
    }
    
    
    [Fact]
    public async Task AddUserResource_ShouldAddResource()
    {
        using var connection = CreateInMemoryDatabase();
        var repository = new ResourceRepository(connection);
        await repository.InitializeScheme();

        var userId = new UserId(Guid.NewGuid());
        var resourceId = new ResourceId(Guid.NewGuid());

        await repository.AddUserResource(userId, resourceId);

        var resources = await repository.GetResourcesForUser(userId);
        Assert.Contains(resources, p => p.Id == resourceId.Id);
    }
}