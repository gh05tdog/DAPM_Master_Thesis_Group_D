using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;
using DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class RepositoryRepository : IRepositoryRepository
{
    private readonly IDbConnection dbConnection;
    private bool initialized;

    public RepositoryRepository(IDbConnection dbConnection, ITableInitializer<UserRepository> tableInitializer)
    {
        this.dbConnection = dbConnection;
        tableInitializer.InitializeTable().Wait();
    }
    
    public async Task CreateUserRepository(UserRepository userRepository)
    {
        const string sql = @"
                INSERT INTO UserRepositories (UserId, RepositoryId)
                VALUES (@UserId, @RepositoryId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userRepository.UserId.Id, RepositoryId = userRepository.RepositoryId.Id });
    }

    public async Task<ICollection<RepositoryId>> ReadRepositoriesForUser(UserId userId)
    {
        const string sql = @"
                SELECT RepositoryId
                FROM UserRepositories
                WHERE UserId = @UserId;
            ";
        
        var repositoryIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return repositoryIds.Select(id => new RepositoryId(Guid.Parse(id))).ToList();
    }
}