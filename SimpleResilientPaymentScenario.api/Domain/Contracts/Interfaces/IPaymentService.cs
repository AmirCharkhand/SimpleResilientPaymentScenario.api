namespace SimpleResilientPaymentScenario.api.Domain.Contracts.Interfaces;

public interface IPaymentService
{
    Task<PaymentResult> Pay(PaymentRequest request, CancellationToken cancellationToken);
}