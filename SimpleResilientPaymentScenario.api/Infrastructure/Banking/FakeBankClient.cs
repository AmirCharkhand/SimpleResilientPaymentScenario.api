using SimpleResilientPaymentScenario.api.Domain.Contracts;
using SimpleResilientPaymentScenario.api.Domain.Contracts.Interfaces;

namespace SimpleResilientPaymentScenario.api.Infrastructure.Banking;

public class FakeBankClient : IBankClient
{
    public async Task<BankResult> Pay(string orderId, long amount, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken); // simulate network latency
        return new BankResult(true);
    }
}