using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.Core
{
    public class Summary
    {
        [Column("SummaryId")]
        public Guid Id { get; set; }
        public string Photo { get; set; }

        [Required(ErrorMessage = "FirstName is a required field.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is a required field.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Phone is 10 characters.")]
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "City is a required field.")]
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string Position { get; set; }
        public string WorkExperience { get; set; }
        public string KeySkills { get; set; }
        public string AboutMe { get; set; }
        public string Education { get; set; }
        public string KnowledgeOfLanguages { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
