﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Abstractions
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company> GetCompanyAsync(Guid companyId);
        void CreateCompany(Company company);
        void DeleteCompany(Company company);

    }
}
