using HH.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<IEnumerable<SummaryDto>> GetSummarysAsync(Guid employeeId, CancellationToken token = default);
        Task<SummaryDto> GetSummaryAsync(Guid employeeId, Guid id, CancellationToken token = default);
        Task<Guid> CreateSummaryAsync(Guid employeeId, SummaryForCreationDto summary, CancellationToken token = default);
        Task DeleteSummaryAsync(Guid employeeId, Guid id, CancellationToken token = default);
        Task<SummaryDto> UpdateSummaryAsync(Guid employeeId, Guid id, SummaryForUpdateDto summary, CancellationToken token = default);

    }
}
