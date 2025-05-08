namespace MovieStore.Api.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
