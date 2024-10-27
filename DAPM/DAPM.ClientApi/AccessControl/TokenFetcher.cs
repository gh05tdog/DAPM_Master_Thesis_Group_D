namespace DAPM.ClientApi.AccessControl;

public class TokenFetcher(AccessControlConfig accessControlConfig) : ITokenFetcher
{
    private Token? token;
    
    public async Task<string> GetTokenAsync()
    {
        if (token != null && DateTime.UtcNow <= token.TokenExpire)
            return token.AccessToken;

        TokenResponse tokenResponse;
        if (token != null && DateTime.UtcNow <= token.RefreshTokenExpire)
            tokenResponse = await RefreshTokenAsync(token.RefreshToken);
        else
            tokenResponse = await FetchTokenAsync();

        token = new Token
        {
            AccessToken = tokenResponse.Access_Token,
            RefreshToken = tokenResponse.Refresh_Token,
            TokenExpire = DateTime.UtcNow.AddSeconds(tokenResponse.Expires_In - 30),
            RefreshTokenExpire = DateTime.UtcNow.AddSeconds(tokenResponse.Refresh_Expires_In - 30)
        };
        
        return token.AccessToken;
    }

    private async Task<TokenResponse> FetchTokenAsync()
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", accessControlConfig.ClientId),
            new KeyValuePair<string, string>("username", accessControlConfig.Username),
            new KeyValuePair<string, string>("password", accessControlConfig.Password),
        });
        
        return await PostTokenRequestAsync(formContent);
    }
    
    private async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("client_id", accessControlConfig.ClientId),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
        });
        
        return await PostTokenRequestAsync(formContent);
    }

    private async Task<TokenResponse> PostTokenRequestAsync(FormUrlEncodedContent formContent)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(accessControlConfig.KeycloakUrl);

        var uri = $"/realms/{accessControlConfig.Realm}/protocol/openid-connect/token";
        var response = await client.PostAsync(uri, formContent);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TokenResponse>() ??
               throw new ArgumentNullException(nameof(TokenResponse));
    }
}