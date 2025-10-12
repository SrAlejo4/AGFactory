using AGFactory.Backend.Repositories.Interfaces;
using AGFactory.Backend.UnitsOfWork.Interface;
using AGFactory.Shared.DTOs;
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

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
        => await _employeesRepository.GetTotalRecordsAsync(pagination);

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination)
        => await _employeesRepository.GetAsync(pagination);
}