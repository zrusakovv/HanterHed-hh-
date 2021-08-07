using AutoMapper;
using DataAbstraction;
using DataTransferObjects.Employee;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HanterHed_hh_.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        public EmployeeController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employee = await repository.Employee.GetAllEmployeesAsync();

            var employeeDto = mapper.Map<IEnumerable<EmployeeDto>>(employee);

            return Ok(employeeDto);
        }

        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var employee = await repository.Employee.GetEmployeeAsync(id);

            if (employee == null)
            {
                logger.LogInfo($"Сотрудник с индификатором {id} не существует");

                return NotFound();
            }
            else
            {
                var employeeDto = mapper.Map<EmployeeDto>(employee);

                return Ok(employeeDto);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForCreationDto employee)
        {
            if (employee == null)
            {
                logger.LogError("EmployeeForCreationDto отправленный клиентом, имеет значение NULL.");

                return BadRequest("EmployeeForCreationDto объект имеет значение null");
            }

            var employeeEntity = mapper.Map<Employee>(employee);

            repository.Employee.CreateEmployee(employeeEntity);
            await repository.SaveAsync();

            var employeeToReturn = mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("EmployeeById", new
            {
                id = employeeToReturn.Id
            }, employeeToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await repository.Employee.GetEmployeeAsync(id);

            if (employee == null)
            {
                logger.LogInfo($"Сотрудник с идентификатором: {id} не существует в базе данных.");

                return NotFound();
            }

            repository.Employee.DeleteEmployee(employee);
            await repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee == null)
            {
                logger.LogError("Объект EmployeeForUpdateDto, отправленный клиентом, имеет значение NULL.");

                return BadRequest("Объект EmployeeForUpdateDto имеет значение NULL.");
            }

            var employeeEntity = await repository.Employee.GetEmployeeAsync(id);

            if (employeeEntity == null)
            {
                logger.LogInfo($"Сотрудниу с идентификатором: {id} не существует в базе данных.");

                return NotFound();
            }

            mapper.Map(employee, employeeEntity);
            await repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployee(Guid id, [FromBody] JsonPatchDocument<EmployeeForPatchDto> employee)
        {
            if (employee == null)
            {
                logger.LogError("Объект EmployeeForPatchDto, отправленный клиентом, имеет значение NULL.");

                return BadRequest("Объект EmployeeForPatchDto имеет значение NULL.");
            }

            var employeeEntity = await repository.Employee.GetEmployeeAsync(id);

            if (employeeEntity == null)
            {
                logger.LogInfo($"Сотрудник с идентификатором: {id} не существует в базе данных.");

                return NotFound();
            }

            var employeeToPatch = mapper.Map<EmployeeForPatchDto>(employeeEntity);

            employee.ApplyTo(employeeToPatch);

            mapper.Map(employeeToPatch, employeeEntity);

            await repository.SaveAsync();

            return NoContent();
        }
    }
}
