using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

public class ResourceTableInitializer : ITableInitializer<UserResource>
{
    private readonly IDbConnection dbConnection;

    public ResourceTableInitializer(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }


    public async Task InitializeTable()
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
        
        await dbConnection.ExecuteAsync(sql);
    }
}