using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class RepositoryRepository : IRepositoryRepository
{
    private readonly IDbConnection dbConnection;

    public RepositoryRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
        InitializeScheme().GetAwaiter().GetResult();
    }

    public async Task InitializeScheme()
    {   
        const string sql = @"
                CREATE TABLE IF NOT EXISTS UserRepositories (
                    UserId TEXT NOT NULL,
                    RepositoryId TEXT NOT NULL,
                    PRIMARY KEY (UserId, RepositoryId)
                );
            ";
        
        await dbConnection.ExecuteAsync(sql);
    }

    public async Task AddUserRepository(UserId userId, RepositoryId repositoryId)
    {
        const string sql = @"
                INSERT INTO UserRepositories (UserId, RepositoryId)
                VALUES (@UserId, @RepositoryId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userId.Id, RepositoryId = repositoryId.Id });
    }

    public async Task<ICollection<RepositoryId>> GetRepositoriesForUser(UserId userId)
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