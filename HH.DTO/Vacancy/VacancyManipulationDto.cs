
namespace HH.DTO
{
    public abstract class VacancyManipulationDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        public string RequiredWorkExperience { get; set; }
        public string Busyness { get; set; }
        public string CompanyDescription { get; set; }
        public string Requirements { get; set; }
        public string Conditions { get; set; }
        public string KeySkills { get; set; }
    }
}
