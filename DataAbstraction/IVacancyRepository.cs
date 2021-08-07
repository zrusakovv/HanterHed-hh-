﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAbstraction
{
    public interface IVacancyRepository
    {
        Task<IEnumerable<Vacancy>> GetVacancysAsync(Guid companyId);
        Task<Vacancy> GetVacancyAsync(Guid companyId, Guid id);
        void CreateVacancyForCompany(Guid companyId, Vacancy vacancy);
        void DeleteVacancy(Vacancy employee);
    }
}