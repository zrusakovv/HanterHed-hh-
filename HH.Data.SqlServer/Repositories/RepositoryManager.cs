using HH.Data.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace HH.Data.SqlServer
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DbContext dbContext;
        private ICompanyRepository companyRepository;
        private IEmployeeRepository employeeRepository;
        private ISummaryRepository summaryRepository;
        private IVacancyRepository vacancyRepository;
        public RepositoryManager(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICompanyRepository Company
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(dbContext);

                return companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(dbContext);

                return employeeRepository;
            }
        }
        public ISummaryRepository Summary
        {
            get
            {
                if (summaryRepository == null)
                    summaryRepository = new SummaryRepository(dbContext);

                return summaryRepository;
            }
        }
        public IVacancyRepository Vacancy
        {
            get
            {
                if (vacancyRepository == null)
                    vacancyRepository = new VacancyRepository(dbContext);

                return vacancyRepository;
            }
        }

        public async Task SaveAsync(CancellationToken token = default) =>
            await dbContext.SaveChangesAsync(token);
    }
}
