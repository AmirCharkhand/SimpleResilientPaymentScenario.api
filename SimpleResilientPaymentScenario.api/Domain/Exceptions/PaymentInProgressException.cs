namespace SimpleResilientPaymentScenario.api.Domain.Exceptions;

public sealed class PaymentInProgressException(string orderId)
    : Exception($"A payment for OrderId '{orderId}' is still being processed.");