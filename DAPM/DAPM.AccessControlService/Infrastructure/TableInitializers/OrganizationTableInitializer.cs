using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.TableInitializers;

public class OrganizationTableInitializer : ITableInitializer<UserOrganization>
{
    private readonly IDbConnectionFactory dbConnectionFactory;

    public OrganizationTableInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        this.dbConnectionFactory = dbConnectionFactory;
    }

    public void InitializeTable()
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
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        dbConnection.Execute(sql);
    }
}