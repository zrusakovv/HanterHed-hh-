using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using HH.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HH.Api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await employeeService.GetEmployeesAsync();

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var result = await employeeService.GetEmployeeAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForCreationDto employee)
        {
            var result = await employeeService.CreateEmployeeAsync(employee);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await employeeService.DeleteEmployeeAsync(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, [FromBody] EmployeeForUpdateDto employee)
        {
            var result = await employeeService.PutEmployeeAsync(id, employee);

            return Ok(result);
        }
    }
}
