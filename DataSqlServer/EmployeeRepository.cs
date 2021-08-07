using Data.Abstractions;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.SqlServer
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public void CreateEmployee(Employee employee) =>
            Create(employee);

        public void DeleteEmployee(Employee employee) =>
            Delete(employee);

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync() =>
            await FindAll()
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Employee> GetEmployeeAsync(Guid employeeId) =>
            await FindByCondition(c => c.Id.Equals(employeeId))
            .SingleOrDefaultAsync();


    }
}
