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
            _context.Employees.Add(new Employee { FirstName = "Juliana", LastName = "Morales", IsActive = true, HireDate = DateTime.Now, Salary = 1780000 });
            _context.Employees.Add(new Employee { FirstName = "Camilo", LastName = "Torres", IsActive = true, HireDate = DateTime.Now, Salary = 2985000 });
            _context.Employees.Add(new Employee { FirstName = "Valentina", LastName = "Rios", IsActive = true, HireDate = DateTime.Now, Salary = 2150000 });
            _context.Employees.Add(new Employee { FirstName = "Andres", LastName = "Lopez", IsActive = true, HireDate = DateTime.Now, Salary = 1345000 });
            _context.Employees.Add(new Employee { FirstName = "Carolina", LastName = "Martinez", IsActive = true, HireDate = DateTime.Now, Salary = 2890000 });
            _context.Employees.Add(new Employee { FirstName = "Jorge", LastName = "Fernandez", IsActive = true, HireDate = DateTime.Now, Salary = 1675000 });
            _context.Employees.Add(new Employee { FirstName = "Natalia", LastName = "Castro", IsActive = true, HireDate = DateTime.Now, Salary = 2420000 });
        }
        await _context.SaveChangesAsync();
    }
}