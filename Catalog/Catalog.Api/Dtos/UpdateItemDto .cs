using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Dtos
{
    public class UpdateItemDto
    {

        [Required]
        public string Name { get; init; } = null!;

        [Required]
        public decimal Price { get; init; }

    }
}

