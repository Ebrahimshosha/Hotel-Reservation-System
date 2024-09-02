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
        try
        {
            var accessToken = await _payPalAuthService.GetAccessToken();

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
