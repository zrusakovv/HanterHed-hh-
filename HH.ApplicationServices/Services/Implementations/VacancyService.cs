using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Implementations
{
    public class VacancyService : IVacancyService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        public VacancyService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<VacancyDto>> GetVacancysAsync(Guid companyId, CancellationToken token = default)
        {
            var company = await repository.Company.GetCompanyAsync(companyId, token);

            if (company == null)
            {
                throw new ArgumentNullException($"Компания с идентификатором: {companyId} не существует в базе данных.");
            }

            var vacancys = await repository.Vacancy.GetVacancysAsync(companyId, token);

            return mapper.Map<IEnumerable<VacancyDto>>(vacancys);
        }

        public async Task<VacancyDto> GetVacancyAsync(Guid companyId, Guid id, CancellationToken token = default)
        {
            var company = await repository.Company.GetCompanyAsync(companyId, token);

            if (company == null)
            {
                throw new ArgumentNullException($"Компания с идентификатором: {companyId} не существует в базе данных.");
            }

            var vacancyDb = await repository.Vacancy.GetVacancyAsync(companyId, id, token);

            if (vacancyDb == null)
            {
                throw new ArgumentNullException($"Вакансии с id: {id} нет в базе данных.");
            }

            return mapper.Map<VacancyDto>(vacancyDb);
        }

        public async Task<Guid> CreateVacancyAsync(Guid companyId, VacancyForCreationDto vacancy, CancellationToken token = default)
        {
            var company = await repository.Vacancy.GetVacancysAsync(companyId, token);

            if (company == null)
            {
                throw new ArgumentNullException($"Вакансии с идентификатором: {companyId} нет в базе данных.");
            }

            var vacancyEntity = mapper.Map<Vacancy>(vacancy);

            repository.Vacancy.CreateVacancyForCompany(companyId, vacancyEntity, token);

            await repository.SaveAsync(token);

            var vacancyToReturn = mapper.Map<VacancyDto>(vacancyEntity);

            return vacancyToReturn.Id;
        }

        public async Task DeleteVacancyAsync(Guid companyId, Guid id, CancellationToken token = default)
        {
            var company = await repository.Company.GetCompanyAsync(companyId, token);

            if (company == null)
            {
                throw new ArgumentNullException($"Компании с идентификатором: {companyId} нет в базе данных.");
            }

            var vacancyForCompany = await repository.Vacancy.GetVacancyAsync(companyId, id, token);

            if (vacancyForCompany == null)
            {
                throw new ArgumentNullException($"Вакансии с идентификатором: {companyId} нет в базе данных.");
            }

            repository.Vacancy.DeleteVacancy(vacancyForCompany, token);

            await repository.SaveAsync(token);
        }

        public async Task<VacancyDto> PutVacancyAsync(Guid companyId, Guid id, VacancyForUpdateDto vacancy, CancellationToken token = default)
        {
            var company = await repository.Company.GetCompanyAsync(companyId, token);

            if (company == null)
            {
                throw new ArgumentNullException($"Компания с идентификатором: {companyId} не существует в базе данных.");
            }

            var vacancyEntity = await repository.Vacancy.GetVacancyAsync(companyId, id, token);

            if (vacancyEntity == null)
            {
                throw new ArgumentNullException($"Вакансия с идентификатором: {id} не существует в базе данных.");
            }

            mapper.Map(vacancy, vacancyEntity);

            await repository.SaveAsync(token);

            return mapper.Map<VacancyDto>(vacancyEntity);
        }
    }
}
