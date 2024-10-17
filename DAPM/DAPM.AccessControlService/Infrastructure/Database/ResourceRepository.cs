using System.Data.SqlClient;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class ResourceRepository : IResourceRepository
{
    private readonly string connectionString;

    public ResourceRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task InitializeScheme()
    {
        const string sql = @"
                CREATE TABLE IF NOT EXISTS UserResources (
                    UserId UNIQUEIDENTIFIER NOT NULL,
                    ResourceId UNIQUEIDENTIFIER NOT NULL,
                    PRIMARY KEY (UserId, ResourceId)
                );
            ";
        
        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql);
    }

    public async Task AddUserResource(UserId userId, ResourceId resourceId)
    {
        const string sql = @"
                INSERT INTO UserResources (UserId, ResourceId)
                VALUES (@UserId, @ResourceId);
            ";
        
        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql, new { UserId = userId.Id, ResourceId = resourceId.Id });
    }

    public async Task<ICollection<ResourceId>> GetResourcesForUser(UserId userId)
    {
        const string sql = @"
                SELECT ResourceId
                FROM UserResources
                WHERE UserId = @UserId;
            ";
        
        await using var connection = new SqlConnection(connectionString);
        var resourceIds = await connection.QueryAsync<Guid>(sql, new { UserId = userId.Id });
        return resourceIds.Select(id => new ResourceId(id)).ToList();
    }
}