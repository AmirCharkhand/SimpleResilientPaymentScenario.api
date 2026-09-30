namespace SimpleResilientPaymentScenario.api.Domain.Contracts.Interfaces;

public interface IBankClient
{
    Task<BankResult> Pay(string orderId, long amount, CancellationToken cancellationToken);
}