using System.Data;
using DAPM.AccessControlService.Infrastructure;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit;

public class SqlliteConnectionFactory : IDbConnectionFactory
{
    private IDbConnection? connection;
    public IDbConnection CreateConnection()
    {
        if (connection != null) return connection;
        connection = new SqliteConnection("DataSource=:memory:;Mode=Memory;Cache=Shared;");

        return connection;
    }
}