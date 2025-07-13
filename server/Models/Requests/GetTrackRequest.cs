using Repository.Entities;

namespace computerized_sports_course.Models.Requests
{
    public class GetTrackRequest
    {
        public int Id { get; set; }
        public List<CategoryFitness> Ctgry { get; set; }
        public int Time { get; set; }
    }
}
