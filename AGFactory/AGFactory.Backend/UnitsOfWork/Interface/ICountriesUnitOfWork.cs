using AGFactory.Shared.DTOs;
using AGFactory.Shared.Entities;
using AGFactory.Shared.Responses;

namespace AGFactory.Backend.UnitsOfWork.Interface;

public interface ICountriesUnitOfWork
{
    Task<IEnumerable<Country>> GetComboAsync();

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync();
}