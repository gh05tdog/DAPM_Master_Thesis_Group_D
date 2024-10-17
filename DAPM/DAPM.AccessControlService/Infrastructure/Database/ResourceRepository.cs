using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class ResourceRepository : IResourceRepository
{
    private readonly IDbConnection dbConnection;

    public ResourceRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task InitializeScheme()
    {
        const string sql = @"
                CREATE TABLE IF NOT EXISTS UserResources (
                    UserId TEXT NOT NULL,
                    ResourceId TEXT NOT NULL,
                    PRIMARY KEY (UserId, ResourceId)
                );
            ";
        
        await dbConnection.ExecuteAsync(sql);
    }

    public async Task AddUserResource(UserId userId, ResourceId resourceId)
    {
        const string sql = @"
                INSERT INTO UserResources (UserId, ResourceId)
                VALUES (@UserId, @ResourceId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userId.Id, ResourceId = resourceId.Id });
    }

    public async Task<ICollection<ResourceId>> GetResourcesForUser(UserId userId)
    {
        const string sql = @"
                SELECT ResourceId
                FROM UserResources
                WHERE UserId = @UserId;
            ";
        
        var resourceIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return resourceIds.Select(id => new ResourceId(Guid.Parse(id))).ToList();
    }
}