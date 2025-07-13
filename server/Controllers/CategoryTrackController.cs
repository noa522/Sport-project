using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace computerized_sports_course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryTrackController : ControllerBase
    {
        private readonly IService<CategoryTrack> service;

        // Constructor should inject IService<CategoryTrack>
        public CategoryTrackController(IService<CategoryTrack> _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryTrack>>> GetAll()
        {
            return Ok(await service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryTrack>> GetById(int id)
        {
            var item = await service.GetById(id);
            if (item == null)
                return NotFound("CategoryTrack not found");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryTrack item)
        {
            await service.AddItem(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryTrack item)
        {
            if (id != item.Id)
                return BadRequest("Mismatched ID");

            await service.UpdateItem(id, item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteItem(id);
            return NoContent();
        }
    }
}
