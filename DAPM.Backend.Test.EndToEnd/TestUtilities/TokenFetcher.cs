using System.Net.Http.Json;

namespace TestUtilities;

public class TokenFetcher(Uri baseAddress, string realm, string clientId)
{
    private readonly Dictionary<string, Token> tokens = new();
    
    public async Task<string> GetTokenAsync(KeycloakUser keycloakUser)
    {
        var token = tokens.GetValueOrDefault(keycloakUser.Username);
        
        if (token != null && DateTime.UtcNow <= token.TokenExpire)
            return token.AccessToken;

        TokenResponse tokenResponse;
        if (token != null && DateTime.UtcNow <= token.RefreshTokenExpire)
            tokenResponse = await RefreshTokenAsync(token.RefreshToken);
        else
            tokenResponse = await FetchTokenAsync(keycloakUser.Username, keycloakUser.Password);

        token = new Token
        {
            AccessToken = tokenResponse.Access_Token,
            RefreshToken = tokenResponse.Refresh_Token,
            TokenExpire = DateTime.UtcNow.AddSeconds(tokenResponse.Expires_In - 30),
            RefreshTokenExpire = DateTime.UtcNow.AddSeconds(tokenResponse.Refresh_Expires_In - 30)
        };
        
        tokens[keycloakUser.Username] = token;
        
        return token.AccessToken;
    }

    private async Task<TokenResponse> FetchTokenAsync(string username, string password)
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
        });
        
        return await PostTokenRequestAsync(formContent);
    }
    
    private async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
        });
        
        return await PostTokenRequestAsync(formContent);
    }

    private async Task<TokenResponse> PostTokenRequestAsync(FormUrlEncodedContent formContent)
    {
        using var client = new HttpClient();
        client.BaseAddress = baseAddress;

        var uri = $"/realms/{realm}/protocol/openid-connect/token";
        var response = await client.PostAsync(uri, formContent);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TokenResponse>() ??
               throw new ArgumentNullException(nameof(TokenResponse));
    }
}