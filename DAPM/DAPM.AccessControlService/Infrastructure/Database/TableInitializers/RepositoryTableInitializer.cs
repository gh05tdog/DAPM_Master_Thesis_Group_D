using System.Data;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

public class RepositoryTableInitializer : ITableInitializer
{
    private readonly IDbConnection dbConnection;

    public RepositoryTableInitializer(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }


    public async Task InitializeTable()
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
        
        await dbConnection.ExecuteAsync(sql);
    }
}