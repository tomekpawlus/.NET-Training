using System;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Conrtollers
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
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());

            return items;
        }


        [HttpGet("{id}")]

        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
        {
            Item item = new Item()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                Name = createItemDto.Name,
                Price = createItemDto.Price
            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }
            Item updatedItem =  existingItem with{
                Name = updateItemDto.Name,
                Price = updateItemDto.Price
            };

            repository.UpdateItem(updatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existengItem = repository.GetItem(id);
            if(existengItem is null)
            {
                return NotFound();
            }

            repository.DeleteItem(id);
            return NoContent();
        }

    }
}