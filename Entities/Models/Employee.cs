using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Phone is 10 characters.")]
        public string Phone { get; set; }
        public string Photo { get; set; }

        public ICollection<Summary> Summaries { get; set; }
    }
}
