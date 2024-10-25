namespace DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

public interface ITableInitializer<T>
{
    Task InitializeTable();
}