using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

public class PayPalPaymentService
{
    private readonly PayPalAuthService _authService;

    public PayPalPaymentService(PayPalAuthService authService)
    {
        _authService = authService;
    }

    public async Task<string> CreatePayment()
    {
        var accessToken = await _authService.GetAccessToken();
        var client = new HttpClient(); 
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken); //Authorization header

        var payment = new
        {
            intent = "CAPTURE",
            purchase_units = new[]
            {
                new
                {
                    amount = new
                    {
                        currency_code = "USD",
                        value = "100.00" 
                    }
                }
            },
            application_context = new
            {
                return_url = "https://example.com/return",
                cancel_url = "https://example.com/cancel"
                //these URLs are endpoints to which the user will be redirected after completing or canceling the payment
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", content);

        var json = await response.Content.ReadAsStringAsync();
        var paymentResult = JsonConvert.DeserializeObject<dynamic>(json);

        return paymentResult.id; // Return the payment ID
    }

    public async Task<string> CapturePayment(string paymentId)
    {
        var accessToken = await _authService.GetAccessToken();
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.PostAsync($"https://api-m.sandbox.paypal.com/v2/checkout/orders/{paymentId}/capture", null);

        var json = await response.Content.ReadAsStringAsync();
        var captureResult = JsonConvert.DeserializeObject<dynamic>(json);

        return captureResult.status; // Return the capture status
    }
}
