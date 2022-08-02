using System;
using Catalog.Api.Dtos;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Conrtollers
{

    [ApiController]
    [Route("items")]

    public class ItemController : ControllerBase
    {
        private IItemRepository repository;

        public ItemController(IItemRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());

            return items;
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto)
        {
            Item item = new Item()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                Name = createItemDto.Name,
                Price = createItemDto.Price
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }
            Item updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existengItem = await repository.GetItemAsync(id);
            if (existengItem is null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);
            return NoContent();
        }

    }
}