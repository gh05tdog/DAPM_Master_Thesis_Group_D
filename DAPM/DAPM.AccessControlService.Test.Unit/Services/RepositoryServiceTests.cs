using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Database;
using DAPM.AccessControlService.Infrastructure.Database.TableInitializers;
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

        return new RepositoryService(repository);
    }
    
    [Fact]
    public async Task AddUserRepository_ShouldAddRepository()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var repository = new RepositoryDto{Id = Guid.NewGuid()};

        await service.AddUserRepository(user, repository);

        var repositories = await service.GetRepositoriesForUser(user);
        Assert.Contains(repositories, p => p.Id == repository.Id);
    }
}