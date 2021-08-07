
namespace DataTransferObjects.Employee
{
    public abstract class EmployeeManipulationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
    }
}
