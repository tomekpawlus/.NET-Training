using System;
namespace Catalog.Api.Dtos
{
    public record ItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

    }
}

