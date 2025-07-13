using Repository.Entities;

namespace computerized_sports_course.Models.Requests
{
    public class PostFitnessTrack
    {
        public DateTime Date { get; set; }

        public int ClientId { get; set; }

        public int Duration { get; set; }

        public List<PostTrackExercises> Exercises { get; set; }

    }

    public class PostTrackExercises
    {
        public int Mark { get; set; }

        public int FitnessExerciseId { get; set; }

    }
}
