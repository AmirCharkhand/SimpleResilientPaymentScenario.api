using Microsoft.EntityFrameworkCore;
using SimpleResilientPaymentScenario.api.Domain.Contracts;
using SimpleResilientPaymentScenario.api.Domain.Contracts.Interfaces;
using SimpleResilientPaymentScenario.api.Domain.Models;
using SimpleResilientPaymentScenario.api.Infrastructure.Data;

namespace SimpleResilientPaymentScenario.api.Infrastructure.Services;

public class PaymentService(AppDbContext db, IBankClient bankClient) : IPaymentService
{
    public async Task<PaymentResult> Pay(PaymentRequest request, CancellationToken cancellationToken)
    {
        var exists = await db.Payments
            .AnyAsync(x => x.OrderId == request.OrderId, cancellationToken);

        if (exists)
            throw new Exception("Payment already exists");

        var payment = new Payment
        {
            OrderId = request.OrderId,
            CustomerId = request.CustomerId,
            Amount = request.Amount,
            Status = 0,
            CreatedAt = DateTime.UtcNow
        };

        db.Payments.Add(payment);
        await db.SaveChangesAsync(cancellationToken);

        var bankResult = await bankClient.Pay(
            request.OrderId,
            request.Amount,
            cancellationToken);

        payment.Status = bankResult.IsSuccessful
            ? 1
            : 2;

        await db.SaveChangesAsync(cancellationToken);

        return new PaymentResult(payment.PaymentId);
    }
}