using AGFactory.Shared.DTOs;
using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;
using System.Diagnostics.Metrics;

namespace AGFactory.Backend.UnitsOfWork.Interface;

public interface IEmployeesUnitOfWork
{
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Employee>>> GetByNameLastNameAsync(string search);
}