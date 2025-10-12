using AGFactory.Backend.UnitsOfWork.Interface;
using AGFactory.Shared.DTOs;
using AGFactory.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace AGFactory.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : GenericController<Employee>
    {
        private readonly IEmployeesUnitOfWork _employeesUnitOfWork;

        public EmployeesController(IGenericUnitOfWork<Employee> unitOfWork, IEmployeesUnitOfWork employeesUnitOfWork) : base(unitOfWork)
        {
            _employeesUnitOfWork = employeesUnitOfWork;
        }

        [HttpGet("totalRecords")]
        public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _employeesUnitOfWork.GetTotalRecordsAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("paginated")]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var response = await _employeesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
    }
}