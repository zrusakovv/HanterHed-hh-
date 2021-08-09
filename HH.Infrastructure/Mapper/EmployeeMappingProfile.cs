using AutoMapper;
using HH.Core;
using HH.DTO;

namespace HH.Infrastructure.Mapper
{
    public  class EmployeeMappingProfile: Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeForCreationDto, Employee>();

            CreateMap<EmployeeForUpdateDto, Employee>();
        }
    }
}
