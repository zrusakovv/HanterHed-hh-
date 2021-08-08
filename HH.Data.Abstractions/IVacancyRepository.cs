using HH.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HH.Data.Abstractions
{
    public interface IVacancyRepository
    {
        Task<IEnumerable<Vacancy>> GetVacancysAsync(Guid companyId);
        Task<Vacancy> GetVacancyAsync(Guid companyId, Guid id);
        void CreateVacancyForCompany(Guid companyId, Vacancy vacancy);
        void DeleteVacancy(Vacancy employee);
    }
}
