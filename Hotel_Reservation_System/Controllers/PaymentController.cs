[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PayPalAuthService _payPalAuthService;

    public PaymentController(PayPalAuthService payPalAuthService)
    {
        _payPalAuthService = payPalAuthService;
    }

    [HttpPost("create-payment")]
    public async Task<IActionResult> CreatePayment()
    {
       //commit Bassant
        try
        {
            var accessToken = await _payPalAuthService.GetAccessToken();

            // Call another service to handle payment creation or handle it here
            // For example:
            var paymentService = new PayPalPaymentService(_payPalAuthService);
            var paymentId = await paymentService.CreatePayment();

            return Ok(new { PaymentId = paymentId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
