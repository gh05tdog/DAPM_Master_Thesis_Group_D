using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.Database.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly IDbConnection dbConnection;

    public OrganizationRepository(IDbConnection dbConnection, ITableInitializer<UserOrganization> tableInitializer)
    {
        this.dbConnection = dbConnection;
        tableInitializer.InitializeTable().Wait();
    }

    public async Task CreateUserOrganization(UserId userId, OrganizationId organizationId)
    {
        const string sql = @"
                INSERT INTO UserOrganizations (UserId, OrganizationId)
                VALUES (@UserId, @OrganizationId);
            ";
        await dbConnection.ExecuteAsync(sql, new { UserId = userId.Id, OrganizationId = organizationId.Id });
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
}