using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository, IUserResourceQueries
{
    private readonly IDbConnectionFactory dbConnectionFactory;

    public ResourceRepository(IDbConnectionFactory dbConnectionFactory, ITableInitializer<UserResource> tableInitializer)
    {
        this.dbConnectionFactory = dbConnectionFactory;
        tableInitializer.InitializeTable().Wait();
    }

    public async Task CreateUserResource(UserResource userResource)
    {
        const string sql = @"
                INSERT INTO UserResources (UserId, ResourceId)
                VALUES (@UserId, @ResourceId);
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userResource.UserId.Id, ResourceId = userResource.ResourceId.Id });
    }

    public async Task<ICollection<ResourceId>> ReadResourcesForUser(UserId userId)
    {
        const string sql = @"
                SELECT ResourceId
                FROM UserResources
                WHERE UserId = @UserId;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        var resourceIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return resourceIds.Select(id => new ResourceId(Guid.Parse(id))).ToList();
    }
    
    public async Task DeleteUserResource(UserResource userResource)
    {
        const string sql = @"
                DELETE FROM UserResources
                WHERE UserId = @UserId AND ResourceId = @ResourceId;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userResource.UserId.Id, ResourceId = userResource.ResourceId.Id });
    }
    
    public async Task<ICollection<UserResource>> ReadAllUserResources()
    {
        const string sql = @"
                SELECT UserId, ResourceId
                FROM UserResources;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        var userResources = await dbConnection.QueryAsync<(string, string)>(sql);
        return userResources
            .Select(x => new UserResource(new UserId(Guid.Parse(x.Item1)), new ResourceId(Guid.Parse(x.Item2))))
            .ToList();
    }
    
    public async Task<bool> UserHasAccessToResource(UserResource userResource)
    {
        const string sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM UserResources
                    WHERE UserId = @UserId AND ResourceId = @ResourceId
                ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        return await dbConnection.ExecuteScalarAsync<bool>(sql, new { UserId = userResource.UserId.Id, ResourceId = userResource.ResourceId.Id });
    }
}