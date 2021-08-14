using AutoMapper;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HH.Core
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public CompanyService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> CreateCompanyAsync(CompanyForCreationDto payload, CancellationToken token = default)
        {
            var companyEntity = mapper.Map<Company>(payload);

            await repository.Create(companyEntity, token);

            return companyEntity.Id;
        }

        public async Task DeleteCompanyAsync(Guid id, CancellationToken token = default)
        {
            var company = await repository.SingleOrDefaultAsync<Company>(x => x.Id == id, token: token);

            if (company == null)
            {
                throw new EntityNotFoundException($"Компании с идентификатором: {id} не существует в базе данных.");
            }

            await repository.DeleteAsync(company, token);
        }

        public async Task<CompanyDto[]> GetCompaniesAsync(CancellationToken token = default)
        {
            var companies = await repository.FindAll<Company>().ToListAsync(token);

            return mapper.Map<CompanyDto[]>(companies);
        }
        
        public async Task<CompanyDto> GetCompanyAsync(Guid id, CancellationToken token = default)
        {
            var company = await repository.SingleOrDefaultAsync<Company>(
                x => x.Id == id,
                x => x.Vacancies,
                token);

            if (company == null)
            {
                throw new EntityNotFoundException($"Компании с идентификатором: {id} не существует в базе данных.");
            }

            return mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(Guid id, CompanyForUpdateDto payload, CancellationToken token = default)
        {
            var companyEntity = await repository.SingleOrDefaultAsync<Company>(
                x => x.Id == id, 
                token: token
                );

            if (companyEntity == null)
            {
                throw new EntityNotFoundException($"Компании с идентификатором: {id} не существует в базе данных.");
            }

            mapper.Map(payload, companyEntity);

            await repository.Update(companyEntity, token);

            return mapper.Map<CompanyDto>(companyEntity);
        }
    }
}
