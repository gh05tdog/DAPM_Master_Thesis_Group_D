using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Queries;

public class UserRepositoryQueriesTests
{
    private readonly IUserRepositoryQueries userRepositoryQueries;
    private readonly IRepositoryRepository repositoryRepository;
    
    public UserRepositoryQueriesTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var repository = new RepositoryRepository(connection, new RepositoryTableInitializer(connection));
        userRepositoryQueries = repository;
        repositoryRepository = repository;
    }
    
    [Fact]
    public async Task UserHasAccessToRepository_WhenUserHasAccessToRepository_ReturnsTrue()
    {
        var userRepository = new UserRepository(new UserId(Guid.NewGuid()), new RepositoryId(Guid.NewGuid()));
        await repositoryRepository.CreateUserRepository(userRepository);
        
        var result = await userRepositoryQueries.UserHasAccessToRepository(userRepository);
        
        Assert.True(result);
    }
}