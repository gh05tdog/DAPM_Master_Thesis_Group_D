namespace DAPM.AccessControlService.Infrastructure.TableInitializers;

public interface ITableInitializer<T>
{
    Task InitializeTable();
}