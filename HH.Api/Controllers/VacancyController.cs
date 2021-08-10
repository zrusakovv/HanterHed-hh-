using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using HH.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        private readonly IVacancyService vacancyService;
        public VacancyController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IVacancyService vacancyService)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.vacancyService = vacancyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVacancys(Guid companyId)
        {
            var result = await vacancyService.GetVacancys(companyId);

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetVacancyForCompany")]
        public async Task<IActionResult> GetVacancy(Guid companyId, Guid id)
        {
            var result = await vacancyService.GetVacancy(companyId, id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacancy(Guid companyId, [FromBody] VacancyForCreationDto vacancy)
        {
            var result = await vacancyService.CreateVacancy(companyId, vacancy);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(Guid companyId, Guid id)
        {
            await vacancyService.DeleteVacancy(companyId, id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacancy(Guid companyId, Guid id, [FromBody] VacancyForUpdateDto vacancy)
        {
            var result = await vacancyService.PutVacancy(companyId, id, vacancy);

            return Ok(result);
        }
    }
}
