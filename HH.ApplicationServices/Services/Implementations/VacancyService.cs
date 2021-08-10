using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<VacancyDto>> GetVacancys(Guid companyId)
        {
            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                throw new InvalidOperationException($"Компания с идентификатором: {companyId} не существует в базе данных.");
            }

            var vacancys = await repository.Vacancy.GetVacancysAsync(companyId);

            return mapper.Map<IEnumerable<VacancyDto>>(vacancys);
        }

        public async Task<VacancyDto> GetVacancy(Guid companyId, Guid id)
        {
            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                throw new InvalidOperationException($"Компания с идентификатором: {companyId} не существует в базе данных.");
            }

            var vacancyDb = await repository.Vacancy.GetVacancyAsync(companyId, id);

            if (vacancyDb == null)
            {
                throw new InvalidOperationException($"Вакансии с id: {id} нет в базе данных.");
            }

            return mapper.Map<VacancyDto>(vacancyDb);
        }

        public async Task<Guid> CreateVacancy(Guid companyId, VacancyForCreationDto vacancy)
        {
            var company = await repository.Vacancy.GetVacancysAsync(companyId);

            if (company == null)
            {
                throw new InvalidOperationException($"Вакансии с идентификатором: {companyId} нет в базе данных.");
            }

            var vacancyEntity = mapper.Map<Vacancy>(vacancy);

            repository.Vacancy.CreateVacancyForCompany(companyId, vacancyEntity);

            await repository.SaveAsync();

            var vacancyToReturn = mapper.Map<VacancyDto>(vacancyEntity);

            return vacancyToReturn.Id;
        }

        public async Task DeleteVacancy(Guid companyId, Guid id)
        {
            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                throw new InvalidOperationException($"Компании с идентификатором: {companyId} нет в базе данных.");
            }

            var vacancyForCompany = await repository.Vacancy.GetVacancyAsync(companyId, id);

            if (vacancyForCompany == null)
            {
                throw new InvalidOperationException($"Вакансии с идентификатором: {companyId} нет в базе данных.");
            }

            repository.Vacancy.DeleteVacancy(vacancyForCompany);

            await repository.SaveAsync();
        }

        public async Task<VacancyDto> PutVacancy(Guid companyId, Guid id, VacancyForUpdateDto vacancy)
        {
            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                throw new InvalidOperationException($"Компания с идентификатором: {companyId} не существует в базе данных.");
            }

            var vacancyEntity = await repository.Vacancy.GetVacancyAsync(companyId, id);

            if (vacancyEntity == null)
            {
                throw new InvalidOperationException($"Вакансия с идентификатором: {id} не существует в базе данных.");
            }

            mapper.Map(vacancy, vacancyEntity);

            await repository.SaveAsync();

            return mapper.Map<VacancyDto>(vacancyEntity);
        }
    }
}
