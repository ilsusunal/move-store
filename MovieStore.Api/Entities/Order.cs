namespace MovieStore.Api.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}
