namespace SimpleResilientPaymentScenario.api.Domain.Exceptions;

public sealed class PaymentStatusUnknownException(string orderId)
    : Exception($"The status of the payment for OrderId '{orderId}' is unknown: the bank may or may not have processed it. " +
                "Do not retry with a new OrderId; the payment must be verified with the bank first.");