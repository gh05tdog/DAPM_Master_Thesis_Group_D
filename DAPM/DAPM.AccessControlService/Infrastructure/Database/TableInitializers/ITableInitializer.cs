namespace DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

public interface ITableInitializer
{
    Task InitializeTable();
}