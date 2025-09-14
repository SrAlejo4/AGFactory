using AGFactory.Backend.Repositories.Interfaces;
using AGFactory.Backend.UnitsOfWork.Interface;
using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;
using System.Diagnostics.Metrics;

namespace AGFactory.Backend.UnitsOfWork.Implementations;

public class EmployeesUnitOfWork : GenericUnitOfWork<Employee>, IEmployeesUnitOfWork
{
    private readonly IEmployeesRepository _employeesRepository;

    public EmployeesUnitOfWork(IGenericRepository<Employee> repository, IEmployeesRepository employeesRepository) : base(repository)
    {
        _employeesRepository = employeesRepository;
    }

    public async Task<ActionResponse<IEnumerable<Employee>>> GetByNameLastNameAsync(string search) => await _employeesRepository.GetByNameLastNameAsync(search);
}