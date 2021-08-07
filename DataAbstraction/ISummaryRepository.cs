using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAbstraction
{
    public interface ISummaryRepository
    {
        Task<IEnumerable<Summary>> GetSummarysAsync(Guid summaryId);
        Task<Summary> GetSummaryAsync(Guid summaryId, Guid id);
        void CreateSummaryForCompany(Guid summaryId, Summary summary);
        void DeleteSummary(Summary summary);
    }
}
