namespace TestUtilities;

public class KeycloakConfig
{
    public string BaseUrl { get; set; }
    public string Realm { get; set; }
    public string ClientId { get; set; }
    public Dictionary<string, KeycloakUser> Users { get; set; }
}