using AGFactory.Shared.DTOs;
using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;

namespace AGFactory.Backend.Repositories.Interfaces;

public interface ICitiesRepository
{
    Task<IEnumerable<City>> GetComboAsync(int stateId);

    Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
}