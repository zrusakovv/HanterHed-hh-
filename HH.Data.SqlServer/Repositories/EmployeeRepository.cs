using HH.Core;
using HH.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Data.SqlServer
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext dbContext) : base(dbContext) { }

        public void CreateEmployee(Employee employee, CancellationToken token = default) =>
            Create(employee, token);
        public void DeleteEmployee(Employee employee, CancellationToken token = default) =>
            Delete(employee, token);

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken token = default) =>
            await FindAll(token)
            .OrderBy(c => c.Name)
            .ToListAsync(token);

        public async Task<Employee> GetEmployeeAsync(Guid employeeId, CancellationToken token = default) =>
            await FindByCondition(c => c.Id.Equals(employeeId), token)
            .SingleOrDefaultAsync(token);


    }
}
