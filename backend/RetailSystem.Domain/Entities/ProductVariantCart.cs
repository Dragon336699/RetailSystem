namespace RetailSystem.Domain.Entities
{
    public class ProductVariantCart
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid CartId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; } = null!;
        public ProductVariant ProductVariant { get; set; } = null!;
    }
}
