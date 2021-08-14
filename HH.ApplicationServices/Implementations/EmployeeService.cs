using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HH.ApplicationServices.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        public EmployeeService(IRepository repository, IMapper mapper)
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
            
            await repository.Create(employeeEntity, token);
            
            var employeeToReturn = mapper.Map<EmployeeDto>(employeeEntity);
            
            return employeeToReturn.Id;
        }

        public async Task DeleteEmployeeAsync(Guid id, CancellationToken token = default)
        {
            var employee = await repository.SingleOrDefaultAsync<Employee>(x=>x.Id == id, token: token);
            
            if(employee == null)
            {
                throw new EntityNotFoundException($"Сотрудник с идентификатором: {id} не существует в базе данных.");
            }
            
            await repository.DeleteAsync(employee, token);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid id, CancellationToken token = default)
        {
            var employee = await repository.SingleOrDefaultAsync<Employee>(
                x => x.Id == id,
                x => x.Summaries,
                token
            );
            
            if (employee == null)
            {
                throw new EntityNotFoundException($"Сотрудник с идентификатором: {id} не существует в базе данных.");
            }
            
            return mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(CancellationToken token = default)
        {
            var employees = await repository.FindAll<Employee>().ToListAsync(token);
            
            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> PutEmployeeAsync(Guid id, EmployeeForUpdateDto employee, CancellationToken token)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            var employeeEntity = await repository.SingleOrDefaultAsync<Employee>(x => x.Id == id, token: token);
            
            if (employeeEntity == null)
            {
                throw new EntityNotFoundException($"Сотрудник с идентификатором: {id} не существует в базе данных.");
            }
            
            mapper.Map(employee, employeeEntity);
            
            await repository.Update(employeeEntity, token);
            
            return mapper.Map<EmployeeDto>(employeeEntity);
        }
    }
}
