using HH.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Data.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken token = default);
        Task<Employee> GetEmployeeAsync(Guid employeeId, CancellationToken token = default);
        void CreateEmployee(Employee employee, CancellationToken token = default);
        void DeleteEmployee(Employee employee, CancellationToken token = default);
    }
}
