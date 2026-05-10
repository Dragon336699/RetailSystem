namespace RetailSystem.Domain.Entities
{
    public class ProductVariant
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Guid SizeId { get; set; }
        public int StockQuantity { get; set; }
        public Product Product { get; set; } = null!;
        public Size Size { get; set; } = null!;
        public ICollection<ProductVariantCart> ProductVariantCarts { get; set; } = new List<ProductVariantCart>();
    }
}
