namespace RetailSystem.Domain.Entities
{
    public class OrderDetail
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public bool IsReviewed { get; set; } = false;
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public Order Order { get; set; } = null!;
        public ProductVariant ProductVariant { get; set; } = null!;
    }
}
