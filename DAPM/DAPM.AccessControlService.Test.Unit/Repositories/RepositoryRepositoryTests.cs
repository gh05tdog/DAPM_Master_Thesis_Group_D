using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Infrastructure.Database;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class RepositoryRepositoryTests
{
    private IDbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        return connection;
    }
    
    
    [Fact]
    public async Task AddUserRepository_ShouldAddRepository()
    {
        using var connection = CreateInMemoryDatabase();
        var repository = new RepositoryRepository(connection);
        await repository.InitializeScheme();

        var userId = new UserId(Guid.NewGuid());
        var repositoryId = new RepositoryId(Guid.NewGuid());

        await repository.AddUserRepository(userId, repositoryId);

        var repositories = await repository.GetRepositoriesForUser(userId);
        Assert.Contains(repositories, p => p.Id == repositoryId.Id);
    }
}