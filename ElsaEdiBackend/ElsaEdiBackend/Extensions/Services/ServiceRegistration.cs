using ElsaEdiBackend.Databases;
using Microsoft.EntityFrameworkCore;

namespace ElsaEdiBackend.Extensions.Services
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddScoped<DatabaseHelper>();

            // DbContext -- Do Not Delete
            var connectionString = configuration.GetConnectionString("Elsa");
            if (string.IsNullOrEmpty(connectionString))
            {
                // this makes local migrations easier to manage. feel free to refactor if desired.
                // this makes local migrations easier to manage. feel free to refactor if desired.
                connectionString = env.IsDevelopment()
                    ? "Data Source=elsa.sqlite.db;Cache=Shared;"
                    : throw new Exception("DB_CONNECTION_STRING environment variable is not set.");
            }

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString,
                    builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }
    }
}