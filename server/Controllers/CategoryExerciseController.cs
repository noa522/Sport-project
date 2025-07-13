using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace computerized_sports_course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryExerciseController : ControllerBase
    {
        private readonly IService<CategoryExercise> service;

        public CategoryExerciseController(IService<CategoryExercise> _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryExercise>>> GetAll()
        {
            return Ok(await service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryExercise>> GetById(int id)
        {
            var item = await service.GetById(id);
            if (item == null)
                return NotFound("CategoryExercise not found");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryExercise([FromBody] CategoryExercise item)
        {
            await service.AddItem(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryExercise item)
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
