using Employees.Models;
using Employees.Sevices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Employees.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public JsonFileEmployeeService EmployeeService { get; }

        public EmployeesController(JsonFileEmployeeService productService)
        {
            this.EmployeeService = productService;
        }

        [HttpGet("GetEmployees")]
        public IEnumerable<Employee> GetAll() => EmployeeService.GetAllEmployees();


        [HttpGet("{lastName}")]
        public ActionResult<Employee> Get(string lastName)
        {
            var employee = EmployeeService.Get(lastName);

            if (employee == null)
                return NotFound();
            return employee;
        }


        [HttpPut("targetName")]
        public ActionResult<Employee> UpdateEmployee( string targetName, string firstName, string lastName, int years, string jobTitle)
        {
            EmployeeService.UpdateEmployee(targetName, firstName, lastName, years, jobTitle);
            return NoContent();
        }


        [HttpPost]
        public ActionResult<Employee> CreateEmployee(string firstName, string lastName, int years, string jobTitle)
        {
            EmployeeService.CreateEmployee(firstName, lastName, years, jobTitle);
            return NoContent();
        }


        [HttpDelete("targetName")]
        public ActionResult<Employee> DeleteEmployee(string targetName)
        {
            EmployeeService.DeleteEmployee(targetName);
            return NoContent();
        }
    }
}
