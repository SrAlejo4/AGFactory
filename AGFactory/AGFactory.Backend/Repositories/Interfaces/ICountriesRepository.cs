using AGFactory.Shared.DTOs;
using AGFactory.Shared.Responses;
using AGFactory.Shared.Entities;

namespace AGFactory.Backend.Repositories.Interfaces;

public interface ICountriesRepository
{
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync();
}