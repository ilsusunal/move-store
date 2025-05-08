using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Entities;

namespace MovieStore.Api.Data
{
    public class MovieStoreDbContext : DbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options)
            : base(options) { }

        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Movie - Actor M:N
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity(j => j.ToTable("MovieActors"));

            // Customer - FavoriteGenres M:N
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.FavoriteGenres)
                .WithMany()
                .UsingEntity(j => j.ToTable("CustomerGenres"));

            // Director - Movie 1:N
            modelBuilder.Entity<Director>()
                .HasMany(d => d.DirectedMovies)
                .WithOne(m => m.Director)
                .HasForeignKey(m => m.DirectorId);

            // Genre - Movie 1:N
            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Movies)
                .WithOne(m => m.Genre)
                .HasForeignKey(m => m.GenreId);

            // Soft Delete Global Filter
            modelBuilder.Entity<Movie>().HasQueryFilter(m => m.IsActive);
            modelBuilder.Entity<Genre>().HasQueryFilter(g => g.IsActive);
        }
    }
}
