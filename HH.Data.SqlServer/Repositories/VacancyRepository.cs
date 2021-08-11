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
    public class VacancyRepository : RepositoryBase<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(DbContext dbContext)
            : base(dbContext) { }

        public void CreateVacancyForCompany(Guid companyId, Vacancy vacancy, CancellationToken token = default)
        {
            vacancy.CompanyId = companyId;
            Create(vacancy, token);
        }

        public void DeleteVacancy(Vacancy vacancy, CancellationToken token = default)
        {
            Delete(vacancy, token);
        }

        public async Task<Vacancy> GetVacancyAsync(Guid companyId, Guid id, CancellationToken token = default) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), token)
            .SingleOrDefaultAsync(token);

        public async Task<IEnumerable<Vacancy>> GetVacancysAsync(Guid companyId, CancellationToken token = default) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId), token)
            .OrderBy(e => e.Name)
            .ToListAsync(token);

    }

}
