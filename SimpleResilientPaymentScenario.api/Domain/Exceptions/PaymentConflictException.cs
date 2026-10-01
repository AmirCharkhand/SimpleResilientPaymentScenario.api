namespace SimpleResilientPaymentScenario.api.Domain.Exceptions;

public sealed class PaymentConflictException(string orderId)
    : Exception($"OrderId '{orderId}' already exists with different payment data.");