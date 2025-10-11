using AGFactory.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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