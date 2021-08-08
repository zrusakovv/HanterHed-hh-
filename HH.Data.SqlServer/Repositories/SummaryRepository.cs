using HH.Core;
using HH.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH.Data.SqlServer
{
    public class SummaryRepository : RepositoryBase<Summary>, ISummaryRepository
    {
        public SummaryRepository(DbContext dbContext)
            : base(dbContext) { }


        public void CreateSummaryForCompany(Guid summaryId, Summary summary)
        {
            summary.EmployeeId = summaryId;
            Create(summary);
        }

        public void DeleteSummary(Summary summary)
        {
            Delete(summary);
        }

        public async Task<Summary> GetSummaryAsync(Guid summaryId, Guid id) =>
            await FindByCondition(e => e.EmployeeId.Equals(summaryId) && e.Id.Equals(id))
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Summary>> GetSummarysAsync(Guid employeeId) =>
            await FindByCondition(e => e.EmployeeId.Equals(employeeId))
            .OrderBy(e => e.FirstName)
            .ToListAsync();
    }
}
