using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        public EmployeeService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> CreateEmployeeAsync(EmployeeForCreationDto employee, CancellationToken token = default)
        {
            if (employee == null)
            {
                throw new ArgumentNullException();
            }

            var employeeEntity = mapper.Map<Employee>(employee);

            repository.Employee.CreateEmployee(employeeEntity);
            await repository.SaveAsync();

            var employeeToReturn = mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn.Id;
        }

        public async Task DeleteEmployeeAsync(Guid id, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(id);

            if(employee == null)
            {
                throw new InvalidOperationException($"Сотрудник с идентификатором: {id} не существует в базе данных.");
            }

            repository.Employee.DeleteEmployee(employee);

            await repository.SaveAsync();
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid id, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(id);

            if (employee == null)
            {
                throw new InvalidOperationException($"Сотрудник с идентификатором: {id} не существует в базе данных.");
            }

            return mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(CancellationToken token = default)
        {
            var employees = await repository.Employee.GetAllEmployeesAsync();

            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> PutEmployeeAsync(Guid id, EmployeeForUpdateDto employee, CancellationToken token)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            var employeeEntity = await repository.Employee.GetEmployeeAsync(id);

            if (employeeEntity == null)
            {
                throw new InvalidOperationException($"Сотрудник с идентификатором: {id} не существует в базе данных.");
            }

            mapper.Map(employee, employeeEntity);

            await repository.SaveAsync();

            return mapper.Map<EmployeeDto>(employeeEntity);

        }
    }
}
