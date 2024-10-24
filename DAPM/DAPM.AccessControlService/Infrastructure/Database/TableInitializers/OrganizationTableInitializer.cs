using System.Data;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

public class OrganizationTableInitializer : ITableInitializer
{
    private readonly IDbConnection dbConnection;

    public OrganizationTableInitializer(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task InitializeTable()
    {
        const string sql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserOrganizations' AND xtype='U')
            BEGIN
                CREATE TABLE UserOrganizations (
                    UserId NVARCHAR(50) NOT NULL,
                    OrganizationId NVARCHAR(50) NOT NULL,
                    PRIMARY KEY (UserId, OrganizationId)
                );
            END
        ";
        
        await dbConnection.ExecuteAsync(sql);
    }
}