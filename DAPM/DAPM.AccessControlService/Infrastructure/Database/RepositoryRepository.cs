using System.Data.SqlClient;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class RepositoryRepository : IRepositoryRepository
{
    private readonly string connectionString;

    public RepositoryRepository(string connectionString)
    {
        this.connectionString = connectionString;
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
        
        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql);
    }

    public async Task AddUserRepository(UserId userId, RepositoryId repositoryId)
    {
        const string sql = @"
                INSERT INTO UserRepositories (UserId, RepositoryId)
                VALUES (@UserId, @RepositoryId);
            ";
        
        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql, new { UserId = userId.Id, RepositoryId = repositoryId.Id });
    }

    public async Task<ICollection<RepositoryId>> GetRepositoriesForUser(UserId userId)
    {
        const string sql = @"
                SELECT RepositoryId
                FROM UserRepositories
                WHERE UserId = @UserId;
            ";
        
        await using var connection = new SqlConnection(connectionString);
        var repositoryIds = await connection.QueryAsync<Guid>(sql, new { UserId = userId.Id });
        return repositoryIds.Select(id => new RepositoryId(id)).ToList();
    }
}