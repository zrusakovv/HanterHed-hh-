using HH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<IEnumerable<SummaryDto>> GetSummarysAsync(Guid id, CancellationToken token = default);
        //Task<IEnumerable<SummaryDto>> GetSummaryAsync(Guid summaryId, Guid id, CancellationToken token = default);
        Task<SummaryDto> CreateSummaryAsync(Guid employeeId, SummaryForCreationDto summary, CancellationToken token = default);
        Task DeleteSummaryAsync(Guid id, Guid summaryId, CancellationToken token = default);
        //Task<SummaryDto> PutSummaryAsync(Guid id, Guid summaryId, CancellationToken token = default);
    }
}
