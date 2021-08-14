using System;
using System.ComponentModel.DataAnnotations.Schema;
using HH.Core.Domain;

namespace HH.Core
{
    public class Summary : IEntity
    {
        public Guid Id { get; set; }
        public string Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
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
