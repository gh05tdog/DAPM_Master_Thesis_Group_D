using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.TableInitializers;

public class PipelineTableInitializer : ITableInitializer<UserPipeline>
{
    private readonly IDbConnectionFactory dbConnectionFactory;

    public PipelineTableInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        this.dbConnectionFactory = dbConnectionFactory;
    }

    public void InitializeTable()
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
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        dbConnection.Execute(sql);
    }
}