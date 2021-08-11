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
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext dbContext)
            : base(dbContext) { }

        public void CreateCompany(Company company, CancellationToken token = default) =>
            Create(company, token);

        public void DeleteCompany(Company company, CancellationToken token = default) =>
            Delete(company, token);

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(CancellationToken token = default) =>
            await FindAll(token)
            .OrderBy(c => c.Name)
            .ToListAsync(token);

        public async Task<Company> GetCompanyAsync(Guid companyId, CancellationToken token = default) =>
            await FindByCondition(c => c.Id.Equals(companyId), token)
            .SingleOrDefaultAsync(token);
    }
}
