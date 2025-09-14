using AGFactory.Backend.Data;
using AGFactory.Backend.Repositories.Implementations;
using AGFactory.Backend.Repositories.Interfaces;
using AGFactory.Backend.UnitsOfWork.Implementations;
using AGFactory.Backend.UnitsOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace AGFactory.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
            builder.Services.AddTransient<SeedDb>();

            builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            builder.Services.AddScoped<IEmployeesUnitOfWork, EmployeesUnitOfWork>();

            var app = builder.Build();
            SeedData(app);

            void SeedData(WebApplication app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory!.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<SeedDb>();
                    service!.SeedAsync().Wait();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}