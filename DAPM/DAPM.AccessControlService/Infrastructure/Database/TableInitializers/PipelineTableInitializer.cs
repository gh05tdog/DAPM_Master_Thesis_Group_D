using System.Data;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

public class PipelineTableInitializer : ITableInitializer
{
    private readonly IDbConnection dbConnection;

    public PipelineTableInitializer(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }


    public async Task InitializeTable()
    {
        const string sql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserPipelines' AND xtype='U')
            BEGIN
                CREATE TABLE UserPipelines (
                    UserId NVARCHAR(50) NOT NULL,
                    PipelineId NVARCHAR(50) NOT NULL,
                    PRIMARY KEY (UserId, PipelineId)
                );
            END
        ";
        
        await dbConnection.ExecuteAsync(sql);
    }
}