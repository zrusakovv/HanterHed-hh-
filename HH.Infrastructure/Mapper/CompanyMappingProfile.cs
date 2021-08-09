using AutoMapper;
using HH.Core;
using HH.DTO;

namespace HH.Infrastructure.Mapper
{
    public class CompanyMappingProfile: Profile
    {
        public CompanyMappingProfile()
        {
            CreateMap<Company, CompanyDto>();

            CreateMap<CompanyForCreationDto, Company>();

            CreateMap<CompanyForUpdateDto, Company>();
        }
    }
}
