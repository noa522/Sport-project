using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using computerized_sports_course.Models.Requests;

namespace computerized_sports_course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessExerciseController : ControllerBase
    {
        private readonly IFitnessExerciseService service;


        public FitnessExerciseController(IFitnessExerciseService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FitnessExercise>>> GetExercises()
        {
            return Ok(await service.GetAll());
        }

        [HttpPost("by-time")]
        public async Task<ActionResult<IEnumerable<FitnessExercise>>> GetByTime([FromBody] GetTrackRequest request)
        {
            return Ok(await service.GetByTime(request.Id, request.Ctgry, request.Time));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FitnessExercise>> GetExercise(int id)
        {
            var exercise = await service.GetById(id);
            if (exercise == null)
                return NotFound("Exercise not found");

            return Ok(exercise);
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise([FromBody] FitnessExercise exercise)
        {
            await service.AddItem(exercise);
            return CreatedAtAction(nameof(GetExercise), new { id = exercise.Id }, exercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] FitnessExercise exercise)
        {
            if (id != exercise.Id)
                return BadRequest("Mismatched exercise ID");

            await service.UpdateItem(id, exercise);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            await service.DeleteItem(id);
            return NoContent();
        }
    }
}
