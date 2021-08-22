using HH.Core;
using HH.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HH.Api.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompaniesController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var result = await companyService.GetCompaniesAsync();

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }
        
        [Authorize(Roles = "Employee")]
        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var result = await companyService.GetCompanyAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            var result = await companyService.CreateCompanyAsync(company);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await companyService.DeleteCompanyAsync(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            var result = await companyService.UpdateCompanyAsync(id, company);

            return Ok(result);
        }
    }
}
