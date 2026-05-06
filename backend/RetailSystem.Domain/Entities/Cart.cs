namespace RetailSystem.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public int TotalProducts { get; set; } = 0;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<ProductVariantCart> ProductVariantCarts { get; set; } = new List<ProductVariantCart>();
    }
}
