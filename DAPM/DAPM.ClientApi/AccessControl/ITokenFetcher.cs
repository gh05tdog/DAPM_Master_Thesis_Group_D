namespace DAPM.ClientApi.AccessControl;

public interface ITokenFetcher
{
    Task<string> GetTokenAsync();
}