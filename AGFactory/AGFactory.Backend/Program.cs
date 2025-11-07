using AGFactory.Backend.Data;
using AGFactory.Backend.Repositories.Implementations;
using AGFactory.Backend.Repositories.Interfaces;
using AGFactory.Backend.UnitsOfWork.Implementations;
using AGFactory.Backend.UnitsOfWork.Interface;
using AGFactory.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace AGFactory.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection", sqlOptions =>
             {
                 sqlOptions.CommandTimeout(180);
             }));

            builder.Services.AddTransient<SeedDb>();

            builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
            builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
            builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            builder.Services.AddScoped<IStatesRepository, StatesRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();

            builder.Services.AddScoped<ICitiesUnitOfWork, CitiesUnitOfWork>();
            builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();
            builder.Services.AddScoped<IEmployeesUnitOfWork, EmployeesUnitOfWork>();
            builder.Services.AddScoped<IStatesUnitOfWork, StatesUnitOfWork>();
            builder.Services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();

            builder.Services.AddIdentity<User, IdentityRole>(x =>
            {
                x.User.RequireUniqueEmail = true;
                x.Password.RequireDigit = false;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
                ClockSkew = TimeSpan.Zero
            });

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