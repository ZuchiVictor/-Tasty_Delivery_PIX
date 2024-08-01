// src/Controllers/PaymentController.cs
using Microsoft.AspNetCore.Mvc;
using PixMicroservice.DAO;
using PixMicroservice.Events;
using PixMicroservice.Sagas;


[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PaymentSaga _paymentSaga;

    public PaymentController(PaymentSaga paymentSaga)
    {
        _paymentSaga = paymentSaga;
    }

    [HttpPost]
    public IActionResult InitiatePayment([FromBody] PixTransaction paymentEvent)
    {
        _paymentSaga.StartSaga(paymentEvent);
        return Ok(new { Message = "Payment initiated", TransactionId = paymentEvent.Id });
    }
}
