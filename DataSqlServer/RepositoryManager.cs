using Data.Abstractions;
using Entities;
using System.Threading.Tasks;

namespace Data.SqlServer
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext repositoryContext;
        private ICompanyRepository companyRepository;
        private IEmployeeRepository employeeRepository;
        private ISummaryRepository summaryRepository;
        private IVacancyRepository vacancyRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public ICompanyRepository Company
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(repositoryContext);

                return companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(repositoryContext);

                return employeeRepository;
            }
        }
        public ISummaryRepository Summary
        {
            get
            {
                if (summaryRepository == null)
                    summaryRepository = new SummaryRepository(repositoryContext);

                return summaryRepository;
            }
        }
        public IVacancyRepository Vacancy
        {
            get
            {
                if (vacancyRepository == null)
                    vacancyRepository = new VacancyRepository(repositoryContext);

                return vacancyRepository;
            }
        }

        public async Task SaveAsync() =>
            await repositoryContext.SaveChangesAsync();
    }
}
