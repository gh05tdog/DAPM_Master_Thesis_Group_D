using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Repositories;

public class OrganizationRepository : IOrganizationRepository, IUserOrganizationQueries
{
    private readonly IDbConnectionFactory dbConnectionFactory;

    public OrganizationRepository(IDbConnectionFactory dbConnectionFactory, ITableInitializer<UserOrganization> tableInitializer)
    {
        this.dbConnectionFactory = dbConnectionFactory;
        tableInitializer.InitializeTable().Wait();
    }

    public async Task CreateUserOrganization(UserOrganization userOrganization)
    {
        const string sql = @"
                INSERT INTO UserOrganizations (UserId, OrganizationId)
                VALUES (@UserId, @OrganizationId);
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userOrganization.UserId.Id, OrganizationId = userOrganization.OrganizationId.Id });
    }

    public async Task<ICollection<OrganizationId>> ReadOrganizationsForUser(UserId userId)
    {
        const string sql = @"
                SELECT OrganizationId
                FROM UserOrganizations
                WHERE UserId = @UserId;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        var organizationIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return organizationIds.Select(id => new OrganizationId(Guid.Parse(id))).ToList();
    }
    
    public async Task DeleteUserOrganization(UserOrganization userOrganization)
    {
        const string sql = @"
                DELETE FROM UserOrganizations
                WHERE UserId = @UserId AND OrganizationId = @OrganizationId;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userOrganization.UserId.Id, OrganizationId = userOrganization.OrganizationId.Id });
    }
    
    public async Task<ICollection<UserOrganization>> ReadAllUserOrganizations()
    {
        const string sql = @"
                SELECT UserId, OrganizationId
                FROM UserOrganizations;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        var userOrganizations = await dbConnection.QueryAsync<(string, string)>(sql);
        return userOrganizations
            .Select(x => new UserOrganization(new UserId(Guid.Parse(x.Item1)), new OrganizationId(Guid.Parse(x.Item2))))
            .ToList();
    }

    public async Task<bool> UserHasAccessToOrganization(UserOrganization userOrganization)
    {
        const string sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM UserOrganizations
                    WHERE UserId = @UserId AND OrganizationId = @OrganizationId
                ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END;
            ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        return await dbConnection.ExecuteScalarAsync<bool>(sql, new { UserId = userOrganization.UserId.Id, OrganizationId = userOrganization.OrganizationId.Id });
    }
}