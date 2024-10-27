using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.Database;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class RepositoryRepositoryTests
{
    private readonly IRepositoryRepository repository;

    public RepositoryRepositoryTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        repository = new RepositoryRepository(connection, new RepositoryTableInitializer(connection));
    }


    [Fact]
    public async Task AddUserRepository_ShouldAddRepository()
    {
        var userId = new UserId(Guid.NewGuid());
        var repositoryId = new RepositoryId(Guid.NewGuid());

        await repository.CreateUserRepository(new UserRepository(userId, repositoryId));

        var repositories = await repository.ReadRepositoriesForUser(userId);
        Assert.Contains(repositories, p => p.Id == repositoryId.Id);
    }
    
    [Fact]
    public async Task RemoveUserRepository_ShouldRemoveRepository()
    {
        var userId = new UserId(Guid.NewGuid());
        var repositoryId = new RepositoryId(Guid.NewGuid());

        await repository.CreateUserRepository(new UserRepository(userId, repositoryId));
        await repository.DeleteUserRepository(new UserRepository(userId, repositoryId));

        var repositories = await repository.ReadRepositoriesForUser(userId);
        Assert.DoesNotContain(repositories, p => p.Id == repositoryId.Id);
    }

    [Fact]
    public async Task ReadAllRepositories_ShouldReturnRepositories()
    {
        var userId = new UserId(Guid.NewGuid());
        var repositoryId = new RepositoryId(Guid.NewGuid());

        await repository.CreateUserRepository(new UserRepository(userId, repositoryId));

        var repositories = await repository.ReadAllUserRepositories();
        Assert.Contains(repositories, p => p.RepositoryId.Id == repositoryId.Id);
    }
}