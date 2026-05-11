namespace RetailSystem.SharedLibrary.Dtos.Products
{
    public class UploadProductSizeDto
    {
        public Guid? Id { get; init; }
        public Guid SizeId { get; init; }
        public int Quantity { get; init; }
    }
}
