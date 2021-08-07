using System.Threading.Tasks;

namespace DataAbstraction
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
