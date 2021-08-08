using System.Threading.Tasks;

namespace HH.Data.Abstractions
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        ISummaryRepository Summary { get; }
        IVacancyRepository Vacancy { get; }
        Task SaveAsync();
    }
}
