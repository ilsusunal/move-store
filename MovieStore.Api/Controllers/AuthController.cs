using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;
using MovieStore.Api.Helpers;
using MovieStore.Api.Models.Requests;
using System.Security.Cryptography;
using System.Text;

namespace MovieStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MovieStoreDbContext _context;
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthController(MovieStoreDbContext context, JwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var hashed = HashPassword(request.Password);

            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Email == request.Email && x.PasswordHash == hashed);

            if (customer == null)
                return Unauthorized("E-posta ya da şifre yanlış.");

            var token = _tokenGenerator.GenerateToken(customer);
            return Ok(new { Token = token });
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
