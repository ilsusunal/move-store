using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Interfaces;

namespace MovieStore.Api.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Movie)
                .Include(o => o.Customer)
                .OrderByDescending(o => o.PurchaseDate)
                .ToListAsync();

            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetByCustomerIdAsync(int customerId)
        {
            var orders = await _context.Orders
                .Include(o => o.Movie)
                .Include(o => o.Customer)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.PurchaseDate)
                .ToListAsync();

            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateAsync(CreateOrderRequest request)
        {
            var movie = await _context.Movies.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == request.MovieId);
            var customer = await _context.Customers.FindAsync(request.CustomerId);

            if (movie == null || customer == null)
                throw new Exception("Geçersiz film veya müşteri.");

            var order = new Order
            {
                MovieId = movie.Id,
                CustomerId = customer.Id,
                Price = movie.Price,
                PurchaseDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }
    }
}
