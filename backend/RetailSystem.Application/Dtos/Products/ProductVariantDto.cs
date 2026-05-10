namespace RetailSystem.Application.Dtos.Products
{
    public class ProductVariantDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public Guid SizeId { get; init; }
        public int StockQuantity { get; init; }
    }
}
