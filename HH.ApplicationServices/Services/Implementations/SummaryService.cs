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
            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            var summarys = await repository.Summary.GetSummarysAsync(employeeId);

            return mapper.Map<IEnumerable<SummaryDto>>(summarys);
        }

        public async Task<SummaryDto> GetSummaryAsync(Guid employeeId, Guid id, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            var summaryDto = await repository.Summary.GetSummaryAsync(employeeId, id);

            if (summaryDto == null)
            {
                throw new ArgumentException("Summary not found.");
            }

            return mapper.Map<SummaryDto>(summaryDto);
        }

        public async Task<Guid> CreateSummaryAsync(Guid employeeId, SummaryForCreationDto summary, CancellationToken token = default)
        {
            if (summary == null)
            {
                throw new ArgumentException("SummaryForCreationDto объект, отправленный от клиента, имеет значение null.");
            }

            var employee = await repository.Summary.GetSummarysAsync(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Резюме с идентификатором: {employeeId} нет в базе данных."); ;
            }

            var summaryEntity = mapper.Map<Summary>(summary);

            repository.Summary.CreateSummaryForCompany(employeeId, summaryEntity);
            await repository.SaveAsync();

            var summaryToReturn = mapper.Map<SummaryDto>(summaryEntity);

            return summaryToReturn.Id;
        }

        public async Task DeleteSummaryAsync(Guid employeeId, Guid id, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                throw new InvalidOperationException($"Резюме с идентификатором: {employeeId} нет в базе данных.");
            }

            var summaryForEmployee = await repository.Summary.GetSummaryAsync(employeeId, id);

            if (summaryForEmployee == null)
            {
                throw new ArgumentException($"Резюме с идентификатором: {employeeId} нет в базе данных.");
            }

            repository.Summary.DeleteSummary(summaryForEmployee);

            await repository.SaveAsync();
        }

        public async Task<SummaryDto> UpdateSummaryAsync(Guid employeeId, Guid id, SummaryForUpdateDto summary, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"Сотрудник с идентификатором: {employeeId} не существует в базе данных.");
            }

            var summaryEntity = await repository.Summary.GetSummaryAsync(employeeId, id);

            if (summaryEntity == null)
            {
                throw new ArgumentException($"Резюме с идентификатором: {id} не существует в базе данных.");
            }

            mapper.Map(summary, summaryEntity);

            await repository.SaveAsync();

            return mapper.Map<SummaryDto>(summaryEntity);
        }

    }
}
