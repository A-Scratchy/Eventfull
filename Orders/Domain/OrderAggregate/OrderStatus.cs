namespace Orders.Domain.Aggregates
{
    public enum OrderStatus
    {
        New,
        AwaitingValidation,
        Confirmed,
        Paid,
        Shipped,
        Cancelled,
        Complete
    }
}