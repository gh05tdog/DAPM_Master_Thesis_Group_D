using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository, IUserResourceQueries
{
    private readonly IDbConnection dbConnection;
    private bool initialized;

    public ResourceRepository(IDbConnection dbConnection, ITableInitializer<UserResource> tableInitializer)
    {
        this.dbConnection = dbConnection;
        tableInitializer.InitializeTable().Wait();
    }

    public async Task CreateUserResource(UserResource userResource)
    {
        const string sql = @"
                INSERT INTO UserResources (UserId, ResourceId)
                VALUES (@UserId, @ResourceId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userResource.UserId.Id, ResourceId = userResource.ResourceId.Id });
    }

    public async Task<ICollection<ResourceId>> ReadResourcesForUser(UserId userId)
    {
        const string sql = @"
                SELECT ResourceId
                FROM UserResources
                WHERE UserId = @UserId;
            ";
        
        var resourceIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return resourceIds.Select(id => new ResourceId(Guid.Parse(id))).ToList();
    }
    
    public async Task DeleteUserResource(UserResource userResource)
    {
        const string sql = @"
                DELETE FROM UserResources
                WHERE UserId = @UserId AND ResourceId = @ResourceId;
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userResource.UserId.Id, ResourceId = userResource.ResourceId.Id });
    }
    
    public async Task<ICollection<UserResource>> ReadAllUserResources()
    {
        const string sql = @"
                SELECT UserId, ResourceId
                FROM UserResources;
            ";
        
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
        return await dbConnection.ExecuteScalarAsync<bool>(sql, new { UserId = userResource.UserId.Id, OrganizationId = userResource.ResourceId.Id });
    }
}