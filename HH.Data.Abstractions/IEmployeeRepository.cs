using HH.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HH.Data.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsync(Guid employeeId);
        void CreateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
