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
    public class FitnessTrackController : ControllerBase
    {
        private readonly IService<FitnessTrack> service;

        // Constructor that correctly injects the IService<FitnessTrack> service
        public FitnessTrackController(IService<FitnessTrack> service)
        {
            this.service = service;
        }

        // Get all fitness tracks filtered by clientId
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FitnessTrack>>> GetTracks(int clientId)
        {
            var tracks = await service.GetAll();

            // סינון התוצאות לפי clientId
            var filteredTracks = tracks.Where(t => t.ClientId == clientId).ToList();

            return Ok(filteredTracks); // Return filtered fitness tracks
        }


        // Get a specific fitness track by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<FitnessTrack>> GetTrack(int id)
        {
            var track = await service.GetById(id);
            if (track == null)
                return NotFound("Track not found");

            return Ok(track); // Return the requested track
        }

        // Add a new fitness track
        [HttpPost]
        public async Task<IActionResult> AddTrack([FromBody] PostFitnessTrack postTrack)
        {
            var newTrack = new FitnessTrack
            {
                ClientId = postTrack.ClientId,
                date = postTrack.Date,
                Duration = postTrack.Duration,
                TrackExercises = postTrack.Exercises.Select(e => new TrackExercise
                {
                    FitnessExerciseId = e.FitnessExerciseId,
                    Mark = e.Mark
                }).ToList()
            };

            var result = await service.AddItem(newTrack);

            return Ok(result);
        }

        // Update an existing fitness track
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrack(int id, [FromBody] FitnessTrack track)
        {
            if (id != track.Id)
                return BadRequest("Mismatched track ID"); // Return 400 if IDs do not match

            await service.UpdateItem(id, track);
            return NoContent(); // Return 204 if update is successful
        }

        // Delete a fitness track by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            await service.DeleteItem(id);
            return NoContent(); // Return 204 if deletion is successful
        }
    }
}
