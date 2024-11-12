namespace DAPM.ClientApi.AccessControl;

public class Token
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? TokenExpire { get; set; }
    public DateTime? RefreshTokenExpire { get; set; }
}