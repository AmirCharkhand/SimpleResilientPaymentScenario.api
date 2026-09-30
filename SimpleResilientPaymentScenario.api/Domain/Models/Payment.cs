namespace SimpleResilientPaymentScenario.api.Domain.Models;

public class Payment
{
    public long PaymentId { get; set; }
    public string OrderId { get; set; } = default!;
    public long CustomerId { get; set; }
    public long Amount { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
}