namespace MovieStore.Api.Models.Requests
{
    public class CreateMovieRequest
    {
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public decimal Price { get; set; }

        public int GenreId { get; set; }
        public int DirectorId { get; set; }

        public List<int> ActorIds { get; set; } = new();
    }
}
