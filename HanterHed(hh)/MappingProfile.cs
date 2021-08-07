
using AutoMapper;
using DataTransferObjects.Company;
using DataTransferObjects.Employee;
using DataTransferObjects.Summary;
using DataTransferObjects.Vacancy;
using Entities.Models;

namespace HanterHed_hh_
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>();

            CreateMap<CompanyForCreationDto, Company>();

            CreateMap<CompanyForUpdateDto, Company>();

            CreateMap<CompanyForPatchDto, Company>().ReverseMap();


            CreateMap<Vacancy, VacancyDto>();

            CreateMap<VacancyForCreationDto, Vacancy>();

            CreateMap<VacancyForUpdateDto, Vacancy>();

            CreateMap<VacancyForPatchDto, Vacancy>().ReverseMap();


            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeForCreationDto, Employee>();

            CreateMap<EmployeeForUpdateDto, Employee>();

            CreateMap<EmployeeForPatchDto, Employee>().ReverseMap();

            CreateMap<Summary, SummaryDto>();

            CreateMap<SummaryForCreationDto, Summary>();

            CreateMap<SummaryForUpdateDto, Summary>();

            CreateMap<SummaryForPatchDto, Summary>().ReverseMap();
        }
    }
}
