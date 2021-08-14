using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HH.ApplicationServices.Services.Implementations
{
    public class SummaryService: ISummaryService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        public SummaryService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SummaryDto>> GetSummarysAsync(Guid employeeId, CancellationToken token = default)
        {
            var summarys = await repository.FindByCondition<Summary>(
                x => x.EmployeeId == employeeId,
                x=>x.Employee
            ).ToListAsync(token);
            
            if (summarys == null)
            {
                throw new ArgumentNullException($"Сотрудника с идентификатором: {employeeId} не существует в базе данных.");
            }
            
            return mapper.Map<IEnumerable<SummaryDto>>(summarys);
        }

        public async Task<SummaryDto> GetSummaryAsync(Guid employeeId, Guid id, CancellationToken token = default)
        {
            var summary = await repository.SingleOrDefaultAsync<Summary>(
                    x => x.EmployeeId == employeeId && x.Id == id,
                    x=>x.Employee,
                    token
                );
            
            if (summary == null)
            {
                throw new EntityNotFoundException($"Резюме с идентификатором: {id} не существует в базе данных.");
            }
            
            return mapper.Map<SummaryDto>(summary);
        }

        public async Task<Guid> CreateSummaryAsync(Guid employeeId, SummaryForCreationDto summary, CancellationToken token = default)
        {
            var summaryEntity = mapper.Map<Summary>(summary);

            summaryEntity.EmployeeId = employeeId;
            
            await repository.Create(summaryEntity, token);
            
            var summaryToReturn = mapper.Map<SummaryDto>(summaryEntity);
            
            return summaryToReturn.Id;
        }

        public async Task DeleteSummaryAsync(Guid employeeId, Guid id, CancellationToken token = default)
        {
            var summary = await repository.SingleOrDefaultAsync<Summary>(
                x => x.EmployeeId == employeeId && x.Id == id,
                x=>x.Employee,
                token
            );
            
            if (summary == null)
            {
                throw new ArgumentNullException($"Резюме с идентификатором: {employeeId} нет в базе данных.");
            }
            
            await repository.DeleteAsync(summary, token);
        }

        public async Task<SummaryDto> UpdateSummaryAsync(Guid employeeId, Guid id, SummaryForUpdateDto summary, CancellationToken token = default)
        {
            var employee = await repository.SingleOrDefaultAsync<Summary>(
                x => x.EmployeeId == employeeId,
                x=>x.Employee,
                token
            );
            
            if (employee == null)
            {
                throw new ArgumentNullException($"Сотрудник с идентификатором: {employeeId} не существует в базе данных.");
            }
            
            var summaryEntity = await repository.SingleOrDefaultAsync<Summary>(
                x => x.EmployeeId == employeeId && x.Id == id,
                x=>x.Employee,
                token
            );
            
            if (summaryEntity == null)
            {
                throw new ArgumentNullException($"Резюме с идентификатором: {id} не существует в базе данных.");
            }
            
            mapper.Map(summary, summaryEntity);
            
            await repository.Update(summaryEntity ,token);
            
            return mapper.Map<SummaryDto>(summaryEntity);
        }

    }
}
