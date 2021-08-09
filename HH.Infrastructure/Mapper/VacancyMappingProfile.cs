using AutoMapper;
using HH.Core;
using HH.DTO;

namespace HH.Infrastructure.Mapper
{
    public class VacancyMappingProfile: Profile
    {
        public VacancyMappingProfile()
        {
            CreateMap<Vacancy, VacancyDto>();

            CreateMap<VacancyForCreationDto, Vacancy>();

            CreateMap<VacancyForUpdateDto, Vacancy>();
        }
    }
}
