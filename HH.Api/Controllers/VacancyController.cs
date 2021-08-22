using HH.ApplicationServices.Services.Interfaces;
using HH.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HH.Api.Controllers
{
    [Route("api/vacancys/{companyId}/vacancys")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService vacancyService;
        public VacancyController(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<IActionResult> GetVacancys(Guid companyId)
        {
            var result = await vacancyService.GetVacancysAsync(companyId);

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("{id}", Name = "GetVacancyForCompany")]
        public async Task<IActionResult> GetVacancy(Guid companyId, Guid id)
        {
            var result = await vacancyService.GetVacancyAsync(companyId, id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVacancy(Guid companyId, [FromBody] VacancyForCreationDto vacancy)
        {
            var result = await vacancyService.CreateVacancyAsync(companyId, vacancy);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(Guid companyId, Guid id)
        {
            await vacancyService.DeleteVacancyAsync(companyId, id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacancy(Guid companyId, Guid id, [FromBody] VacancyForUpdateDto vacancy)
        {
            var result = await vacancyService.PutVacancyAsync(companyId, id, vacancy);

            return Ok(result);
        }
    }
}
