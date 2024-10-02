namespace Ordering.Domain.Models;

public class Order: Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItem = new();
    public IReadOnlyList<OrderItem> OrderItem => _orderItem.AsReadOnly();

    public Guid CustomerId { get; set; } = default!;
    public string OrderName { get; set; } = default!;
    public Address ShippingAddress { get; set; } = default!;
    public Address BillingAddress { get; set; } = default!;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItem.Sum(x => x.Price * x.Quantity);
        private set { }
    }
}
