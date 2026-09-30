using Microsoft.AspNetCore.Mvc;
using SimpleResilientPaymentScenario.api.Domain.Contracts;
using SimpleResilientPaymentScenario.api.Domain.Contracts.Interfaces;

namespace SimpleResilientPaymentScenario.api.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController(IPaymentService paymentService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PaymentResult>> Pay(
        [FromBody] PaymentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await paymentService.Pay(request, cancellationToken);
        return Ok(result);
    }
}