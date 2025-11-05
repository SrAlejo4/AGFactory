using Microsoft.EntityFrameworkCore;

namespace AGFactory.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesFullAsync();
        await CheckCountriesFullAsync();
    }

    private async Task CheckCountriesFullAsync()
    {
        if (!_context.Countries.Any())
        {
            var countriesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }

    private async Task CheckEmployeesFullAsync()
    {
        if (!_context.Employees.Any())
        {
            var countriesSQLScript = File.ReadAllText("Data\\AGFactoryScript.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }
}