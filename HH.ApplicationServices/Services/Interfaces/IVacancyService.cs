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
        Task<IEnumerable<VacancyDto>> GetVacancys(Guid companyId);
        Task<VacancyDto> GetVacancy(Guid companyId, Guid id);
        Task<Guid> CreateVacancy(Guid companyId, VacancyForCreationDto vacancy);
        Task DeleteVacancy(Guid companyId, Guid id);
        Task<VacancyDto> PutVacancy(Guid companyId, Guid id, VacancyForUpdateDto vacancy);

    }
}
