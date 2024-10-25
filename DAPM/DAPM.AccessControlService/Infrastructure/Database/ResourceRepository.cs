using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;
using DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class ResourceRepository : IResourceRepository
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
}