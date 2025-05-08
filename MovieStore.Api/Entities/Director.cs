namespace MovieStore.Api.Entities
{
    public class Director
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<Movie> DirectedMovies { get; set; } = new List<Movie>();
    }
}
