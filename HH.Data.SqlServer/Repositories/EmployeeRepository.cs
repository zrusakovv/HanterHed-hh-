using HH.Core;
using HH.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH.Data.SqlServer
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContext) : base(dbContext) { }

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
