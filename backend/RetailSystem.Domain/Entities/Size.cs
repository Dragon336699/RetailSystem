namespace RetailSystem.Domain.Entities
{
    public class Size
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public decimal SizeNumber { get; set; }
    }
}
