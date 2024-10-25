using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Infrastructure.Database.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;

public class ResourceTableInitializer : ITableInitializer<UserResource>
{
    private readonly IDbConnection dbConnection;

    public ResourceTableInitializer(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }


    public async Task InitializeTable()
    {
        await dbConnection.ExecuteAsync(TestHelper.ResourceInitSql);
    }
}