using HH.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Core
{
    public interface ICompanyService
    {
        Task<CompanyDto[]> GetCompaniesAsync(CancellationToken token = default);

        Task<CompanyDto> GetCompanyAsync(Guid id, CancellationToken token = default);

        Task<Guid> CreateCompanyAsync(CompanyForCreationDto payload, CancellationToken token = default);

        Task<CompanyDto> UpdateCompanyAsync(Guid id, CompanyForUpdateDto payload, CancellationToken token = default);

        Task DeleteCompanyAsync(Guid id, CancellationToken token = default);
    }
}
