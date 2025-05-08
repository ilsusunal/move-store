namespace MovieStore.Api.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
