using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.TableInitializers;

public class ResourceTableInitializer : ITableInitializer<UserResource>
{
    private readonly IDbConnectionFactory dbConnectionFactory;

    public ResourceTableInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        this.dbConnectionFactory = dbConnectionFactory;
    }

    public void InitializeTable()
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
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        dbConnection.Execute(sql);
    }
}