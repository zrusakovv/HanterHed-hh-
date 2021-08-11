using HH.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Data.Abstractions
{
    public interface IVacancyRepository
    {
        Task<IEnumerable<Vacancy>> GetVacancysAsync(Guid companyId, CancellationToken token = default);
        Task<Vacancy> GetVacancyAsync(Guid companyId, Guid id, CancellationToken token = default);
        void CreateVacancyForCompany(Guid companyId, Vacancy vacancy, CancellationToken token = default);
        void DeleteVacancy(Vacancy employee, CancellationToken token = default);
    }
}
