namespace DAPM.ClientApi.AccessControl;

public class AccessControlConfig
{
    public string AccessControlUrl { get; set; }
    public string KeycloakUrl { get; set; }
    public string ClientId { get; set; }
    public string Realm { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}