using Hotel_Reservation_System;
using Hotel_Reservation_System.DTO;
using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.ViewModels.ResultViewModel;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PayPalAuthService _payPalAuthService;
    private readonly StoreContext _context;



    public PaymentController(PayPalAuthService payPalAuthService, StoreContext context)
    {
        _payPalAuthService = payPalAuthService;
        _context = context;
    }

    [HttpPost("create-payment")]
   
    public async Task<IActionResult> CreatePayment([FromForm] CreatePaymentViewModel viewModel)
    {
       //commit Bassant
        try
        {
            var accessToken = await _payPalAuthService.GetAccessToken();

            var paymentService = new PayPalPaymentService(_payPalAuthService);

            var paymentData = await paymentService.CreatePayment(viewModel);


            //database add

            var payment = new Payment();
            payment.ReservationID = viewModel.reservationID;
            payment.PaymentDate = DateTime.Now;
            payment.Amount = viewModel.amount;
            _context.Payments.Add(payment);
            _context.SaveChanges();

   


            return Ok(true);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpGet("getPaymentByReservation/{id}")]
    public async Task<IActionResult> getPaymentByReservation([FromRoute] int id)
    {
        var paymentDTO = await _context.Payments.Where(x => x.ReservationID == id).ToListAsync();
        return Ok(paymentDTO);
    }




    public class CreatePaymentViewModel
    {
        public int reservationID { get; set; }
        public double amount { get; set; }
    }
}
