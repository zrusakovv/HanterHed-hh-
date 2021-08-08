using HH.Core;
using HH.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH.Data.SqlServer
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext dbContext)
            : base(dbContext) { }

        public void CreateCompany(Company company) =>
            Create(company);

        public void DeleteCompany(Company company) =>
            Delete(company);

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync() =>
            await FindAll()
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Company> GetCompanyAsync(Guid companyId) =>
            await FindByCondition(c => c.Id.Equals(companyId))
            .SingleOrDefaultAsync();
    }
}
