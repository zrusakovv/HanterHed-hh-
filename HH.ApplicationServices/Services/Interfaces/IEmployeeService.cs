using HH.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(CancellationToken token = default);
        Task<EmployeeDto> GetEmployeeAsync(Guid id, CancellationToken token = default);
        Task<Guid> CreateEmployeeAsync(EmployeeForCreationDto employee, CancellationToken token = default);
        Task DeleteEmployeeAsync(Guid id, CancellationToken token = default);
        Task<EmployeeDto> PutEmployeeAsync(Guid id, EmployeeForUpdateDto employee, CancellationToken token = default);
    }
}
