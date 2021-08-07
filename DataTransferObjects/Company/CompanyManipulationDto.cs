
namespace DataTransferObjects.Company
{
    public abstract class CompanyManipulationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}
