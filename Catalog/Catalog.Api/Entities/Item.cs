namespace Catalog.Api.Entities
{
    public record Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}

