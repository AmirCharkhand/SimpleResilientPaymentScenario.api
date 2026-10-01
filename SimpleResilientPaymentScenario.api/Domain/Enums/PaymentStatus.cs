namespace SimpleResilientPaymentScenario.api.Domain.Enums;

// Values are persisted in the Status INT column, so they are explicit and must never be renumbered.
public enum PaymentStatus
{
    Pending = 0,
    Succeeded = 1,
    Failed = 2,
    Unknown = 3
}