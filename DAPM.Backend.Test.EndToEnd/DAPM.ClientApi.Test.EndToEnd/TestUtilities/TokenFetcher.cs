using System.Net.Http.Json;
using DAPM.Test.EndToEnd.Models;

namespace DAPM.Test.EndToEnd.TestUtilities;

public class TokenFetcher(Uri baseAddress, string realm, string clientId, string username, 
    string password, string clientSecret)
{
    private DateTime? tokenExpire;
    private DateTime? refreshTokenExpire;
    private string token;
    private string refreshToken;
    
    public async Task<string> GetTokenAsync()
    {
        if (tokenExpire != null && DateTime.UtcNow <= tokenExpire)
            return token;

        TokenResponse tokenResponse;
        if (refreshTokenExpire != null && DateTime.UtcNow <= refreshTokenExpire)
            tokenResponse = await RefreshTokenAsync();
        else
            tokenResponse = await FetchTokenAsync();
    
        tokenExpire = DateTime.UtcNow.AddSeconds(tokenResponse.Expires_In - 30);
        refreshTokenExpire = DateTime.UtcNow.AddSeconds(tokenResponse.Refresh_Expires_In - 30);
        
        token = tokenResponse.Access_Token;
        refreshToken = tokenResponse.Refresh_Token;
        
        return token;
    }

    private async Task<TokenResponse> FetchTokenAsync()
    {
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("client_secret", clientSecret),
        });
        
        return await PostTokenRequestAsync(formContent);
    }
    
    private async Task<TokenResponse> RefreshTokenAsync()
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
               throw new ArgumentNullException(nameof(TicketResponse));
    }
}