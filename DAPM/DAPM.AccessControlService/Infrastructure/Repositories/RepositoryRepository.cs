using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Repositories;

public class RepositoryRepository : IRepositoryRepository, IUserRepositoryQueries
{
    private readonly IDbConnectionFactory dbConnectionFactory;
    
    public RepositoryRepository(IDbConnectionFactory dbConnectionFactory, ITableInitializer<UserRepository> tableInitializer)
    {
        this.dbConnectionFactory = dbConnectionFactory;
        tableInitializer.InitializeTable();
    }
    
    public async Task CreateUserRepository(UserRepository userRepository)
    {
        const string sql = @"
                INSERT INTO UserRepositories (UserId, RepositoryId)
                VALUES (@UserId, @RepositoryId);
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userRepository.UserId.Id, RepositoryId = userRepository.RepositoryId.Id });
    }

    public async Task<ICollection<RepositoryId>> ReadRepositoriesForUser(UserId userId)
    {
        const string sql = @"
                SELECT RepositoryId
                FROM UserRepositories
                WHERE UserId = @UserId;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        var repositoryIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return repositoryIds.Select(id => new RepositoryId(Guid.Parse(id))).ToList();
    }
    
    public async Task DeleteUserRepository(UserRepository userRepository)
    {
        const string sql = @"
                DELETE FROM UserRepositories
                WHERE UserId = @UserId AND RepositoryId = @RepositoryId;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userRepository.UserId.Id, RepositoryId = userRepository.RepositoryId.Id });
    }
    
    public async Task<ICollection<UserRepository>> ReadAllUserRepositories()
    {
        const string sql = @"
                SELECT UserId, RepositoryId
                FROM UserRepositories;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        var userRepositories = await dbConnection.QueryAsync<(string, string)>(sql);
        return userRepositories
            .Select(x => new UserRepository(new UserId(Guid.Parse(x.Item1)), new RepositoryId(Guid.Parse(x.Item2))))
            .ToList();
    }

    public async Task<bool> UserHasAccessToRepository(UserRepository userRepository)
    {
        const string sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM UserRepositories
                    WHERE UserId = @UserId AND RepositoryId = @RepositoryId
                ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        return await dbConnection.ExecuteScalarAsync<bool>(sql, new { UserId = userRepository.UserId.Id, RepositoryId = userRepository.RepositoryId.Id });
    }
}