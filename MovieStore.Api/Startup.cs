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
	services.AddScoped<ICustomerService, CustomerService>();
	services.AddScoped<IOrderService, OrderService>();

	services.AddSingleton<JwtTokenGenerator>();
	


        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

	services.AddAuthentication("Bearer")
    	.AddJwtBearer("Bearer", options =>
    	{
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
        };
    });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

		using (var scope = app.ApplicationServices.CreateScope())
    		{
        		var context = scope.ServiceProvider.GetRequiredService<MovieStoreDbContext>();
        		DbInitializer.SeedGenres(context);
    		}
        }
    }
}
