using AGFactory.Backend.UnitsOfWork.Interface;
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

        [HttpGet("search/{search}")]
        public async Task<IActionResult> GetByNameLastName(string search)
        {
            var action = await _employeesUnitOfWork.GetByNameLastNameAsync(search);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound();
        }
    }
}