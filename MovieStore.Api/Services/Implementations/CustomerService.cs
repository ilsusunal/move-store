using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MovieStore.Api.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer == null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerRequest request)
        {
            var customer = _mapper.Map<Customer>(request);
            customer.PasswordHash = HashPassword(request.Password);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCustomerRequest request)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _mapper.Map(request, customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
