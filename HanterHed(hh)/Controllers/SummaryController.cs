using AutoMapper;
using DataAbstraction;
using DataTransferObjects.Summary;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HanterHed_hh_.Controllers
{
    [Route("api/employees/{employeeId}/summarys")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;
        public SummaryController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSummarysFromCompany(Guid employeeId)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                logger.LogInfo($"Сотрудник с идентификатором: {employeeId} не существует в базе данных.");

                return NotFound();
            }

            var summarys = await repository.Summary.GetSummarysAsync(employeeId);

            var summarysDto = mapper.Map<IEnumerable<SummaryDto>>(summarys);

            return Ok(summarysDto);
        }

        [HttpGet("{id}", Name = "GetSummaryForEmployee")]
        public async Task<IActionResult> GetSummaryFromEmployee(Guid employeeId, Guid id)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                logger.LogInfo($"Сотрудник с идентификатором: {employeeId} не существует в базе данных.");

                return NotFound();
            }

            var summaryDto = await repository.Summary.GetSummaryAsync(employeeId, id);

            if (summaryDto == null)
            {
                logger.LogInfo($"Резюме с id: {id} нет в базе данных.");

                return NotFound();
            }

            var vacancy = mapper.Map<SummaryDto>(summaryDto);

            return Ok(vacancy);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSummaryFromEmployee(Guid employeeId, [FromBody] SummaryForCreationDto summary)
        {
            if (summary == null)
            {
                logger.LogError("SummaryForCreationDto объект, отправленный от клиента, имеет значение null.");

                return BadRequest("SummaryForCreationDto объект равен нулю.");
            }

            var employee = await repository.Summary.GetSummarysAsync(employeeId);

            if (employee == null)
            {
                logger.LogInfo($"Резюме с идентификатором: {employeeId} нет в базе данных.");

                return NotFound();
            }

            var summaryEntity = mapper.Map<Summary>(summary);

            repository.Summary.CreateSummaryForCompany(employeeId, summaryEntity);
            await repository.SaveAsync();

            var summaryToReturn = mapper.Map<SummaryDto>(summaryEntity);

            return CreatedAtRoute("GetSummaryForEmployee", new
            {
                employeeId,
                id = summaryToReturn
            }, summaryToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSummaryFromEmployee(Guid employeeId, Guid id)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                logger.LogInfo($"Резюме с идентификатором: {employeeId} нет в базе данных.");

                return NotFound();
            }

            var summaryForEmployee = await repository.Summary.GetSummaryAsync(employeeId, id);

            if (summaryForEmployee == null)
            {
                logger.LogInfo($"Резюме с идентификатором: {employeeId} нет в базе данных.");

                return NotFound();
            }

            repository.Summary.DeleteSummary(summaryForEmployee);
            await repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSummaryFromEmployee(Guid employeeId, Guid id, [FromBody] SummaryForUpdateDto summary)
        {
            if (summary == null)
            {
                logger.LogError("Объект SummaryForUpdateDto, отправленный клиентом, имеет значение NULL.");
                return BadRequest("Объект SummaryForUpdateDto имеет значение NULL");
            }

            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                logger.LogInfo($"Сотрудник с идентификатором: {employeeId} не существует в базе данных.");

                return NotFound();
            }

            var summaryEntity = await repository.Summary.GetSummaryAsync(employeeId, id);

            if (summaryEntity == null)
            {
                logger.LogInfo($"Сотрудник с идентификатором: {id} не существует в базе данных.");

                return NotFound();
            }

            mapper.Map(summary, summaryEntity);
            await repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchSummaryFromEmployee(Guid employeeId, Guid id,
            [FromBody] JsonPatchDocument<SummaryForPatchDto> summary)
        {
            if (summary == null)
            {
                logger.LogError("Объект резюме, отправленный от клиента, равен NULL.");
                return BadRequest("Объект резюме равен NULL");
            }

            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                logger.LogInfo($"Сотрудник с идентификатором: {employeeId} не существует в базе данных.");

                return NotFound();
            }

            var summaryEntity = await repository.Summary.GetSummaryAsync(employeeId, id);

            if (summaryEntity == null)
            {
                logger.LogInfo($"Резюме с id: {id} нет в базе данных.");

                return NotFound();
            }

            var summaryToPatch = mapper.Map<SummaryForPatchDto>(summaryEntity);

            summary.ApplyTo(summaryToPatch);

            mapper.Map(summaryToPatch, summaryEntity);

            await repository.SaveAsync();

            return NoContent();
        }
    }
}
