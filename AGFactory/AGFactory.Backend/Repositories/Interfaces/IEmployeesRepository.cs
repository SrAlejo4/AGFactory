using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;

namespace AGFactory.Backend.Repositories.Interfaces;

public interface IEmployeesRepository
{
    Task<ActionResponse<IEnumerable<Employee>>> GetByNameLastNameAsync(string search);
}