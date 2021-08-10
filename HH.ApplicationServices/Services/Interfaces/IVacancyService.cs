using HH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Interfaces
{
    public interface IVacancyService
    {
        Task<IEnumerable<VacancyDto>> GetVacancysAsync(Guid companyId);
        Task<VacancyDto> GetVacancyAsync(Guid companyId, Guid id);
        Task<Guid> CreateVacancyAsync(Guid companyId, VacancyForCreationDto vacancy);
        Task DeleteVacancyAsync(Guid companyId, Guid id);
        Task<VacancyDto> PutVacancyAsync(Guid companyId, Guid id, VacancyForUpdateDto vacancy);

    }
}
