using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.Core
{
    public class Vacancy
    {
        [Column("VacancyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Company name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company price is a required field.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        public string Address { get; set; }
        public string RequiredWorkExperience { get; set; }
        public string Busyness { get; set; }
        public string CompanyDescription { get; set; }
        public string Requirements { get; set; }
        public string Conditions { get; set; }
        public string KeySkills { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
