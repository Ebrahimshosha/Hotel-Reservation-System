using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

public class PayPalAuthService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private string _accessToken;
    private DateTime _tokenExpiryTime;

    public PayPalAuthService(string clientId, string clientSecret)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    public async Task<string> GetAccessToken()
    {
        if (string.IsNullOrEmpty(_accessToken) || DateTime.UtcNow >= _tokenExpiryTime)
        {
            await Authenticate();
        }

        return _accessToken;
    }

    private async Task Authenticate()
    {
        var client = new HttpClient(); // created to make the API request
        var authToken = Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

        var body = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" }
        };

        var content = new FormUrlEncodedContent(body);
        var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", content);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<dynamic>(json);

        _accessToken = tokenResponse.access_token;
        int expiresIn = tokenResponse.expires_in;
        _tokenExpiryTime = DateTime.UtcNow.AddSeconds(expiresIn);
    }
}
