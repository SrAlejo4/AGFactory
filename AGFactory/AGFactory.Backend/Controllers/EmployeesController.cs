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
        public EmployeesController(IGenericUnitOfWork<Employee> unit) : base(unit)
        {
        }
    }
}