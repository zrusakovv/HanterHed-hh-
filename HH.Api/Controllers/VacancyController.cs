using AutoMapper;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using HH.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HH.Api.Controllers
{
    [Route("api/vacancys/{companyId}/vacancys")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        public VacancyController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetVacancysFromCompany(Guid companyId)
        {
            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                logger.LogInfo($"Компания с идентификатором: {companyId} не существует в базе данных.");

                return NotFound();
            }

            var vacancys = await repository.Vacancy.GetVacancysAsync(companyId);

            var vacancysDto = mapper.Map<IEnumerable<VacancyDto>>(vacancys);

            return Ok(vacancysDto);
        }

        [HttpGet("{id}", Name = "GetVacancyForCompany")]
        public async Task<IActionResult> GetVacancyFromCompany(Guid companyId, Guid id)
        {
            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                logger.LogInfo($"Компания с идентификатором: {companyId} не существует в базе данных.");

                return NotFound();
            }

            var vacancyDb = await repository.Vacancy.GetVacancyAsync(companyId, id);

            if (vacancyDb == null)
            {
                logger.LogInfo($"Вакансии с id: {id} нет в базе данных.");

                return NotFound();
            }

            var vacancy = mapper.Map<VacancyDto>(vacancyDb);

            return Ok(vacancy);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacancyFromCompany(Guid companyId, [FromBody] VacancyForCreationDto vacancy)
        {
            if (vacancy == null)
            {
                logger.LogError("VacancyForCreationDto объект, отправленный от клиента, имеет значение null.");

                return BadRequest("VacancyForCreationDto объект равен нулю.");
            }

            var company = await repository.Vacancy.GetVacancysAsync(companyId);

            if (company == null)
            {
                logger.LogInfo($"Вакансии с идентификатором: {companyId} нет в базе данных.");

                return NotFound();
            }

            var vacancyEntity = mapper.Map<Vacancy>(vacancy);

            repository.Vacancy.CreateVacancyForCompany(companyId, vacancyEntity);
            await repository.SaveAsync();

            var vacancyToReturn = mapper.Map<VacancyDto>(vacancyEntity);

            return CreatedAtRoute("GetVacancyForCompany", new
            {
                companyId,
                id = vacancyToReturn
            }, vacancyToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancyFromCompany(Guid companyId, Guid id)
        {
            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                logger.LogInfo($"Вакансии с идентификатором: {companyId} нет в базе данных.");

                return NotFound();
            }

            var vacancyForCompany = await repository.Vacancy.GetVacancyAsync(companyId, id);

            if (vacancyForCompany == null)
            {
                logger.LogInfo($"Вакансии с идентификатором: {companyId} нет в базе данных.");

                return NotFound();
            }

            repository.Vacancy.DeleteVacancy(vacancyForCompany);
            await repository.SaveAsync();

            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacancyFromCompany(Guid companyId, Guid id, [FromBody] VacancyForUpdateDto vacancy)
        {
            if (vacancy == null)
            {
                logger.LogError("Объект VacancyForUpdateDto, отправленный клиентом, имеет значение NULL.");
                return BadRequest("Объект VacancyForUpdateDto имеет значение NULL");
            }

            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                logger.LogInfo($"Компания с идентификатором: {companyId} не существует в базе данных.");

                return NotFound();
            }

            var vacancyEntity = await repository.Vacancy.GetVacancyAsync(companyId, id);

            if (vacancyEntity == null)
            {
                logger.LogInfo($"Вакансия с идентификатором: {id} не существует в базе данных.");

                return NotFound();
            }

            mapper.Map(vacancy, vacancyEntity);
            await repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchVacancyFromCompany(Guid companyId, Guid id,
            [FromBody] JsonPatchDocument<VacancyForPatchDto> vacancy)
        {
            if (vacancy == null)
            {
                logger.LogError("Объект вакансии, отправленный от клиента, равен NULL.");
                return BadRequest("Объект вакансии равен NULL");
            }

            var company = await repository.Company.GetCompanyAsync(companyId);

            if (company == null)
            {
                logger.LogInfo($"Компания с идентификатором: {companyId} не существует в базе данных.");

                return NotFound();
            }

            var vacancyEntity = await repository.Vacancy.GetVacancyAsync(companyId, id);

            if (vacancyEntity == null)
            {
                logger.LogInfo($"Вакансии с id: {id} нет в базе данных.");

                return NotFound();
            }

            var vacancyToPatch = mapper.Map<VacancyForPatchDto>(vacancyEntity);

            vacancy.ApplyTo(vacancyToPatch);

            mapper.Map(vacancyToPatch, vacancyEntity);

            await repository.SaveAsync();

            return NoContent();
        }
    }
}
