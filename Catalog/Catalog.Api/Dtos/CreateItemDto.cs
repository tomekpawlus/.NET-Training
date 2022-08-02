using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Dtos
{
    public class CreateItemDto
    {

        [Required]
        public string Name { get; init; } = null!;

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; init; }

    }
}

