namespace SimpleResilientPaymentScenario.api.Domain.Contracts;

public sealed record PaymentRequest(string OrderId, long CustomerId, long Amount);