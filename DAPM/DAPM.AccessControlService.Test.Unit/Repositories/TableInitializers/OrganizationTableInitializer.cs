using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;

public class OrganizationTableInitializer : ITableInitializer<UserOrganization>
{
    private readonly IDbConnection dbConnection;

    public OrganizationTableInitializer(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task InitializeTable()
    {
        await dbConnection.ExecuteAsync(TestHelper.OrganizationInitSql);
    }
}