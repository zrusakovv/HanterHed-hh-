using AutoMapper;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Core
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public CompanyService
        (
            IRepositoryManager repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> CreateCompanyAsync(CompanyForCreationDto payload, CancellationToken token = default)
        {
            if (payload == null)
            {
                throw new ArgumentNullException();
            }

            var companyEntity = mapper.Map<Company>(payload);

            repository.Company.CreateCompany(companyEntity, token); // Todo: token

            await repository.SaveAsync(token); // Todo: token

            return companyEntity.Id;
        }

        public async Task DeleteCompanyAsync(Guid id, CancellationToken token = default)
        {
            var company = await repository.Company.GetCompanyAsync(id, token); // Todo: token

            if (company == null)
            {
                throw new InvalidOperationException($"Компании с идентификатором: {id} не существует в базе данных.");
            }

            repository.Company.DeleteCompany(company, token); // Todo: token

            await repository.SaveAsync(token); // Todo: token
        }

        public async Task<CompanyDto[]> GetCompaniesAsync(CancellationToken token = default)
        {
            var companies = await repository.Company.GetAllCompaniesAsync(token); // Todo: token

            return mapper.Map<CompanyDto[]>(companies);
        }
        
        public async Task<CompanyDto> GetCompanyAsync(Guid id, CancellationToken token = default)
        {
            var company = await repository.Company.GetCompanyAsync(id, token); // Todo: token

            if (company == null)
            {
                throw new InvalidOperationException($"Компании с идентификатором: {id} не существует в базе данных.");
            }

            return mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(Guid id, CompanyForUpdateDto payload, CancellationToken token = default)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var companyEntity = await repository.Company.GetCompanyAsync(id, token); // Todo: token

            if (companyEntity == null)
            {
                throw new InvalidOperationException($"Компании с идентификатором: {id} не существует в базе данных.");
            }

            mapper.Map(payload, companyEntity);

            await repository.SaveAsync(token); // Todo: token

            return mapper.Map<CompanyDto>(companyEntity);
        }
    }
}
