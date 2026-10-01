using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleResilientPaymentScenario.api.Domain.Contracts;
using SimpleResilientPaymentScenario.api.Domain.Contracts.Interfaces;
using SimpleResilientPaymentScenario.api.Domain.Exceptions;
using SimpleResilientPaymentScenario.api.Domain.Models;
using SimpleResilientPaymentScenario.api.Infrastructure.Data;

namespace SimpleResilientPaymentScenario.api.Infrastructure.Services;

public class PaymentService(AppDbContext db, IBankClient bankClient) : IPaymentService
{
    public async Task<PaymentResult> Pay(PaymentRequest request, CancellationToken cancellationToken)
    {
        var payment = new Payment
        {
            OrderId = request.OrderId,
            CustomerId = request.CustomerId,
            Amount = request.Amount,
            Status = 0,
            CreatedAt = DateTime.UtcNow
        };

        db.Payments.Add(payment);

        try
        {
            // The unique index on OrderId decides the winner if requests race.
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            db.Entry(payment).State = EntityState.Detached;
            return await HandleDuplicateAsync(request, cancellationToken);
        }

        // Only the request that inserted the row reaches the bank.
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

    private async Task<PaymentResult> HandleDuplicateAsync(PaymentRequest request, CancellationToken cancellationToken)
    {
        var existing = await db.Payments
            .AsNoTracking()
            .SingleAsync(x => x.OrderId == request.OrderId, cancellationToken);

        if (existing.CustomerId != request.CustomerId || existing.Amount != request.Amount)
            throw new PaymentConflictException(request.OrderId);

        if (existing.Status == 0)
            throw new PaymentInProgressException(request.OrderId);

        return new PaymentResult(existing.PaymentId);
    }

    // 2601 = duplicate key in a unique index, 2627 = unique constraint violation
    private static bool IsUniqueViolation(DbUpdateException ex) =>
        ex.InnerException is SqlException { Number: 2601 or 2627 };
}