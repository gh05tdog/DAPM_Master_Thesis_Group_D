using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class RepositoryServiceTests
{
    private RepositoryService CreateService()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        var repository = new RepositoryRepository(connection, new RepositoryTableInitializer(connection));

        return new RepositoryService(repository, repository);
    }
    
    [Fact]
    public async Task AddUserRepository_ShouldAddRepository()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var repository = new RepositoryDto{Id = Guid.NewGuid()};
        var userRepository = new UserRepositoryDto{UserId = user.Id, RepositoryId = repository.Id};

        await service.AddUserRepository(userRepository);

        var repositories = await service.GetRepositoriesForUser(user);
        Assert.Contains(repositories, p => p.Id == repository.Id);
    }
    
    [Fact]
    public async Task RemoveUserRepository_ShouldRemoveRepository()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var repository = new RepositoryDto{Id = Guid.NewGuid()};
        var userRepository = new UserRepositoryDto{UserId = user.Id, RepositoryId = repository.Id};

        await service.AddUserRepository(userRepository);
        await service.RemoveUserRepository(userRepository);

        var repositories = await service.GetRepositoriesForUser(user);
        Assert.DoesNotContain(repositories, p => p.Id == repository.Id);
    }
    
    [Fact]
    public async Task ReadAllRepositories_ShouldReturnRepositories()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var repository = new RepositoryDto{Id = Guid.NewGuid()};
        var userRepository = new UserRepositoryDto{UserId = user.Id, RepositoryId = repository.Id};

        await service.AddUserRepository(userRepository);

        var repositories = await service.GetAllUserRepositories();
        Assert.Contains(repositories, p => p.RepositoryId == repository.Id);
    }
}