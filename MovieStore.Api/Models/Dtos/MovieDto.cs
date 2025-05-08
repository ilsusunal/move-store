namespace MovieStore.Api.Models.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string GenreName { get; set; } = null!;
        public string DirectorName { get; set; } = null!;
    }
}
