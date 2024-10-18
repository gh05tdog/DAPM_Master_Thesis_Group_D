namespace DAPM.Test.EndToEnd.Models;

public record TokenResponse
{
    
    public string Access_Token { get; init; }
    public int Expires_In { get; init; }
    public int Refresh_Expires_In { get; init; }
    public string Refresh_Token { get; init; }
    public string Token_Type { get; init; }
    public int Not_Before_Policy { get; init; }
    public string Session_State { get; init; }
    public string Scope { get; init; }
}