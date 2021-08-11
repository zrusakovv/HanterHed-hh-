using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Implementations
{
    public class SummaryService: ISummaryService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        public SummaryService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SummaryDto>> GetSummarysAsync(Guid employeeId, CancellationToken token = default)
        {
            var summarys = await repository.Summary.GetSummarysAsync(employeeId, token);

            if (summarys == null)
            {
                throw new InvalidOperationException($"Сотрудника с идентификатором: {employeeId} не существует в базе данных.");
            }

            return mapper.Map<IEnumerable<SummaryDto>>(summarys);
        }

        public async Task<SummaryDto> GetSummaryAsync(Guid employeeId, Guid id, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId, token);

            if (employee == null)
            {
                throw new InvalidOperationException($"Сотрудника с идентификатором: {employeeId} не существует в базе данных.");
            }

            var summaryDto = await repository.Summary.GetSummaryAsync(employeeId, id, token);

            if (summaryDto == null)
            {
                throw new InvalidOperationException($"Резюме с идентификатором: {id} не существует в базе данных.");
            }

            return mapper.Map<SummaryDto>(summaryDto);
        }

        public async Task<Guid> CreateSummaryAsync(Guid employeeId, SummaryForCreationDto summary, CancellationToken token = default)
        {
            var employee = await repository.Summary.GetSummarysAsync(employeeId, token);

            if (employee == null)
            {
                throw new InvalidOperationException($"Резюме с идентификатором: {employeeId} не существует в базе данных."); ;
            }

            var summaryEntity = mapper.Map<Summary>(summary);

            repository.Summary.CreateSummaryForCompany(employeeId, summaryEntity, token);

            await repository.SaveAsync(token);

            var summaryToReturn = mapper.Map<SummaryDto>(summaryEntity);

            return summaryToReturn.Id;
        }

        public async Task DeleteSummaryAsync(Guid employeeId, Guid id, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId, token);

            if (employee == null)
            {
                throw new InvalidOperationException($"Сотрудника с идентификатором: {employeeId} нет в базе данных.");
            }

            var summaryForEmployee = await repository.Summary.GetSummaryAsync(employeeId, id, token);

            if (summaryForEmployee == null)
            {
                throw new InvalidOperationException($"Резюме с идентификатором: {employeeId} нет в базе данных.");
            }

            repository.Summary.DeleteSummary(summaryForEmployee, token);

            await repository.SaveAsync(token);
        }

        public async Task<SummaryDto> UpdateSummaryAsync(Guid employeeId, Guid id, SummaryForUpdateDto summary, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId, token);

            if (employee == null)
            {
                throw new InvalidOperationException($"Сотрудник с идентификатором: {employeeId} не существует в базе данных.");
            }

            var summaryEntity = await repository.Summary.GetSummaryAsync(employeeId, id, token);

            if (summaryEntity == null)
            {
                throw new InvalidOperationException($"Резюме с идентификатором: {id} не существует в базе данных.");
            }

            mapper.Map(summary, summaryEntity);

            await repository.SaveAsync(token);

            return mapper.Map<SummaryDto>(summaryEntity);
        }

    }
}
