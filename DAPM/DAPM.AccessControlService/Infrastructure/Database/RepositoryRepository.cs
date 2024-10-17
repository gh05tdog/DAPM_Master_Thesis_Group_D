using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class RepositoryRepository : IRepositoryRepository
{
    private readonly IDbConnection dbConnection;
    private bool initialized;

    public RepositoryRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task InitializeScheme()
    {   
        const string sql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRepositories' AND xtype='U')
            BEGIN
                CREATE TABLE UserRepositories (
                    UserId NVARCHAR(50) NOT NULL,
                    RepositoryId NVARCHAR(50) NOT NULL,
                    PRIMARY KEY (UserId, RepositoryId)
                );
            END
        ";
        
        await dbConnection.ExecuteAsync(sql);
        initialized = true;
    }
    
    public async Task InitializeScheme(string sql)
        {
            await dbConnection.ExecuteAsync(sql);
            initialized = true;
        }

    public async Task AddUserRepository(UserId userId, RepositoryId repositoryId)
    {
        if (!initialized)
            await InitializeScheme();
        
        const string sql = @"
                INSERT INTO UserRepositories (UserId, RepositoryId)
                VALUES (@UserId, @RepositoryId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userId.Id, RepositoryId = repositoryId.Id });
    }

    public async Task<ICollection<RepositoryId>> GetRepositoriesForUser(UserId userId)
    {
        if (!initialized)
            await InitializeScheme();
        
        const string sql = @"
                SELECT RepositoryId
                FROM UserRepositories
                WHERE UserId = @UserId;
            ";
        
        var repositoryIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return repositoryIds.Select(id => new RepositoryId(Guid.Parse(id))).ToList();
    }
}