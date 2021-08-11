using HH.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Data.Abstractions
{
    public interface ISummaryRepository
    {
        Task<IEnumerable<Summary>> GetSummarysAsync(Guid summaryId, CancellationToken token = default);
        Task<Summary> GetSummaryAsync(Guid summaryId, Guid id, CancellationToken token = default);
        void CreateSummaryForCompany(Guid summaryId, Summary summary, CancellationToken token = default);
        void DeleteSummary(Summary summary, CancellationToken token = default);
    }
}
