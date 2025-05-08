namespace MovieStore.Api.Models.Requests
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
    }
}
