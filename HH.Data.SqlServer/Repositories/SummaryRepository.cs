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
    public class SummaryRepository : RepositoryBase<Summary>, ISummaryRepository
    {
        public SummaryRepository(DbContext dbContext)
            : base(dbContext) { }


        public void CreateSummaryForCompany(Guid summaryId, Summary summary, CancellationToken token = default)
        {
            summary.EmployeeId = summaryId;
            Create(summary, token);
        }

        public void DeleteSummary(Summary summary, CancellationToken token = default)
        {
            Delete(summary, token);
        }

        public async Task<Summary> GetSummaryAsync(Guid summaryId, Guid id, CancellationToken token = default) =>
            await FindByCondition(e => e.EmployeeId.Equals(summaryId) && e.Id.Equals(id), token)
            .SingleOrDefaultAsync(token);

        public async Task<IEnumerable<Summary>> GetSummarysAsync(Guid employeeId, CancellationToken token = default) =>
            await FindByCondition(e => e.EmployeeId.Equals(employeeId), token)
            .OrderBy(e => e.FirstName)
            .ToListAsync(token);
    }
}
