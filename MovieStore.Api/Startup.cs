using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;

namespace MovieStore.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MovieStoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

	services.AddFluentValidationAutoValidation();
	services.AddValidatorsFromAssemblyContaining<CreateGenreRequestValidator>();

	services.AddAutoMapper(typeof(Startup));

	services.AddScoped<IGenreService, GenreService>();
	services.AddScoped<IMovieService, MovieService>();
	services.AddScoped<IDirectorService, DirectorService>();
	services.AddScoped<IActorService, ActorService>();
	


        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
