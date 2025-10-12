using AGFactory.Backend.Data;
using AGFactory.Backend.Helpers;
using AGFactory.Backend.Repositories.Interfaces;
using AGFactory.Shared.DTOs;
using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AGFactory.Backend.Repositories.Implementations;

public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
{
    private readonly DataContext _context;

    public EmployeesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            var filter = pagination.Filter.ToLower();
            queryable = queryable.Where(x =>
                x.FirstName.ToLower().Contains(filter) ||
                x.LastName.ToLower().Contains(filter)
                );
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            var filter = pagination.Filter.ToLower();
            queryable = queryable.Where(x =>
                x.FirstName.ToLower().Contains(filter) ||
                x.LastName.ToLower().Contains(filter)
                );
        }

        return new ActionResponse<IEnumerable<Employee>>
        {
            WasSuccess = true,
            Result = await queryable
            .Paginate(pagination)
            .ToListAsync()
        };
    }
}