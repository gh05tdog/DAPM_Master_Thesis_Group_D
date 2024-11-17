using System.Data;
using System.Data.SqlClient;

namespace DAPM.AccessControlService.Infrastructure;

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateConnection()
        => new SqlConnection(connectionString);
}