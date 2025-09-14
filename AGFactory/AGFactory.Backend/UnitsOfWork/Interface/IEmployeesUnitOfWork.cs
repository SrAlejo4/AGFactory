using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;

namespace AGFactory.Backend.UnitsOfWork.Interface;

public interface IEmployeesUnitOfWork
{
    Task<ActionResponse<IEnumerable<Employee>>> GetByNameLastNameAsync(string search);
}