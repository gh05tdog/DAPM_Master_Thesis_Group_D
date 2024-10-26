using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly IDbConnection dbConnection;

    public OrganizationRepository(IDbConnection dbConnection, ITableInitializer<UserOrganization> tableInitializer)
    {
        this.dbConnection = dbConnection;
        tableInitializer.InitializeTable().Wait();
    }

    public async Task CreateUserOrganization(UserOrganization userOrganization)
    {
        const string sql = @"
                INSERT INTO UserOrganizations (UserId, OrganizationId)
                VALUES (@UserId, @OrganizationId);
            ";
        await dbConnection.ExecuteAsync(sql, new { UserId = userOrganization.UserId.Id, OrganizationId = userOrganization.OrganizationId.Id });
    }

    public async Task<ICollection<OrganizationId>> ReadOrganizationsForUser(UserId userId)
    {
        const string sql = @"
                SELECT OrganizationId
                FROM UserOrganizations
                WHERE UserId = @UserId;
            ";
        var organizationIds = await dbConnection.QueryAsync<string>(sql, new { UserId = userId.Id });
        return organizationIds.Select(id => new OrganizationId(Guid.Parse(id))).ToList();
    }
    
    public async Task DeleteUserOrganization(UserOrganization userOrganization)
    {
        const string sql = @"
                DELETE FROM UserOrganizations
                WHERE UserId = @UserId AND OrganizationId = @OrganizationId;
            ";
        await dbConnection.ExecuteAsync(sql, new { UserId = userOrganization.UserId.Id, OrganizationId = userOrganization.OrganizationId.Id });
    }
    
    public async Task<ICollection<UserOrganization>> ReadAllUserOrganizations()
    {
        const string sql = @"
                SELECT UserId, OrganizationId
                FROM UserOrganizations;
            ";
        var userOrganizations = await dbConnection.QueryAsync<(string, string)>(sql);
        return userOrganizations
            .Select(x => new UserOrganization(new UserId(Guid.Parse(x.Item1)), new OrganizationId(Guid.Parse(x.Item2))))
            .ToList();
    }
}