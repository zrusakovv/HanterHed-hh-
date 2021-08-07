﻿using DataAbstraction;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSqlServer
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public void CreateCompany(Company company) =>
            Create(company);

        public void DeleteCompany(Company company) =>
            Delete(company);

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync() =>
            await FindAll()
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Company> GetCompanyAsync(Guid companyId) =>
            await FindByCondition(c => c.Id.Equals(companyId))
            .SingleOrDefaultAsync();
    }
}
