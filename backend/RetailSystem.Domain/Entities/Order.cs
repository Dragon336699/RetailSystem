using RetailSystem.Domain.Enum;

namespace RetailSystem.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; set; }
        public string? Note { get; set; }
        public required string ShippingAddress { get; set; }
        public required string CustomerName { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public Guid PaymentId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
