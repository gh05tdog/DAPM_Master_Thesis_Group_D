namespace DAPM.AccessControlService.Infrastructure.TableInitializers;

public interface ITableInitializer<T>
{
    void InitializeTable();
}