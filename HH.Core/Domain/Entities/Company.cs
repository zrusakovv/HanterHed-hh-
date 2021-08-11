using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.Core
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
