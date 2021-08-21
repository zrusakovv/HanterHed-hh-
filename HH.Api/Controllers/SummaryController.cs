using HH.ApplicationServices.Services.Interfaces;
using HH.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HH.Api.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/employees/{employeeId}/summarys")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService summaryService;
        public SummaryController(ISummaryService summaryService)
        {
            this.summaryService = summaryService;
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<IActionResult> GetSummarysFromCompany(Guid employeeId)
        {
            var result = await summaryService.GetSummarysAsync(employeeId);

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("{id}", Name = "GetSummaryForEmployee")]
        public async Task<IActionResult> GetSummaryFromEmployee(Guid employeeId, Guid id)
        {
            var result = await summaryService.GetSummaryAsync(employeeId, id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSummaryFromEmployee(Guid employeeId, [FromBody] SummaryForCreationDto summary)
        {
            var result = await summaryService.CreateSummaryAsync(employeeId, summary);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSummaryFromEmployee(Guid employeeId, Guid id)
        {
            await summaryService.DeleteSummaryAsync(employeeId, id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSummaryFromEmployee(Guid employeeId, Guid id, [FromBody] SummaryForUpdateDto summary)
        {
            var result = await summaryService.UpdateSummaryAsync(employeeId, id, summary);

            return Ok(result);
        }

    }
}