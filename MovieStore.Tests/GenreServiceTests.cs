using AutoMapper;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;
using MovieStore.Api.Entities;
using MovieStore.Api.Mapping;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Implementations;

namespace MovieStore.Tests
{
    public class GenreServiceTests
    {
        private readonly GenreService _genreService;
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreServiceTests()
        {
            // In-memory veritabanı
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new MovieStoreDbContext(options);

            // AutoMapper config
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GenreProfile()));
            _mapper = config.CreateMapper();

            _genreService = new GenreService(_context, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddGenre()
        {
            // Arrange
            var request = new CreateGenreRequest { Name = "Test Türü" };

            // Act
            var result = await _genreService.CreateAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Türü", result.Name);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnGenres()
        {
            // Arrange
            _context.Genres.Add(new Genre { Name = "Aksiyon" });
            _context.Genres.Add(new Genre { Name = "Komedi" });
            await _context.SaveChangesAsync();

            // Act
            var genres = await _genreService.GetAllAsync();

            // Assert
            Assert.Equal(2, genres.Count());
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteGenre()
        {
            // Arrange
            var genre = new Genre { Name = "Silinecek Tür" };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            // Act
            var result = await _genreService.DeleteAsync(genre.Id);

            // Assert
            Assert.True(result);
            var deleted = await _context.Genres.IgnoreQueryFilters().FirstOrDefaultAsync(g => g.Id == genre.Id);
            Assert.False(deleted!.IsActive);
        }
    }
}
