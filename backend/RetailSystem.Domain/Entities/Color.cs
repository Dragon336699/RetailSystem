namespace RetailSystem.Domain.Entities
{
    public class Color
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string ColorName { get; set; }
        public required string ColorCode { get; set; }
    }
}
