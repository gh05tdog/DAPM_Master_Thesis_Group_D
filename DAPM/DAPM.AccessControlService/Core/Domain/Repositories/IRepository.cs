namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IRepository
{
    Task InitializeScheme();
    Task InitializeScheme(string sql);
}