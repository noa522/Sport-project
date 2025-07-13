using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace computerized_sports_course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackExerciseController : ControllerBase
    {
        private readonly ITrackExerciseService service;

        // Constructor that correctly injects the IService<TrackExercise> service
        public TrackExerciseController(ITrackExerciseService service)
        {
            this.service = service;
        }

        // Get all track exercises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackExercise>>> GetTrackExercises()
        {
            var trackExercises = await service.GetAll();
            return Ok(trackExercises); // Return all track exercises
        }

        // Get a specific track exercise by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackExercise>> GetTrackExercise(int id)
        {
            var trackExercise = await service.GetById(id);
            if (trackExercise == null)
                return NotFound("TrackExercise not found");

            return Ok(trackExercise); // Return the requested track exercise
        }

        [HttpGet("udTrack/{id}")]
        public async Task<ActionResult<object>> GetByIdTrack(int id)
        {
            var trackExercise = await service.GetByIdTrack(id);
            if (trackExercise == null)
                return NotFound("TrackExercise not found");

            return Ok(trackExercise);
        }


        // Add a new track exercise
        [HttpPost]
        public async Task<IActionResult> AddTrackExercise([FromBody] TrackExercise trackExercise)
        {
            await service.AddItem(trackExercise);
            return CreatedAtAction(nameof(GetTrackExercise), new { id = trackExercise.Id }, trackExercise); // Return the created track exercise with a 201 status
        }

        // Update an existing track exercise
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrackExercise(int id, [FromBody] TrackExercise trackExercise)
        {
            if (id != trackExercise.Id)
                return BadRequest("Mismatched TrackExercise ID"); // Return 400 if IDs do not match

            await service.UpdateItem(id, trackExercise);
            return NoContent(); // Return 204 if update is successful
        }

        // Delete a track exercise by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackExercise(int id)
        {
            await service.DeleteItem(id);
            return NoContent(); // Return 204 if deletion is successful
        }
    }
}
