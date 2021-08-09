using AutoMapper;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Data.Abstractions;
using HH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<SummaryDto>> GetSummarysAsync(Guid id, CancellationToken token = default)
        {
            var summary = await repository.Summary.GetSummarysAsync(id);

            return mapper.Map<IEnumerable<SummaryDto>>(summary);
        }

        //public async Task<IEnumerable<SummaryDto>> GetSummaryAsync(Guid summaryId, Guid id, CancellationToken token = default)
        //{
            

        //    throw new NotImplementedException();
        //}

        public async Task<SummaryDto> CreateSummaryAsync(Guid id, SummaryForCreationDto summary, CancellationToken token = default)
        {
            if (summary == null)
            {
                throw new ArgumentNullException();
            }

            var employee = await repository.Summary.GetSummarysAsync(id);

            var summaryEntity = mapper.Map<Summary>(summary);

            repository.Summary.CreateSummaryForCompany(id, summaryEntity);
            await repository.SaveAsync();

            var summaryToReturn = mapper.Map<SummaryDto>(summaryEntity);

            return summaryToReturn;
        }

        public async Task DeleteSummaryAsync(Guid employeeId, Guid Id, CancellationToken token = default)
        {
            var employee = await repository.Employee.GetEmployeeAsync(Id);


            var summaryForEmployee = await repository.Summary.GetSummaryAsync(Id, employeeId);


            repository.Summary.DeleteSummary(summaryForEmployee);

            await repository.SaveAsync();
        }



        //public async Task<SummaryDto> PutSummaryAsync(Guid id, Guid summaryId, CancellationToken token = default)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
