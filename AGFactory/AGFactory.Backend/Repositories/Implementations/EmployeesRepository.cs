using AGFactory.Backend.Data;
using AGFactory.Backend.Repositories.Interfaces;
using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace AGFactory.Backend.Repositories.Implementations;

public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
{
    private readonly DataContext _context;

    public EmployeesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ActionResponse<IEnumerable<Employee>>> GetByNameLastNameAsync(string search)
    {
        var employees = await _context.Employees
            .Where(e => e.FirstName.Contains(search) || e.LastName.Contains(search))
            .ToListAsync();

        if (!employees.Any())
        {
            return new ActionResponse<IEnumerable<Employee>>
            {
                WasSuccess = false,
                Message = "No se encontraron empleados con ese criterio"
            };
        }

        return new ActionResponse<IEnumerable<Employee>>
        {
            WasSuccess = true,
            Result = employees
        };
    }
}