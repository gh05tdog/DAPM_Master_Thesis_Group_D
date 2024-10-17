using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data.Common;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class RepositoryRepository : IRepositoryRepository
{
    private readonly DbConnection dbConnection;

    public RepositoryRepository(DbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task InitializeScheme()
    {   
        const string sql = @"
                CREATE TABLE IF NOT EXISTS UserRepositories (
                    UserId UNIQUEIDENTIFIER NOT NULL,
                    RepositoryId UNIQUEIDENTIFIER NOT NULL,
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
        
        var repositoryIds = await dbConnection.QueryAsync<Guid>(sql, new { UserId = userId.Id });
        return repositoryIds.Select(id => new RepositoryId(id)).ToList();
    }
}