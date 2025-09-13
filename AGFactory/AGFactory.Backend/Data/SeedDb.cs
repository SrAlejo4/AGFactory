using AGFactory.Shared.Entities;
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
        await CheckEmployeesAsync();
    }

    private async Task CheckEmployeesAsync()
    {
        if (!_context.Employees.Any())
        {
            _context.Employees.Add(new Employee { FirstName = "Pablo", LastName = "Garcia", IsActive = true, HireDate = DateTime.Now, Salary = 1250000 });
            _context.Employees.Add(new Employee { FirstName = "Martin", LastName = "Sanchez", IsActive = true, HireDate = DateTime.Now, Salary = 2560000 });
            _context.Employees.Add(new Employee { FirstName = "Abelardo", LastName = "Gomez", IsActive = true, HireDate = DateTime.Now, Salary = 1423000 });
        }
        await _context.SaveChangesAsync();
    }
}