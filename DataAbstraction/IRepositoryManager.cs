using System.Threading.Tasks;

namespace Data.Abstractions
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
