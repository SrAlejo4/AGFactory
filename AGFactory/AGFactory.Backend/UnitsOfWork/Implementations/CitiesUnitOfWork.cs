using AGFactory.Backend.Repositories.Interfaces;
using AGFactory.Backend.UnitsOfWork.Interface;
using AGFactory.Shared.DTOs;
using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;

namespace AGFactory.Backend.UnitsOfWork.Implementations;

public class CitiesUnitOfWork : GenericUnitOfWork<City>, ICitiesUnitOfWork
{
    private readonly ICitiesRepository _citiesRepository;

    public CitiesUnitOfWork(IGenericRepository<City> repository, ICitiesRepository citiesRepository) : base(repository)
    {
        _citiesRepository = citiesRepository;
    }

    public async Task<IEnumerable<City>> GetComboAsync(int stateId) => await _citiesRepository.GetComboAsync(stateId);

    public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination) => await _citiesRepository.GetAsync(pagination);

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _citiesRepository.GetTotalRecordsAsync(pagination);
}