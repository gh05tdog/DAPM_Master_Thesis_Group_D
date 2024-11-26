using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.TableInitializers;

public class RepositoryTableInitializer : ITableInitializer<UserRepository>
{
    private readonly IDbConnectionFactory dbConnectionFactory;

    public RepositoryTableInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        this.dbConnectionFactory = dbConnectionFactory;
    }

    public void InitializeTable()
    {
        const string sql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRepositories' AND xtype='U')
            BEGIN
                CREATE TABLE UserRepositories (
                    UserId NVARCHAR(50) NOT NULL,
                    RepositoryId NVARCHAR(50) NOT NULL,
                    PRIMARY KEY (UserId, RepositoryId)
                );
            END
        ";
        
        using var dbConnection = dbConnectionFactory.CreateConnection();
        dbConnection.Open();
        
        dbConnection.Execute(sql);
    }
}