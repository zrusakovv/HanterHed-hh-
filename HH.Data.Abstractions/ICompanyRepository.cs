using HH.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Data.Abstractions
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(CancellationToken token = default);
        Task<Company> GetCompanyAsync(Guid companyId, CancellationToken token = default);
        void CreateCompany(Company company, CancellationToken token = default);
        void DeleteCompany(Company company, CancellationToken token = default);
    }
}
