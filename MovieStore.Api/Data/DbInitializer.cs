using MovieStore.Api.Entities;

namespace MovieStore.Api.Data
{
    public static class DbInitializer
    {
        public static void SeedGenres(MovieStoreDbContext context)
        {
            if (context.Genres.Any()) return; // zaten varsa ekleme

            var genres = new List<Genre>
            {
                new() { Name = "Aksiyon" },
                new() { Name = "Komedi" },
                new() { Name = "Dram" },
                new() { Name = "Bilim Kurgu" },
                new() { Name = "Gerilim" },
                new() { Name = "Animasyon" }
            };

            context.Genres.AddRange(genres);
            context.SaveChanges();
        }
    }
}
