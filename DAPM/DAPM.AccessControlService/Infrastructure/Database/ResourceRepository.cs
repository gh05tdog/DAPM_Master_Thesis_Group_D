using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class ResourceRepository : IResourceRepository
{
    private readonly IDbConnection dbConnection;
    private bool initialized;

    public ResourceRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task InitializeScheme()
    {
        const string sql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserResources' AND xtype='U')
            BEGIN
                CREATE TABLE UserResources (
                    UserId NVARCHAR(50) NOT NULL,
                    ResourceId NVARCHAR(50) NOT NULL,
                    PRIMARY KEY (UserId, ResourceId)
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

    public async Task AddUserResource(UserId userId, ResourceId resourceId)
    {
        if (!initialized)
            await InitializeScheme();
        
        const string sql = @"
                INSERT INTO UserResources (UserId, ResourceId)
                VALUES (@UserId, @ResourceId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userId.Id, ResourceId = resourceId.Id });
    }

    public async Task<ICollection<ResourceId>> GetResourcesForUser(UserId userId)
    {
        if (!initialized)
            await InitializeScheme();
        
        const string sql = @"
                SELECT ResourceId
                FROM UserResources
                WHERE UserId = @UserId;
            ";
        
        var resourceIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return resourceIds.Select(id => new ResourceId(Guid.Parse(id))).ToList();
    }
}