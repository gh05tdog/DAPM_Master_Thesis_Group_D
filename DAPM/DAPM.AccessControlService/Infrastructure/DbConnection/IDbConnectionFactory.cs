using System.Data;

namespace DAPM.AccessControlService.Infrastructure;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}