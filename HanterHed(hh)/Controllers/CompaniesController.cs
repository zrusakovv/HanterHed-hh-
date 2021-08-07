using AutoMapper;
using DataAbstraction;
using DataTransferObjects.Company;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HanterHed_hh_.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var company = await repository.Company.GetAllCompaniesAsync();

            var companyDto = mapper.Map<IEnumerable<CompanyDto>>(company);

            return Ok(companyDto);
        }

        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await repository.Company.GetCompanyAsync(id);

            if (company == null)
            {
                logger.LogInfo($"Компания с индификатором {id} не существует");

                return NotFound();
            }
            else
            {
                var companyDto = mapper.Map<CompanyDto>(company);

                return Ok(companyDto);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company == null)
            {
                logger.LogError("CompanyForCreationDto отправленный клиентом, имеет значение NULL.");

                return BadRequest("CompanyForCreationDto объект имеет значение null");
            }

            var companyEntity = mapper.Map<Company>(company);

            repository.Company.CreateCompany(companyEntity);
            await repository.SaveAsync();

            var companyToReturn = mapper.Map<CompanyDto>(companyEntity);

            return CreatedAtRoute("CompanyById", new
            {
                id = companyToReturn.Id
            }, companyToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await repository.Company.GetCompanyAsync(id);

            if (company == null)
            {
                logger.LogInfo($"Компания с идентификатором: {id} не существует в базе данных.");

                return NotFound();
            }

            repository.Company.DeleteCompany(company);
            await repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            if (company == null)
            {
                logger.LogError("Объект CompanyForUpdateDto, отправленный клиентом, имеет значение NULL.");

                return BadRequest("Объект CompanyForUpdateDto имеет значение NULL.");
            }

            var companyEntity = await repository.Company.GetCompanyAsync(id);

            if (companyEntity == null)
            {
                logger.LogInfo($"Компания с идентификатором: {id} не существует в базе данных.");

                return NotFound();
            }

            mapper.Map(company, companyEntity);
            await repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCompany(Guid id, [FromBody] JsonPatchDocument<CompanyForPatchDto> company)
        {
            if (company == null)
            {
                logger.LogError("Объект CompanyForUpdateDto, отправленный клиентом, имеет значение NULL.");

                return BadRequest("Объект CompanyForUpdateDto имеет значение NULL.");
            }

            var companyEntity = await repository.Company.GetCompanyAsync(id);

            if (companyEntity == null)
            {
                logger.LogInfo($"Компания с идентификатором: {id} не существует в базе данных.");

                return NotFound();
            }

            var companyToPatch = mapper.Map<CompanyForPatchDto>(companyEntity);

            company.ApplyTo(companyToPatch);

            mapper.Map(companyToPatch, companyEntity);

            await repository.SaveAsync();

            return NoContent();
        }
    }
}
