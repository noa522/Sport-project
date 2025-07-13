using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace computerized_sports_course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryFitnessController : ControllerBase
    {
        private readonly IService<CategoryFitness> service;

        public CategoryFitnessController(IService<CategoryFitness> _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryFitness>>> GetAll()
        {
            return Ok(await service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryFitness>> GetById(int id)
        {
            var item = await service.GetById(id);
            if (item == null)
                return NotFound("CategoryFitness not found");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryFitness item)
        {
            await service.AddItem(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryFitness item)
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
