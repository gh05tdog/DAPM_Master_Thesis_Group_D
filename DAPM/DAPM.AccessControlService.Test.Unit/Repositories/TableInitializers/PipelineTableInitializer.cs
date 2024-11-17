using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Infrastructure;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;

public class PipelineTableInitializer : ITableInitializer<UserPipeline>
{
    private readonly IDbConnectionFactory dbConnectionFactory;

    public PipelineTableInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        this.dbConnectionFactory = dbConnectionFactory;
    }


    public async Task InitializeTable()
    {
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        await dbConnection.ExecuteAsync(TestHelper.PipelineInitSql);
    }
}