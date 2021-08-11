using HH.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Interfaces
{
    public interface IVacancyService
    {
        Task<IEnumerable<VacancyDto>> GetVacancysAsync(Guid companyId, CancellationToken token = default);
        Task<VacancyDto> GetVacancyAsync(Guid companyId, Guid id, CancellationToken token = default);
        Task<Guid> CreateVacancyAsync(Guid companyId, VacancyForCreationDto vacancy, CancellationToken token = default);
        Task DeleteVacancyAsync(Guid companyId, Guid id, CancellationToken token = default);
        Task<VacancyDto> PutVacancyAsync(Guid companyId, Guid id, VacancyForUpdateDto vacancy, CancellationToken token = default);

    }
}
