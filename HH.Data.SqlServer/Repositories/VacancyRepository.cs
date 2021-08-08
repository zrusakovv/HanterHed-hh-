﻿using HH.Core;
using HH.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HH.Data.SqlServer
{
    public class VacancyRepository : RepositoryBase<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(DbContext dbContext)
            : base(dbContext) { }

        public void CreateVacancyForCompany(Guid companyId, Vacancy vacancy)
        {
            vacancy.CompanyId = companyId;
            Create(vacancy);
        }

        public void DeleteVacancy(Vacancy vacancy)
        {
            Delete(vacancy);
        }

        public async Task<Vacancy> GetVacancyAsync(Guid companyId, Guid id) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id))
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Vacancy>> GetVacancysAsync(Guid companyId) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId))
            .OrderBy(e => e.Name)
            .ToListAsync();

    }

}