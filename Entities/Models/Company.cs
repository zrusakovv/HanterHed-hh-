using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Company name is a required field.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country company is a required field.")]
        public string Country { get; set; }
        public string Description { get; set; }
        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
