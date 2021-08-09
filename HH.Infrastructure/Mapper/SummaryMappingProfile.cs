using AutoMapper;
using HH.Core;
using HH.DTO;

namespace HH.Infrastructure.Mapper
{
    public class SummaryMappingProfile: Profile
    {
        public SummaryMappingProfile()
        {
            CreateMap<Summary, SummaryDto>();

            CreateMap<SummaryForCreationDto, Summary>();

            CreateMap<SummaryForUpdateDto, Summary>();
        }
    }
}
