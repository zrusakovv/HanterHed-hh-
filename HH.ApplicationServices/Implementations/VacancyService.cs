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
using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.ApplicationServices.Services.Implementations
{
    public class VacancyService : IVacancyService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        public VacancyService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<VacancyDto>> GetVacancysAsync(Guid companyId, CancellationToken token = default)
        {
            var vacancys = await repository.FindByCondition<Vacancy>(
                x => x.CompanyId == companyId,
                x => x.Company
            ).ToListAsync(token);
            
            if (vacancys == null)
            {
                throw new EntityNotFoundException($"Вакансии с идентификатором: {companyId} не существует в базе данных.");
            }
            
            return mapper.Map<IEnumerable<VacancyDto>>(vacancys);
        }

        public async Task<VacancyDto> GetVacancyAsync(Guid companyId, Guid id, CancellationToken token = default)
        {
            var vacancyDb = await repository.SingleOrDefaultAsync<Vacancy>(
                x => x.CompanyId == companyId && x.Id == id,
                x=>x.Company,
                token
            );
            
            if (vacancyDb == null)
            {
                throw new ArgumentNullException($"Вакансии с id: {companyId} нет в базе данных.");
            }
            
            return mapper.Map<VacancyDto>(vacancyDb);
        }

        public async Task<Guid> CreateVacancyAsync(Guid companyId, VacancyForCreationDto vacancy, CancellationToken token = default)
        {
            var vacancyEntity = mapper.Map<Vacancy>(vacancy);

            vacancyEntity.CompanyId = companyId;
            
            await repository.Create(vacancyEntity, token);
            
            var vacancyToReturn = mapper.Map<VacancyDto>(vacancyEntity);
            
            return vacancyToReturn.Id;
        }

        public async Task DeleteVacancyAsync(Guid companyId, Guid id, CancellationToken token = default)
        {
            var vacancyForCompany = await repository.SingleOrDefaultAsync<Vacancy>(
                x => x.CompanyId == companyId && x.Id == id,
                x=>x.Company,
                token
            );
            
            if (vacancyForCompany == null)
            {
                throw new ArgumentNullException($"Вакансии с идентификатором: {companyId} нет в базе данных.");
            }
            
            await repository.DeleteAsync(vacancyForCompany, token);
        }

        public async Task<VacancyDto> PutVacancyAsync(Guid companyId, Guid id, VacancyForUpdateDto vacancy, CancellationToken token = default)
        {
            var vacancyEntity = await repository.SingleOrDefaultAsync<Vacancy>(
                x => x.CompanyId == companyId && x.Id==id,
                x => x.Company,
                token
            );
            
            if (vacancyEntity == null)
            {
                throw new ArgumentNullException($"Вакансия с идентификатором: {id} не существует в базе данных.");
            }
            
            mapper.Map(vacancy, vacancyEntity);

            await repository.Update(vacancyEntity, token);
            
            return mapper.Map<VacancyDto>(vacancyEntity);
        }
    }
}
